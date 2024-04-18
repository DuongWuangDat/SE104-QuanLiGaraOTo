using QuanLiGaraOto.DTOs;
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
            using(var context = new QuanLiGaraOtoEntities())
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
                                         Customer = new CustomerDTO { 
                                             ID = s.Customer.ID,
                                            Name = s.Customer.Name,
                                            PhoneNumber = s.Customer.PhoneNumber,
                                            Email = s.Customer.Email,
                                            Address = s.Customer.Address
                                         }
                                     }).ToListAsync();
                return await receptionList;
            }
        }

        public async Task<(bool, string)> AddNewReception(ReceptionDTO newReception)
        {
            using (var context = new QuanLiGaraOtoEntities())
            {
                var existCustomer = await context.Customers.Where(c => c.PhoneNumber == newReception.Customer.PhoneNumber).FirstOrDefaultAsync();
                if(existCustomer!=null)
                {
                    if(existCustomer.IsDeleted == true)
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
                        CustomerID = newReception.Customer.ID,
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
                        Email = ""
                    },
                    IsDeleted= false
                };
                context.Receptions.Add(receptionCus);
                await context.SaveChangesAsync();
                return (true, "Add new reception successfully!");
            }
        }


        public async Task<int> CountByDate (DateTime date)
        {
            using(var context = new QuanLiGaraOtoEntities())
            {
                var count = await context.Receptions.Where(r => r.CreatedAt == date).CountAsync();
                return count;
            }
        }

        public async Task<decimal> GetDebt (int id)
        {
            using(var context = new QuanLiGaraOtoEntities())
            {
                var reception = await context.Receptions.Where(r => r.ID == id).FirstOrDefaultAsync();
                return (decimal)reception.Debt;
            }
        } 
    }
}
