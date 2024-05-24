using QuanLiGaraOto.DTOs;
using QuanLiGaraOto.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiGaraOto.Model.service
{
    public class ReceptionService
    {
        public ReceptionService() { }
        private static ReceptionService _ins;
        public static ReceptionService Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new ReceptionService();
                return _ins;
            }
            private set { _ins = value; }
        }

        public async Task<List<ReceptionDTO>> GetAllReception()
        {
            try
            {
                using (var context = new QuanLiGaraOtoEntities())
                {
                    var receptionList = (from s in context.Receptions
                                         where s.IsDeleted == false
                                         select new ReceptionDTO
                                         {
                                             ID = s.ID,
                                             LicensePlate = s.LicensePlate,
                                             Debt = s.Debt,
                                             CreatedAt = s.CreatedAt,
                                             BrandCar = new BrandCarDTO
                                             {
                                                 ID = s.BrandCar.ID,
                                                 Name = s.BrandCar.Name
                                             },
                                             Customer = new CustomerDTO
                                             {
                                                 ID = s.Customer.ID,
                                                 Name = s.Customer.Name,
                                                 PhoneNumber = s.Customer.PhoneNumber,
                                                 Email = s.Customer.Email,
                                                 Address = s.Customer.Address
                                             }
                                         }).ToListAsync();
                    return await receptionList;
                }
            }catch(Exception e)
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Không thể kết nối dữ liệu");
                return null;
            }
            
        }

        public async Task<(bool, string)> AddNewReception(ReceptionDTO newReception)
        {
            try
            {
                var maxNumberOfCar = await ParameterService.Ins.SoXeSuaChuaTrongNgay();
                var curNumberOfCar = await CountByDate(DateTime.Now);
                if (curNumberOfCar >= maxNumberOfCar)
                {
                    return (false, "Number of car in a day is over the limit!");
                }
                using (var context = new QuanLiGaraOtoEntities())
                {

                    var existCustomer = await context.Customers.Where(c => c.PhoneNumber == newReception.Customer.PhoneNumber).FirstOrDefaultAsync();
                    if (existCustomer != null)
                    {
                        if (existCustomer.IsDeleted == true)
                        {
                            existCustomer.IsDeleted = false;

                        }
                        existCustomer.Name = newReception.Customer.Name;
                        existCustomer.PhoneNumber = newReception.Customer.PhoneNumber;
                        existCustomer.Address = newReception.Customer.Address;
                        await context.SaveChangesAsync();
                        var reception = new Reception
                        {
                            LicensePlate = newReception.LicensePlate,
                            Debt = 0,
                            CreatedAt = DateTime.Now,
                            BrandID = newReception.BrandCar.ID,
                            CustomerID = existCustomer.ID,
                            IsDeleted = false
                        };
                        context.Receptions.Add(reception);
                        await context.SaveChangesAsync();
                        return (true, "Add new reception successfully!");
                    }
                    var receptionCus = new Reception
                    {
                        LicensePlate = newReception.LicensePlate,
                        Debt = 0,
                        CreatedAt = DateTime.Now,
                        BrandID = newReception.BrandCar.ID,
                        Customer = new Customer
                        {
                            Name = newReception.Customer.Name,
                            Address = newReception.Customer.Address,
                            PhoneNumber = newReception.Customer.PhoneNumber,
                            Email = "",
                            IsDeleted = false
                        },
                        IsDeleted = false
                    };
                    context.Receptions.Add(receptionCus);
                    await context.SaveChangesAsync();
                    return (true, "Add new reception successfully!");
                }
            }catch(Exception e)
            {
                return (false, null);
            }
            
        }



        public async Task<ReceptionDTO> GetReceptionbyPlate(String plate)
        {
            try
            {
                using (var context = new QuanLiGaraOtoEntities())
                {
                    var reception = await context.Receptions.Where(r => r.LicensePlate == plate).OrderByDescending(r => r.CreatedAt).FirstOrDefaultAsync();
                    if (reception == null)
                    {
                        return null;
                    }
                    var receptionDTO = new ReceptionDTO
                    {
                        ID = reception.ID,
                        LicensePlate = reception.LicensePlate,
                        Debt = reception.Debt,
                        CreatedAt = reception.CreatedAt,
                        BrandCar = new BrandCarDTO
                        {
                            ID = reception.BrandCar.ID,
                            Name = reception.BrandCar.Name
                        },
                        Customer = new CustomerDTO
                        {
                            ID = reception.Customer.ID,
                            Name = reception.Customer.Name,
                            PhoneNumber = reception.Customer.PhoneNumber,
                            Email = reception.Customer.Email,
                            Address = reception.Customer.Address
                        }

                    };
                    return receptionDTO;
                }
            }catch(Exception e)
            {
                return null;
            }
            
        }

        public async Task<int> CountByDate (DateTime date)
        {
            try
            {
                using (var context = new QuanLiGaraOtoEntities())
                {
                    var count = await context.Receptions.Where(r => DbFunctions.TruncateTime(r.CreatedAt) == date.Date && r.IsDeleted == false).CountAsync();

                    Console.WriteLine(date.Date);
                    return count;
                }
            }
            catch (Exception e)
            {
                return -1;
            }
            
        }

        public async Task<decimal> GetDebt (int id)
        {
            try
            {
                using (var context = new QuanLiGaraOtoEntities())
                {
                    var reception = await context.Receptions.Where(r => r.ID == id).FirstOrDefaultAsync();
                    return (decimal)reception.Debt;
                }
            }catch(Exception e)
            {
                return -1;
            }
            
        } 

        public async Task<(bool, string)> UpdateReception(int id, ReceptionDTO reception)
        {
            using(var context = new QuanLiGaraOtoEntities())
            {
                var receptionUpdate = await context.Receptions.Where(r => r.ID == id).FirstOrDefaultAsync();
                if (receptionUpdate == null)
                {
                    return (false, "Reception not found!");
                }
                receptionUpdate.LicensePlate = reception.LicensePlate;
                receptionUpdate.Debt = reception.Debt;
                receptionUpdate.BrandID = reception.BrandCar.ID;
                receptionUpdate.Customer.Name = reception.Customer.Name;
                receptionUpdate.Customer.PhoneNumber = reception.Customer.PhoneNumber;
                receptionUpdate.Customer.Address = reception.Customer.Address;
                receptionUpdate.Customer.Email = reception.Customer.Email;
                receptionUpdate.Customer.IsDeleted = false;
                await context.SaveChangesAsync();
                return (true, "Update reception successfully!");
            }
        }

        public async Task<(bool, string)> DeleteReception(int id)
        {
            using(var context = new QuanLiGaraOtoEntities())
            {
                var reception = await context.Receptions.Where(r => r.ID == id).FirstOrDefaultAsync();
                if (reception == null)
                {
                    return (false, "Reception not found!");
                }
                reception.IsDeleted = true;
                await context.SaveChangesAsync();
                return (true, "Delete reception successfully!");
            }
        }
    }
}
