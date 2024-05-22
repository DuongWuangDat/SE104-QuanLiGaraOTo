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
    public class BrandCarService
    {
        public BrandCarService() { }
        private static BrandCarService _ins;
        public static BrandCarService Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new BrandCarService();
                return _ins;
            }
            private set { _ins = value; }
        }

        public async Task<List<BrandCarDTO>> GetListBrandCar()
        {
            try
            {
                using (var context = new QuanLiGaraOtoEntities())
                {
                    var brandCarList = (from b in context.BrandCars
                                        where b.IsDeleted == false
                                        select new BrandCarDTO
                                        {
                                            ID = b.ID,
                                            Name = b.Name
                                        }).ToListAsync();
                    return await brandCarList;
                }
            }catch (Exception e)
            {
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Không thể lấy danh sách hãng xe!");
                return null;
            }

        }

        public async Task<(bool, string)> AddNewBrandCar(BrandCarDTO newBrand)
        {
            try
            {
                using (var context = new QuanLiGaraOtoEntities())
                {
                    var existBrandCar = await context.BrandCars.Where(b => b.Name == newBrand.Name).FirstOrDefaultAsync();
                    if (existBrandCar != null)
                    {
                        if (existBrandCar.IsDeleted == true)
                        {
                            existBrandCar.IsDeleted = false;
                            existBrandCar.Name = newBrand.Name;
                            await context.SaveChangesAsync();
                            return (true, "Add new brand car successfully!");
                        }
                        return (false, "Brand car is already exist!");
                    }
                    var brandCar = new BrandCar
                    {
                        Name = newBrand.Name,
                        IsDeleted = false
                    };
                    context.BrandCars.Add(brandCar);
                    await context.SaveChangesAsync();
                    return (true, "Add new brand car successfully!");
                }
            }
            catch (Exception e)
            {
                return (false, "Không thể thêm dữ liệu");
            }

        }

        public async Task<(bool,string)> DeleteBrandCar(int id)
        {
            try
            {
                using (var context = new QuanLiGaraOtoEntities())
                {
                    var brandCar = await context.BrandCars.Where(b => b.ID == id).FirstOrDefaultAsync();

                    if (brandCar == null)
                    {
                        return (false, "Brand car is not exist!");
                    }
                    brandCar.IsDeleted = true;
                    await context.SaveChangesAsync();
                    return (true, "Delete brand car successfully!");
                }
            }
            catch (Exception e)
            {
                return (false, "Không thể xóa dữ liệu");
            }

        }
    }
}
