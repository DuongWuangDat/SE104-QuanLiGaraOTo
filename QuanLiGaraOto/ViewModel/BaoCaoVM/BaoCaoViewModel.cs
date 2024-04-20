using MaterialDesignThemes.Wpf;
using QuanLiGaraOto.DTOs;
using QuanLiGaraOto.Model;
using QuanLiGaraOto.Model.service;
using QuanLiGaraOto.Utils;
using QuanLiGaraOto.View.BaoCao;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using QuanLiGaraOto.View.MessageBox;

namespace QuanLiGaraOto.ViewModel.BaoCaoVM
{
    internal class BaoCaoViewModel : BaseViewModel
    {
        private int _id;
        private int _month;
        private int _year;


        public int ID { get { return _id; } set {  _id = value; } }

        public int Month { get { return _month; } set { _month = value; OnPropertyChanged(); } }
        public int Year { get { return _year; } set { _year = value; OnPropertyChanged(); } }

        private List<int> _monthList;
        public List<int> MonthList
        {
            get { return _monthList; }
            set { _monthList = value; OnPropertyChanged(); }
        }
        private List<int> _yearList;
        public List<int> YearList
        {
            get { return _yearList; }
            set { _yearList = value; OnPropertyChanged(); }
        }

        // Inventory
        private InventoryReportDTO _currentInventoryReport;
        public InventoryReportDTO CurrentInventoryReport
        {
            get => _currentInventoryReport;
            set => SetProperty(ref _currentInventoryReport, value);
        }

        private ObservableCollection<InventoryReportDetailDTO> _inventoryDetails;
        public ObservableCollection<InventoryReportDetailDTO> InventoryDetails
        {
            get => _inventoryDetails;
            set => SetProperty(ref _inventoryDetails, value);
        }

        private UserControl _currentUserControl;
        public UserControl CurrentUserControl
        {
            get => _currentUserControl;
            set => SetProperty(ref _currentUserControl, value);
        }
        // InventoryCommand---------------------------

        public ICommand GetInventoryReport { get; set; }

        // Revenue-----------------------------
        private RevenueDTO _revenue;
        public RevenueDTO RevenueReport
        {
            get { return _revenue; }
            set { _revenue = value; }
        }

        private ObservableCollection<RevenueDetailDTO> _revenueList;
        public ObservableCollection<RevenueDetailDTO> RevenueList
        {
            get { return _revenueList; }
            set { _revenueList = value; }
        }

        // Revenue Command
        public ICommand GetRevenue { get; set; }

        //---------------------------------------------------
        public ICommand GetMonthAvailable {  get; set; }
        public ICommand GetYearAvailable { get; set; }
        public ICommand FirstLoad { get; set; }
        public ICommand OpenBaoCaoTonKho {  get; set; }
        public ICommand OpenBaoCaoDoanhThu { get; set; }
        
        public BaoCaoViewModel() { 
            // PageCommand
            FirstLoad = new RelayCommand<object>(_=> true, _ =>
            {
                var curDate = DateTime.Now;
                var curYear = curDate.Year;
                var curMonth = curDate.Month;
                Year = curYear;
                Month = curMonth;
            });
            OpenBaoCaoTonKho = new RelayCommand<object>(_ => true, (param) =>
            {
                var tonKho = new TonKho();
                
                CurrentUserControl = tonKho;

            });
            OpenBaoCaoDoanhThu = new RelayCommand<object>(_ => true, (param) =>
            {
                var doanhthu = new DoanhThu();
                CurrentUserControl = doanhthu;
            });

            GetYearAvailable = new RelayCommand<object>(_ => true, (param) =>
            {           
                ComboBox comboBox = param as ComboBox;
                var curDate = DateTime.Now;
                var curYear = curDate.Year;
                var years = Enumerable.Range(2000,(curYear-2000+1)).ToList();
                YearList = years;
                comboBox.Items.Add(years);
            });

            GetMonthAvailable = new RelayCommand<object>(_ => true, (param) =>
            {
                ComboBox combobox = param as ComboBox;
                var curDate = DateTime.Now;
                var curMonth = curDate.Month;
                var curYear = curDate.Year;
                if(Year == curYear)
                {
                    var months = Enumerable.Range(1, curMonth).ToList();
                    MonthList = months;
                    combobox.ItemsSource = months;
                } else
                {
                    var months = Enumerable.Range(1, 12).ToList();
                    MonthList = months;
                    combobox.ItemsSource = months;
                }
               
            });

            // InventoryCommand

            GetInventoryReport = new RelayCommand<object>(_ => true, async _ =>
            {
                var curDate = DateTime.Now;
                var curMonth = curDate.Month;
                var curYear = curDate.Year;
                if ( Year > curYear)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Năm lớn hơn hiện tại, vui lòng nhập lại.");
                }
                else if (Month > curMonth && Year == curYear)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Tháng lớn hơn hiện tại, vui lòng nhập lại.");
                }

                CurrentInventoryReport = await InvetoryReportService.Ins.GetInventoryReport(this.Month, this.Year);
                if (CurrentInventoryReport != null)
                {
                    InventoryDetails = new ObservableCollection<InventoryReportDetailDTO>(CurrentInventoryReport.InventoryReportDetails);
                }
            });

            // Revenue Command

            GetRevenue = new RelayCommand<object>(_ => true, async _ =>
            {
                var curDate = DateTime.Now;
                var curMonth = curDate.Month;
                var curYear = curDate.Year;
                if (Year > curYear)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Năm lớn hơn hiện tại, vui lòng nhập lại.");
                }
                else if (Month > curMonth && Year == curYear)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Tháng lớn hơn hiện tại, vui lòng nhập lại.");
                }

                RevenueReport = await RevenueService.Ins.GetRevenue(this.Month, this.Year);
                if (RevenueReport != null)
                {
                    RevenueList = new ObservableCollection<RevenueDetailDTO>(RevenueReport.RevenueDetails);
                }
            });

        }
        

    }
}
