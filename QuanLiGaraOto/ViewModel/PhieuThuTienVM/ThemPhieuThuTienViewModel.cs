using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiGaraOto.ViewModel.PhieuThuTienVM
{
	internal class ThemPhieuThuTienViewModel : BaseViewModel
	{
		private string _name;
		public string Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}
	}
}
