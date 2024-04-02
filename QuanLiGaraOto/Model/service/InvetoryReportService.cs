using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiGaraOto.Model.service
{
    public class InvetoryReportService
    {
        public InvetoryReportService() { }
        private static InvetoryReportService _ins;
        public static InvetoryReportService Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new InvetoryReportService();
                return _ins;
            }
            private set { _ins = value; }
        }
    }
}
