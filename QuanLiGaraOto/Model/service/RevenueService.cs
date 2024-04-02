using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
