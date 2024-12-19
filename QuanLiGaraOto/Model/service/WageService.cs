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
    public class WageService
    {
        public WageService() { }
        private static WageService _ins;
        public static WageService Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new WageService();
                return _ins;
            }
            private set { _ins = value; }
        }

        public async Task<List<WageDTO>> GetAllWage()
        {
            try
            {
                using (var context = new QuanLiGaraOtoEntities())
                {
                    var wageList = (from s in context.Wages
                                    where s.IsDeleted == false
                                    select new WageDTO
                                    {
                                        ID = s.ID,
                                        Name = s.Name,
                                        Price = s.Price,
                                    }).ToListAsync();
                    return await wageList;
                }
            } catch(Exception e)
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Không thể kết nối dữ liệu");
                return null;
            }
            
        }

        public async Task<(bool, string)> AddNewWage(WageDTO newWage)
        {
            try
            {
                using (var context = new QuanLiGaraOtoEntities())
                {
                    var wage = new Wage
                    {
                        Name = newWage.Name,
                        Price = newWage.Price,
                        IsDeleted = false
                    };
                    context.Wages.Add(wage);
                    await context.SaveChangesAsync();
                    return (true, "Add new wage successfully!");
                }
            }catch(Exception e)
            {
                return (false, null);
            }
            
        }

        public async Task<(bool, string)> DeleteWage(int id)
        {
            try
            {
                using (var context = new QuanLiGaraOtoEntities())
                {
                    var wage = await context.Wages.Where(w => w.ID == id).FirstOrDefaultAsync();
                    if (wage == null)
                    {
                        return (false, "Wage is not exist!");
                    }
                    if (wage.RepairDetails.Count > 0)
                    {
                        return (false, "This wage is used in repair details!");
                    }
                    wage.IsDeleted = true;
                    await context.SaveChangesAsync();
                    return (true, "Delete wage successfully!");
                }
            }catch(Exception e)
            {
                return (false, null);
            }
            
        }

        public async Task<(bool, string)> UpdateWage(int id, decimal Price)
        {
            try
            {
                using (var context = new QuanLiGaraOtoEntities())
                {
                    var wageUpdate = await context.Wages.Where(w => w.ID == id && w.IsDeleted == false).FirstOrDefaultAsync();
                    wageUpdate.Price = Price;
                    await context.SaveChangesAsync();
                    return (true, "Update wage successfully!");
                }
            }catch(Exception e)
            {
                return (false, null);
            }
            
        }
    }
}
