using MaterialDesignThemes.Wpf;
using QuanLiGaraOto.DTOs;
using QuanLiGaraOto.Model;
using QuanLiGaraOto.Model.service;
using QuanLiGaraOto.Utils;
using QuanLiGaraOto.View.BaoCao;
using QuanLiGaraOto.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace QuanLiGaraOto.ViewModel.BaoCaoVM
{
    internal class BaoCaoDoanhThuViewModel : BaseViewModel
    {
        private RevenueDTO _revenue;
        public RevenueDTO _Revenue
        {
            get { return _revenue; }
            set { _revenue = value; }
        }

        private Revenue currevenue;
        public Revenue CurRevenue
        {
            get { return currevenue; }
            set { currevenue = value; }
        }

        private ObservableCollection<RevenueDetailDTO> _revenueList;
        public ObservableCollection<RevenueDetailDTO> RevenueList
        {
            get { return _revenueList; }
            set { _revenueList = value;}
        }
        private int _month;
        public int Month
        {
            get { return _month; }
            set { SetProperty(ref _month, value); OnPropertyChanged(); }
        }

        private int _year;
        public int Year
        {
            get { return _year; }
            set { SetProperty(ref _year, value); OnPropertyChanged(); }
        }


        ICommand InitRevenue {  get; }
        ICommand GetCurrentRevenueReport {  get; }
        ICommand GetRevenue { get; }
        public BaoCaoDoanhThuViewModel()
        {
            InitRevenue = new RelayCommand<object>(_ => true, async _ =>
            {
                var(issuccess, message)= await RevenueService.Ins.InitRevenue();
                if (issuccess)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Success, message);
                } else
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, message);
                }
                await LoadCurrentRevenueReport();
            });

           
            GetCurrentRevenueReport = new RelayCommand<object>(_ => true, async _ =>
            {
                CurRevenue = await RevenueService.Ins.GetCurrentRevenueReport();
                await LoadCurrentRevenueReport();
            });

            GetRevenue = new RelayCommand<object>(_ => true, async _ =>
            {
                var curDate = DateTime.Now;
                var curMonth = curDate.Month;
                var curYear = curDate.Year;
                while (Month > curMonth && Year > curYear)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Tháng và năm lớn hơn hiện tại, vui lòng nhập lại.");
                }
                while (Month < curMonth && Year == curYear)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Tháng lớn hơn hiện tại, vui lòng nhập lại.");
                }

                _Revenue = await RevenueService.Ins.GetRevenue(Month, Year);
                await LoadCurrentRevenueReport();
            });


        }
        private async Task LoadCurrentRevenueReport()
        {

            if (_Revenue != null)
            {
                RevenueList = new ObservableCollection<RevenueDetailDTO>(_Revenue.RevenueDetails);
            }
        }

    }
}
