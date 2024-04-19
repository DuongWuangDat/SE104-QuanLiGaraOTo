using QuanLiGaraOto.View.BaoTriXePage;
using QuanLiGaraOto.View.PhieuThuTien;
using QuanLiGaraOto.View.TraCuuXe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiGaraOto.ViewModel
{
	class MainWindowViewModel : BaseViewModel
	{
		public ICommand LoadBaoTriXePage { get; set; }
		public ICommand LoadSuaChuaXePage { get; set; }
		public ICommand LoadTraCuuXePage { get; set; }
		public ICommand LoadPhieuThuTienPage { get; set; }
		public ICommand LoadBaoCaoThangPage { get; set; }
		public ICommand LoadCaiDatPage { get; set; }
		public MainWindowViewModel()
		{
			LoadBaoTriXePage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
			{
				p.Content = new BaoTriXePage();
			});



			LoadSuaChuaXePage = new RelayCommand<Frame>((p) => { return true; }, (p) => { });
			LoadTraCuuXePage = new RelayCommand<Frame>((p) => { return true; }, (p) => { p.Content = new TraCuuXe(); });
			LoadPhieuThuTienPage = new RelayCommand<Frame>((p) => { return true; }, (p) => { p.Content = new PhieuThuTien(); });
			LoadBaoCaoThangPage = new RelayCommand<Frame>((p) => { return true; }, (p) => { });
			LoadCaiDatPage = new RelayCommand<Frame>((p) => { return true; }, (p) => { });
		}
	}
}
