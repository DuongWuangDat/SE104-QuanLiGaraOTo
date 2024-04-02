using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
