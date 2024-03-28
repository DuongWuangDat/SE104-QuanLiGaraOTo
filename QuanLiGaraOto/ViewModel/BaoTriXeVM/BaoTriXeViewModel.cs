using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiGaraOto.ViewModel.BaoTriXeVM
{
    public class BaoTriXeViewModel : BaseViewModel
    {
		private int _receptionCount;

		public int ReceptionCount
        {
			get { return _receptionCount; }
			set { _receptionCount = value; }
		}


	}
}
