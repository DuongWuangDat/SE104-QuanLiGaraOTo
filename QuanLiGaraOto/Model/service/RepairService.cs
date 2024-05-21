using QuanLiGaraOto.DTOs;
using QuanLiGaraOto.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiGaraOto.Model.service
{
    public class RepairService
    {
        public RepairService() { }
        private static RepairService _ins;
        public static RepairService Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new RepairService();
                return _ins;
            }
            private set { _ins = value; }
        }

        public async Task<List<RepairDTO>> GetAllRepair()
        {
            try
            {
                using (var context = new QuanLiGaraOtoEntities())
                {
                    var listRepair = (from s in context.Repairs
                                      where s.IsDeleted == false
                                      select new RepairDTO
                                      {
                                          ID = s.ID,
                                          RepairDate = s.RepairDate,
                                          TotalPrice = s.TotalPrice,
                                          Reception = new ReceptionDTO
                                          {
                                              ID = s.Reception.ID,
                                              LicensePlate = s.Reception.LicensePlate,
                                              Debt = s.Reception.Debt,
                                              CreatedAt = s.Reception.CreatedAt,
                                              BrandCar = new BrandCarDTO
                                              {
                                                  ID = s.Reception.BrandCar.ID,
                                                  Name = s.Reception.BrandCar.Name
                                              },
                                              Customer = new CustomerDTO
                                              {
                                                  ID = s.Reception.Customer.ID,
                                                  Name = s.Reception.Customer.Name,
                                                  PhoneNumber = s.Reception.Customer.PhoneNumber,
                                                  Email = s.Reception.Customer.Email,
                                                  Address = s.Reception.Customer.Address
                                              }
                                          },
                                          RepairDetails = (from x in s.RepairDetails
                                                           where x.RepairID == s.ID && x.IsDeleted == false
                                                           select new RepairDetailDTO
                                                           {
                                                               ID = x.ID,
                                                               Content = x.Content,
                                                               WageId = x.Wage.ID,
                                                               WageName = x.Wage.Name,
                                                               WagePrice = x.WagePrice,
                                                               RepairSuppliesDetails = (from t in x.RepairSuppliesDetails
                                                                                        where t.RepairDetailID == x.ID && t.IsDeleted == false
                                                                                        select new RepairSuppliesDetailDTO
                                                                                        {
                                                                                            Count = t.Count,
                                                                                            PriceItem = t.PriceItem,
                                                                                            Supply = new SupplyDTO
                                                                                            {
                                                                                                ID = t.Supply.ID,
                                                                                                Name = t.Supply.Name,
                                                                                                CountInStock = t.Supply.CountInStock,
                                                                                                InputPrices = t.Supply.InputPrices,
                                                                                                OutputPrices = t.Supply.OutputPrices,

                                                                                            }
                                                                                        }).ToList()
                                                           }).ToList()
                                      }).ToListAsync();
                    return await listRepair;
                }
            }catch(Exception e)
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Không thể kết nối dữ liệu");
                return null;
            }
            
        }
        // total price được tính toán sẵn
        public async Task<(bool, string)> AddNewRepair(RepairDTO newRepair)
        {
            try
            {
                using (var context = new QuanLiGaraOtoEntities())
                {
                    //Find max id of Repair

                    int? maxRepairID = await context.Repairs.MaxAsync(b => (int?)b.ID);

                    int curRepairID = 0;

                    if (maxRepairID.HasValue)
                    {
                        curRepairID = (int)maxRepairID + 1;
                    }
                    else
                    {
                        curRepairID = 1;
                    }
                    Console.WriteLine(curRepairID);
                    //Find max id of RepairDetail

                    int? maxRepairDetailID = await context.RepairDetails.MaxAsync(b => (int?)b.ID);
                    int curRepairDetailID = 0;
                    if (maxRepairDetailID.HasValue)
                    {
                        curRepairDetailID = (int)maxRepairDetailID + 1;
                    }
                    else
                    {
                        curRepairDetailID = 1;
                    }
                    var recept = await context.Receptions.Where(b => b.ID == newRepair.Reception.ID).FirstOrDefaultAsync();
                    var repair = new Repair
                    {
                        ReceptionID = newRepair.Reception.ID,
                        RepairDate = newRepair.RepairDate,
                        IsDeleted = false,
                    };

                    var totalPrice = 0;
                    List<RepairDetail> RepairDetailsList = new List<RepairDetail>();
                    List<RepairSuppliesDetail> RepairSuppliesDetailsList = new List<RepairSuppliesDetail>();
                    foreach (var rpDT in newRepair.RepairDetails)
                    {
                        var totalPriceDetail = 0;
                        var repairDetail = new RepairDetail
                        {
                            RepairID = curRepairID,
                            WageID = rpDT.WageId,
                            Content = rpDT.Content,
                            Price = rpDT.Price,
                            WagePrice = rpDT.WagePrice,
                            IsDeleted = false
                        };
                        foreach (var rpSDT in rpDT.RepairSuppliesDetails)
                        {

                            var countInStock = await SuppliesService.Ins.GetCountInStock(rpSDT.Supply.ID);
                            if (countInStock < rpSDT.Count)
                            {
                                return (false, "Not enough supply in stock");
                            }
                            var repairSuppliesDetail = new RepairSuppliesDetail
                            {
                                RepairDetailID = curRepairDetailID,
                                SuppliesID = rpSDT.Supply.ID,
                                Count = rpSDT.Count,
                                PriceItem = rpSDT.PriceItem,
                                IsDeleted = false
                            };
                            totalPriceDetail += (int)rpSDT.PriceItem * (int)(rpSDT.Count);
                            RepairSuppliesDetailsList.Add(repairSuppliesDetail);
                            //Modify supply
                            var (isSuccessSp, newCountInStock) = await DecreaseCountInStockSp((int)rpSDT.Count, rpSDT.Supply.ID);

                            //-----------------
                            //Modify inventory report
                            var isSuccessTonCuoi = await InvetoryReportService.Ins.UpdateTonCuoi(newCountInStock, rpSDT.Supply.ID);
                            if (!isSuccessTonCuoi || !isSuccessSp)
                            {
                                return (false, "Something went wrong");
                            }
                            //-----------------------
                        }
                        totalPriceDetail += (int)rpDT.WagePrice;
                        repairDetail.Price = totalPriceDetail;
                        totalPrice += totalPriceDetail;
                        totalPriceDetail = 0;
                        RepairDetailsList.Add(repairDetail);
                        curRepairDetailID++;
                    }
                    recept.Debt += totalPrice;
                    repair.TotalPrice = totalPrice;
                    context.Repairs.Add(repair);
                    await context.SaveChangesAsync();
                    context.RepairDetails.AddRange(RepairDetailsList);
                    await context.SaveChangesAsync();
                    context.RepairSuppliesDetails.AddRange(RepairSuppliesDetailsList);


                    await context.SaveChangesAsync();
                    //Modify revenue service
                    var isSuccessRevenue = await RevenueService.Ins.AddRepairRevenueDetail(repair, (DateTime)repair.RepairDate);
                    if (!isSuccessRevenue)
                    {
                        return (false, "Something went wrong");
                    }

                    //---------------------
                    return (true, "Add new repair successfully");
                }
            }catch(Exception e)
            {
                return (false, null);
            }
            
        }
        
        public async Task<(bool, string)> DeleteRepair(int id)
        {
            try
            {
                using (var context = new QuanLiGaraOtoEntities())
                {
                    var repair = await context.Repairs.Where(b => b.ID == id).FirstOrDefaultAsync();
                    if (repair == null)
                    {
                        return (false, "Repair is not exist!");
                    }

                    var isSuccessDeleteRevenue = await RevenueService.Ins.DeleteRepairRevenueDetail(repair);
                    var isSuccessRecoveryInventory = await InvetoryReportService.Ins.RecoveryInventory(repair);
                    if (!isSuccessDeleteRevenue || !isSuccessRecoveryInventory)
                    {
                        return (false, "Something went wrong");
                    }

                    repair.IsDeleted = true;
                    await context.SaveChangesAsync();
                    return (true, "Delete repair successfully!");
                }
            }catch(Exception e)
            {
                return (false, null);
            }
            
        }

        public async Task<(bool, int)> DecreaseCountInStockSp(int delta, int supplyId)
        {
            try
            {
                if (delta <= 0)
                {
                    return (false, -1);
                }
                using (var context = new QuanLiGaraOtoEntities())
                {
                    var supply = await context.Supplies.Where(s => s.ID == supplyId).FirstOrDefaultAsync();
                    supply.CountInStock -= delta;
                    await context.SaveChangesAsync();
                    return (true, (int)supply.CountInStock);
                }
            }catch(Exception e)
            {
                return (false, -1);
            }
            
        }
    }
}
