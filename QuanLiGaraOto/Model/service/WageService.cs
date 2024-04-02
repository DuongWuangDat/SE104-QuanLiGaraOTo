using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiGaraOto.Model.service
{
    public class WageService
    {
        public WageService() { }
        private static WageService _ins;
        public static WageService Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new WageService();
                return _ins;
            }
            private set { _ins = value; }
        }
    }
}
