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

        public async Task<(bool, string)> updateUserEmail(int id, string email)
        {
            try
            {
                using (var context = new QuanLiGaraOtoEntities())
                {
                    var customer = await context.Customers.Where(x => x.ID == id).FirstOrDefaultAsync();
                    if (customer == null)
                    {
                        return (false, "Customer not found");
                    }
                    customer.Email = email;
                    await context.SaveChangesAsync();
                    return (true, "Update success");
                }
            }catch (Exception e)
            {
                return (false, "Update fail");
            }

        }
    }
}
