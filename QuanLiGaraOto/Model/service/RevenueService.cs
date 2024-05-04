using QuanLiGaraOto.DTOs;
using QuanLiGaraOto.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace QuanLiGaraOto.Model.service
{
    public class RevenueService
    {
        public RevenueService() { }
        private static RevenueService _ins;
        public static RevenueService Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new RevenueService();
                return _ins;
            }
            private set { _ins = value; }
        }

        public async Task<RevenueDTO> GetRevenue(int Month, int Year)
        {
            try
            {
                using (var context = new QuanLiGaraOtoEntities())
                {
                    var revenueList = (from r in context.Revenues
                                       where r.IsDeleted == false
                                       select new RevenueDTO
                                       {
                                           ID = r.ID,
                                           Month = r.Month,
                                           Year = r.Year,
                                           TotalPrice = r.TotalPrice,
                                           RevenueDetails = (from s in r.RevenueDetails
                                                             select new RevenueDetailDTO
                                                             {
                                                                 RepairCount = s.RepairCount,
                                                                 Ratio = s.Ratio,
                                                                 Price = s.Price,
                                                                 BrandCar = new BrandCarDTO
                                                                 {
                                                                     ID = s.BrandCar.ID,
                                                                     Name = s.BrandCar.Name
                                                                 }

                                                             }).ToList()
                                       }).FirstOrDefaultAsync();
                    return await revenueList;
                }

            }catch(Exception e)
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Không thể kết nối dữ liệu");
                return null;
            }
            
        }   

        public async Task<(bool, string)> InitRevenue()
        {
            try
            {
                using (var context = new QuanLiGaraOtoEntities())
                {
                    var curDate = DateTime.Now;
                    var isExist = await context.Revenues.AnyAsync(r => r.Month == curDate.Month && r.Year == curDate.Year);
                    if (isExist)
                    {
                        return (false, "Bao cao doanh thu ton tai");
                    }
                    var revenue = new Revenue
                    {
                        Month = curDate.Month,
                        Year = curDate.Year,
                        TotalPrice = 0,
                        IsDeleted = false
                    };
                    context.Revenues.Add(revenue);
                    await context.SaveChangesAsync();
                    return (true, "");
                }

            }catch(Exception e)
            {
                return (false, null);
            }
            
        }

        public async Task<Revenue> GetCurrentRevenueReport()
        {
            try
            {
                using (var context = new QuanLiGaraOtoEntities())
                {
                    var curDate = DateTime.Now;
                    var revenue = await context.Revenues.Where(r => r.Month == curDate.Month && r.Year == curDate.Year).FirstOrDefaultAsync();
                    return revenue;
                }
            }catch(Exception e)
            {
                return null;
            }
            
        }

        public async Task<bool> AddRepairRevenueDetail(Repair repair, DateTime date)
        {
            try
            {
                using (var context = new QuanLiGaraOtoEntities())
                {
                    var curRevenue = await context.Revenues.Where(r => r.Month == date.Month && r.Year == date.Year).FirstOrDefaultAsync();
                    var detail = await context.RevenueDetails.Where(r => r.RevenueId == curRevenue.ID && r.BrandCarId == repair.Reception.BrandCar.ID).FirstOrDefaultAsync();
                    var allDetail = await context.RevenueDetails.Where(r => r.RevenueId == curRevenue.ID).ToListAsync();
                    var revenue = await context.Revenues.Where(r => r.ID == curRevenue.ID).FirstOrDefaultAsync();
                    revenue.TotalPrice += repair.TotalPrice;
                    foreach (var item in allDetail)
                    {
                        item.Ratio = (double)item.Price / (double)revenue.TotalPrice;
                    }
                    await context.SaveChangesAsync();
                    if (detail == null)
                    {
                        var newDetail = new RevenueDetail
                        {
                            RevenueId = curRevenue.ID,
                            BrandCarId = (int)repair.Reception.BrandCar.ID,
                            RepairCount = 1,
                            Ratio = (double)repair.TotalPrice / (double)revenue.TotalPrice,
                            Price = repair.TotalPrice,
                            IsDeleted = false
                        };
                        context.RevenueDetails.Add(newDetail);
                        await context.SaveChangesAsync();
                        return true;

                    }
                    detail.RepairCount += 1;
                    detail.Price += repair.TotalPrice;
                    detail.Ratio = (double)detail.Price / (double)revenue.TotalPrice;
                    await context.SaveChangesAsync();
                    return true;
                }
            }catch(Exception e)
            {
                return false;
            }
            
        }

        public async Task<bool> DeleteRepairRevenueDetail(Repair repair)
        {
            try
            {
                using (var context = new QuanLiGaraOtoEntities())
                {
                    var curRevenue = await GetCurrentRevenueReport();
                    var detail = await context.RevenueDetails.Where(r => r.BrandCarId == repair.Reception.BrandID && r.RevenueId == curRevenue.ID).FirstOrDefaultAsync();
                    curRevenue.TotalPrice -= repair.TotalPrice;
                    detail.RepairCount -= 1;
                    detail.Price -= repair.TotalPrice;
                    detail.Ratio = (double)detail.Price / (double)curRevenue.TotalPrice;
                    await context.SaveChangesAsync();
                    return true;
                }
            }catch(Exception e)
            {
                return false;
            }
            
        }
        public async Task<bool> DeleteRevenueDetial(int brandCarID)
        {
            try
            {
                using (var context = new QuanLiGaraOtoEntities())
                {
                    var curRevenue = await GetCurrentRevenueReport();
                    var detail = await context.RevenueDetails.Where(r => r.BrandCarId == brandCarID && r.RevenueId == curRevenue.ID).FirstOrDefaultAsync();
                    context.RevenueDetails.Remove(detail);
                    await context.SaveChangesAsync();
                    return true;
                }

            }catch(Exception e)
            {
                return false;
            }
            
        }
    }
}
