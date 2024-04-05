using MaterialDesignThemes.Wpf.Converters;
using QuanLiGaraOto.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiGaraOto.Model.service
{
    public class InvetoryReportService
    {
        public InvetoryReportService() { }
        private static InvetoryReportService _ins;
        public static InvetoryReportService Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new InvetoryReportService();
                return _ins;
            }
            private set { _ins = value; }
        }
        
        
        //Should check current date before get inventory report
        public async Task<InventoryReportDTO> GetInventoryReport(int Month, int Year)
        {
            using(var context = new QuanLiGaraOtoEntities())
            {
                var inventoryReportList = (from s in context.InventoryReports
                                           where s.IsDeleted == false && s.Month == Month && s.Year == Year
                                           select new InventoryReportDTO
                                           {
                                               ID = s.ID,
                                               Month = s.Month,
                                               Year = s.Year,
                                               InventoryReportDetails = (from d in s.InventoryReportDetails
                                                                         where d.IsDeleted == false
                                                                         select new InventoryReportDetailDTO
                                                                         {
                                                                             TonDau = d.TonDau,
                                                                             PhatSinh = d.PhatSinh,
                                                                             TonCuoi = d.TonCuoi,
                                                                             SupplyID = d.SuppliesID,
                                                                             SupplyName = d.Supply.Name
                                                                         }).ToList()

                                           }).FirstOrDefaultAsync();
                return await inventoryReportList;
            }
        }

        public async Task<(bool, string)> InitInventoryReport()
        {
            using(var context = new QuanLiGaraOtoEntities())
            {
                var supplies = await SuppliesService.Ins.GetAllSupply();
                var curDate = DateTime.Now;
                var curMonth = curDate.Month;
                var curYear = curDate.Year;
                var inventoryReport = await GetInventoryReport(curMonth , curYear);
                if(inventoryReport != null)
                {
                    return (false, "Báo cáo tồn kho tháng này đã được khởi tạo");
                }
                int? maxID = await context.Repairs.MaxAsync(b => (int?)b.ID);

                int curID = 0;

                if (maxID.HasValue)
                {
                    curID = (int)maxID + 1;
                }
                else
                {
                    curID = 1;
                }
                var newInventoryReport = new InventoryReport
                {
                    Month = curMonth,
                    Year = curYear,
                    IsDeleted = false
                };
                List<InventoryReportDetail> detailList = new List<InventoryReportDetail>();
                foreach(var sp in supplies)
                {
                    var inventoryReportDT = new InventoryReportDetail
                    {
                        InventoryReportID = curID,
                        SuppliesID = sp.ID,
                        TonDau = sp.CountInStock,
                        PhatSinh = 0,
                        TonCuoi = sp.CountInStock,
                        IsDeleted = false
                    };
                    detailList.Add(inventoryReportDT);
                }
                context.InventoryReportDetails.AddRange(detailList);
                context.InventoryReports.Add(newInventoryReport);
                await context.SaveChangesAsync();
                return (true, "Initialize inventory report successfully");

            }
        }

        public async Task<InventoryReportDTO> GetCurrentInventoryReport()
        {
            var curDate = DateTime.Now;
            var inventoryReport = await GetInventoryReport(curDate.Month, curDate.Year);
            return inventoryReport;
        }

        public async Task<bool> AddNewSupply(Supply newSupply)
        {
            using(var context = new QuanLiGaraOtoEntities())
            {
                var inventoryReport = await GetCurrentInventoryReport();
                var newDetail = new InventoryReportDetail
                {
                    InventoryReportID = inventoryReport.ID,
                    SuppliesID = newSupply.ID,
                    TonDau = 0,
                    PhatSinh = 0,
                    TonCuoi = 0,
                    IsDeleted = false
                };
                context.InventoryReportDetails.Add(newDetail);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> UpdatePhatSinh(int delta, int supplyId)
        {
            if(delta <= 0)
            {
                return false;
            }
            using(var context = new QuanLiGaraOtoEntities())
            {
                var inventoryReport = await GetCurrentInventoryReport();
                var detail = await context.InventoryReportDetails.Where(b => b.SuppliesID == supplyId && b.InventoryReportID == inventoryReport.ID).FirstOrDefaultAsync();
                detail.PhatSinh += delta;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> UpdateTonCuoi(int tonCuoi, int supplyId)
        {
            using (var context = new QuanLiGaraOtoEntities())
            {
                var inventoryReport = await GetCurrentInventoryReport();
                var detail = await context.InventoryReportDetails.Where(b => b.SuppliesID == supplyId && b.InventoryReportID == inventoryReport.ID).FirstOrDefaultAsync();
                detail.TonCuoi = tonCuoi;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> DeleteDetail(int supplyId)
        {
            using (var context = new QuanLiGaraOtoEntities())
            {
                var inventoryReport = await GetCurrentInventoryReport();
                var detail = await context.InventoryReportDetails.Where(b => b.SuppliesID == supplyId && b.InventoryReportID == inventoryReport.ID).FirstOrDefaultAsync();
                detail.IsDeleted = true;
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
