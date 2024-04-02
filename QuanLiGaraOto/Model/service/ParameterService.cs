using QuanLiGaraOto.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
                if (parameter == null)
                {
                    return (false,"Không tìm thấy tham số nào");
                }
                parameter.Value = value;
                await context.SaveChangesAsync();
                return (true, "Sửa parameter thành công");
            }
        }
    }
}
