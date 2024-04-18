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


    }
}
