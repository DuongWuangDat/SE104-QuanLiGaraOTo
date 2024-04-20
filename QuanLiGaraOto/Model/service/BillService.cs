using QuanLiGaraOto.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiGaraOto.Model.service
{
    public class BillService
    {
        public BillService() { }

        private static BillService _ins;
        public static BillService Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new BillService();
                return _ins;
            }
            private set { _ins = value; }
        }

        public async Task<List<BillDTO>> GetAllBill()
        {
            using (var context = new QuanLiGaraOtoEntities())
            {
                var billList = (from s in context.Bills
                                where s.IsDeleted == false
                                select new BillDTO
                                {
                                    ID = s.ID,
                                    Reception = new ReceptionDTO
                                    {
                                        ID = s.Reception.ID,
                                        LicensePlate = s.Reception.LicensePlate,
                                        Debt = s.Reception.Debt,
                                        CreatedAt = s.Reception.CreatedAt,
                                        BrandCar = new BrandCarDTO { 
                                            ID = s.Reception.BrandCar.ID, 
                                            Name = s.Reception.BrandCar.Name 
                                        },
                                        Customer = new CustomerDTO { 
                                            ID = s.Reception.Customer.ID, 
                                            Name = s.Reception.Customer.Name, 
                                            PhoneNumber = s.Reception.Customer.PhoneNumber, 
                                            Email = s.Reception.Customer.Email, 
                                            Address = s.Reception.Customer.Address 
                                        }
                                    },
                                    CreateAt = s.CreateAt,
                                    Proceeds = s.Proceeds
                                }).ToListAsync();
                return await billList;
            }
        }

        public async Task<(bool, string)> AddNewBill(BillDTO newBill)
        {
            var apdungPhat = await ParameterService.Ins.ApDungPhat();
            if (newBill.Proceeds <= 0)
            {
                return (false, "Số tiền thu phải lớn hơn 0");
            }
            if ((newBill.Proceeds > newBill.Reception.Debt) && apdungPhat)
            {
                return (false, "Số tiền thu không được lớn hơn số tiền nợ");
            }
            
            using (var context = new QuanLiGaraOtoEntities())
            {
                var bill = new Bill
                {
                    ReceptionID = newBill.Reception.ID,
                    CreateAt = newBill.CreateAt,
                    Proceeds = newBill.Proceeds,
                    IsDeleted = false
                };
                var reception = await context.Receptions.Where(r => r.ID == newBill.Reception.ID).FirstOrDefaultAsync();
                reception.Debt -= bill.Proceeds;
                context.Bills.Add(bill);
                await context.SaveChangesAsync();
                return (true, "Thêm hóa đơn thành công");
            }
        }

        public async Task<(bool, string)> DeleteBill(int id)
        {
            using(var context = new QuanLiGaraOtoEntities())
            {
                var bill = await context.Bills.Where(b => b.ID == id).FirstOrDefaultAsync();
                if (bill == null)
                    return (false, "Không tìm thấy hóa đơn");
                var recept = await context.Receptions.Where(c => c.ID == bill.ReceptionID).FirstOrDefaultAsync();
                recept.Debt += bill.Proceeds;
                bill.IsDeleted = true;
                await context.SaveChangesAsync();
                return (true, "Xóa hóa đơn thành công");
            }
        }

        public async Task<(bool, string)> UpdateBill(int id, BillDTO bill)
        {
            using(var context = new QuanLiGaraOtoEntities())
            {
                var billUpdate = await context.Bills.Where(b => b.ID == id && b.IsDeleted== false).FirstOrDefaultAsync();
                if (billUpdate == null)
                    return (false, "Không tìm thấy hóa đơn");
                var recept = await context.Receptions.Where(c => c.ID == billUpdate.ReceptionID).FirstOrDefaultAsync();
                recept.Debt += bill.Proceeds;
                billUpdate.ReceptionID = bill.Reception.ID;
                billUpdate.CreateAt = bill.CreateAt;
                billUpdate.Proceeds = bill.Proceeds;
                recept.Debt -= bill.Proceeds;
                await context.SaveChangesAsync();
                return (true, "Cập nhật hóa đơn thành công");

            }
        }
    }
}
