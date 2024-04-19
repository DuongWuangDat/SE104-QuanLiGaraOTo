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




        ICommand InitRevenue {  get; }

        public BaoCaoTonKhoViewModel()
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
            });
        }

    }
}
