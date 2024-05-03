using QuanLiGaraOto.Model.service;
using QuanLiGaraOto.View.BaoTriXePage;
using QuanLiGaraOto.View.MessageBox;
using QuanLiGaraOto.View.PhieuThuTien;
using QuanLiGaraOto.View.TraCuuXe;
using QuanLiGaraOto.View.SuaChuaXe;
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
		public ICommand FirstLoadCM { get; set;}

        public ICommand LoadBaoTriXePage { get; set; }
		public ICommand LoadSuaChuaXePage { get; set; }
		public ICommand LoadTraCuuXePage { get; set; }
		public ICommand LoadPhieuThuTienPage { get; set; }
		public ICommand LoadBaoCaoThangPage { get; set; }
		public ICommand LoadCaiDatPage { get; set; }
		public MainWindowViewModel()
		{
			FirstLoadCM = new RelayCommand<Frame>((p) => { return true; },async (p) => {
				var (isSuccessInvenLoaded, message) = await InvetoryReportService.Ins.InitInventoryReport();
				var (isSuccessRevenueLoaded,messageRevenue) = await RevenueService.Ins.InitRevenue();
				if (!isSuccessInvenLoaded || !isSuccessRevenueLoaded)
				{
					if(message != "Báo cáo tồn kho tháng này đã được khởi tạo" || messageRevenue != "Bao cao doanh thu ton tai")
					{
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Không thể khởi tạo dữ liệu");
                    }
					
				}
				p.Content = new BaoTriXePage();
			});

			LoadBaoTriXePage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
			{
				p.Content = new BaoTriXePage();
			});




			LoadSuaChuaXePage = new RelayCommand<Frame>((p) => { return true; }, (p) => { p.Content = new PhieuSuaChuaXe(); });
			LoadTraCuuXePage = new RelayCommand<Frame>((p) => { return true; }, (p) => { p.Content = new TraCuuXe(); });
			LoadPhieuThuTienPage = new RelayCommand<Frame>((p) => { return true; }, (p) => { p.Content = new PhieuThuTien(); });
			LoadBaoCaoThangPage = new RelayCommand<Frame>((p) => { return true; }, (p) => { });
			LoadCaiDatPage = new RelayCommand<Frame>((p) => { return true; }, (p) => { });
		}
	}
}
