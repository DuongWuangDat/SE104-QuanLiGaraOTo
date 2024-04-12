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

namespace QuanLiGaraOto.ViewModel.BaoCaoVM
{
    internal class BaoCaoViewModel : BaseViewModel
    {
        private int _id;
        private int _month;
        private int _year;
        private decimal _totalPrice;

        public int ID { get { return _id; } set {  _id = value; } }

        public int Month { get { return _month; } set { _month = value; } }
        public int Year { get { return _year; } set { _year = value; } }
        public decimal TotalPrice { get { return _totalPrice; } set { _totalPrice = value; } }

        

    }
}
