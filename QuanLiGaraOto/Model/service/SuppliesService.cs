using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiGaraOto.Model.service
{
    public class SuppliesService
    {
        public SuppliesService() { }
        private static SuppliesService _ins;
        public static SuppliesService Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new SuppliesService();
                return _ins;
            }
            private set { _ins = value; }
        }
    }
}
