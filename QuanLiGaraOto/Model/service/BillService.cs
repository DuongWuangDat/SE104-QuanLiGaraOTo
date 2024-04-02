using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiGaraOto.Model.service
{
    public class BillService
    {
        public BillService() { }

        private static BillService _ins;
        public static BillService Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new BillService();
                return _ins;
            }
            private set { _ins = value; }
        }   
    }
}
