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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;

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


        private UserControl _currentUserControl;
        public UserControl CurrentUserControl
        {
            get => _currentUserControl;
            set => SetProperty(ref _currentUserControl, value);
        }

        //---------------------------------------------------
        ICommand GetMonthAvailable {  get; set; }
        ICommand GetYearAvailable { get; set; }
        ICommand OpenBaoCaoTonKho {  get; set; }
        ICommand OpenBaoCaoDoanhThu { get; set; }
        
        public BaoCaoViewModel() { 
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
                var curDate = DateTime.Now;
                var curYear = curDate.Year;
                var years = Enumerable.Range(2000,curYear).ToList();

                YearList = years;
            });

            GetMonthAvailable = new RelayCommand<object>(_ => true, (param) =>
            {
                var curDate = DateTime.Now;
                var curMonth = curDate.Month;
                var curYear = curDate.Year;
                if(Year == curYear)
                {
                    var months = Enumerable.Range(1, curMonth).ToList();
                    MonthList = months;
                } else
                {
                    var months = Enumerable.Range(1, 12).ToList();
                    MonthList = months;
                }
               
            });

        }
        

    }
}
