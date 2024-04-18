using QuanLiGaraOto.DTOs;
using QuanLiGaraOto.Model;
using QuanLiGaraOto.Model.service;
using QuanLiGaraOto.View.PhieuThuTien;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiGaraOto.ViewModel.PhieuThuTienVM
{
	internal class PhieuThuTienViewModel : BaseViewModel
	{
		private ObservableCollection<BillDTO> phieuThuTienCollection;

		public ObservableCollection<BillDTO> PhieuThuTienCollection
		{
			get => phieuThuTienCollection;
			set
			{
				if (phieuThuTienCollection != value)
				{
					phieuThuTienCollection = value;
					OnPropertyChanged(nameof(PhieuThuTienCollection));
					OnPropertyChanged(nameof(Count));
				}
			}
		}

		private List<BillDTO> bills;

		public int Count => PhieuThuTienCollection?.Count ?? 0;

		public ICommand FirstLoad { get; }
		public ICommand ThemPhieuThuTienOpen { get; }
		public ICommand TimKiemPhieuThuTien { get; }

		public PhieuThuTienViewModel()
		{
			FirstLoad = new RelayCommand<object>(_ => true, async _ =>
			{
				bills = await BillService.Ins.GetAllBill();
				PhieuThuTienCollection = new ObservableCollection<BillDTO>(bills);
			});

			TimKiemPhieuThuTien = new RelayCommand<object>(_ => true, p =>
			{
				string searchText = ((TextBox)p).Text.ToLower();
				if (string.IsNullOrEmpty(searchText))
				{
					PhieuThuTienCollection = new ObservableCollection<BillDTO>(bills);
				}
				else
				{
					PhieuThuTienCollection = new ObservableCollection<BillDTO>(bills.FindAll(x => x.Reception.Customer.Name.ToLower().Contains(searchText)
					|| x.Reception.LicensePlate.ToLower().Contains(searchText)));
				}
			});

			ThemPhieuThuTienOpen = new RelayCommand<object>(_ => true, async _ =>
			{
				Window mainWindow = Application.Current.MainWindow; // Lấy cửa sổ chính
				mainWindow.Opacity = 0.5; // Làm mờ cửa sổ chính

				Window dialogWindow = new ThemPhieuThuTien();
				dialogWindow.ShowDialog(); // Hiển thị cửa sổ dialog

				bills = await BillService.Ins.GetAllBill();

				PhieuThuTienCollection = new ObservableCollection<BillDTO>(bills);
				mainWindow.Opacity = 1.0;
			});
		}
	}
}
