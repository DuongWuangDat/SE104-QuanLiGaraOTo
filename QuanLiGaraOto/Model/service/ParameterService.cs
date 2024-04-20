using QuanLiGaraOto.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuanLiGaraOto.Model.service
{
    public class ParameterService
    {
        public ParameterService() { }
        private static ParameterService _ins;
        public static ParameterService Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new ParameterService();
                return _ins;
            }
            private set { _ins = value; }
        }

        public async Task<List<ParameterDTO>> GetAllParameter()
        {
            using(var context = new QuanLiGaraOtoEntities())
            {
                var listParameter = (from s in context.Parameters
                                    select new ParameterDTO
                                    {
                                         Name = s.Name,
                                         Value = s.Value
                                     }).ToListAsync();
                return await listParameter;
            }
        }

        public async Task<(bool,string)> UpdateParameter(string name, float value)
        {
            using (var context = new QuanLiGaraOtoEntities())
            {
                var parameter = await context.Parameters.FirstOrDefaultAsync(x => x.Name == name);
                if(name == "TiLeTinhDonGiaBan")
                {
                   await SuppliesService.Ins.UpdateTiLe((double)value);
                }
                if(name == "SoXeSuaChuaToiDa")
                {
                   var numberOfCar = await ReceptionService.Ins.CountByDate(DateTime.Now);
                    if(numberOfCar > value)
                    {
                          return (false, "Số xe sửa chữa trong ngày đã vượt quá số xe tối đa");
                     }
                }
                parameter.Value = value;
                await context.SaveChangesAsync();
                return (true, "Sửa parameter thành công");
            }
        }

        public async Task<double> GetValueByName(string name)
        {
            using (var context = new QuanLiGaraOtoEntities())
            {
                var parameter = await context.Parameters.FirstOrDefaultAsync(x => x.Name == name);
                if (parameter == null)
                {
                    return -1;
                }
                return (double)parameter.Value;
            }
        }

        public async Task<double> GetRatio()
        {
            using (var context = new QuanLiGaraOtoEntities())
            {
                var parameter = await context.Parameters.FirstOrDefaultAsync(x => x.Name == "TiLeTinhDonGiaBan");                                                                                   
                if (parameter == null)
                {
                    return -1;
                }
                return (double)parameter.Value;
            }
        }

        public async Task<bool> ApDungPhat()
        {
            using(var context = new QuanLiGaraOtoEntities())
            {
                var parameter = await context.Parameters.FirstOrDefaultAsync(x => x.Name == "ApDungQÐKiemTraSoTienThu")                                                                          ");
                if(parameter.Value == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<int> SoXeSuaChuaTrongNgay()
        {
            using(var context = new QuanLiGaraOtoEntities())
            {
                var parameter = await context.Parameters.FirstOrDefaultAsync(x => x.Name == "SoXeSuaChuaToiDa")                                                                                    ");
                return (int)parameter.Value;
            }
        }
    }
}
