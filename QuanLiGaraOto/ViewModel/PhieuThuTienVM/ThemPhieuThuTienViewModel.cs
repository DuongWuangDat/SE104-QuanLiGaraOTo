using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
		public ICommand ThemPhieuThuTien { get; set; }

		public ThemPhieuThuTienViewModel ()
		{
			ThemPhieuThuTien = new RelayCommand<object>((p) => { return true; }, (p) =>
			{
				// Code here
			});
		}
	}
}
