using QuanLiGaraOto.DTOs;
using QuanLiGaraOto.Model.service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiGaraOto.ViewModel.PhieuThuTienVM
{
	internal class PhieuThuTienViewModel : BaseViewModel
	{
		private Timer debounceTimer;
		private string searchText;

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

		public int Count => PhieuThuTienCollection?.Count ?? 0;

		public ICommand FirstLoad { get; }
		public ICommand ThemPhieuThuTien { get; }
		public ICommand TimKiemPhieuThuTien { get; }

		public PhieuThuTienViewModel()
		{
			FirstLoad = new RelayCommand<object>(_ => true, async _ =>
			{
				PhieuThuTienCollection = new ObservableCollection<BillDTO>(await BillService.Ins.GetAllBill());
			});

			TimKiemPhieuThuTien = new RelayCommand<object>(_ => true, p =>
			{
				// Debounce search
				searchText = ((TextBox)p).Text;
				debounceTimer?.Dispose();

				debounceTimer = new Timer
				{
					Interval = 500,
					AutoReset = false
				};
				debounceTimer.Elapsed += PerformSearch;
			});

			ThemPhieuThuTien = new RelayCommand<object>(_ => true, _ =>
			{
				// Add new bill
			});
		}

		private void PerformSearch(object sender, ElapsedEventArgs e)
		{
			// Search here
		}
	}
}
