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

        public int Month { get { return _month; } set { _month = value; } }
        public int Year { get { return _year; } set { _year = value; } }

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


        }
        

    }
}
