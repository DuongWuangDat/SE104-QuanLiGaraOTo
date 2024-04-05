using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiGaraOto.Model.service
{
    public class CustomerService
    {
        public CustomerService() { }
        private static CustomerService _ins;
        public static CustomerService Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new CustomerService();
                return _ins;
            }
            private set { _ins = value; }
        }

        public async Task<decimal> GetTotalDebt(int id)
        {
            using(var context = new QuanLiGaraOtoEntities())
            {
                var user = await context.Customers.Where(c => c.ID == id).FirstOrDefaultAsync();
                return (decimal)user.TotalDebt;
            }
        }
    }
}
