using MaterialDesignThemes.Wpf;
using QuanLiGaraOto.DTOs;
using QuanLiGaraOto.Model;
using QuanLiGaraOto.Model.service;
using QuanLiGaraOto.Utils;
using QuanLiGaraOto.View.CaiDatQD;
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

namespace QuanLiGaraOto.ViewModel.CaiDatQuyDinhVM
{
    internal class CaiDatQuyDinhViewModel : BaseViewModel
    {
        private ParameterDTO tiLeTinhDonGiaBan = new ParameterDTO();
        public ParameterDTO TiLeTinhDonGiaBan
        {
            get { return tiLeTinhDonGiaBan; }
            set { tiLeTinhDonGiaBan = value; OnPropertyChanged();  }
        }
        private ParameterDTO apDungQÐKiemTraSoTienThu = new ParameterDTO();
        public ParameterDTO ApDungQÐKiemTraSoTienThu
        {
            get { return apDungQÐKiemTraSoTienThu; }
            set { apDungQÐKiemTraSoTienThu = value; OnPropertyChanged(); }
        }
        private ObservableCollection<int> checklist;
        public ObservableCollection<int> CheckList
        {
            get { return checklist; }
            set { checklist = value; OnPropertyChanged(); }
        }

        private int check;
        public int Check
        {
            get { return check; }
            set { check = value; OnPropertyChanged(); }
        }

        private ParameterDTO soXeSuaChuaToiDa = new ParameterDTO();
        public ParameterDTO SoXeSuaChuaToiDa
        {
            get { return soXeSuaChuaToiDa; }
            set { soXeSuaChuaToiDa = value; OnPropertyChanged(); }
        }
        private List<ParameterDTO> parameterDTOsCollection;
        public List<ParameterDTO> ParameterDTOsCollection
        {
            get { return parameterDTOsCollection; }
            set { parameterDTOsCollection = value; }
        }

        //-----------------
      
        public ICommand ApDungPhat {  get; set; }
        public ICommand UpdateParameter { get; set; }

        public ICommand FirstLoad { get; set; }

        public CaiDatQuyDinhViewModel()
        {

            FirstLoad = new RelayCommand<object>(_ => true, async _ =>
            {
                CheckList = new ObservableCollection<int> { 0, 1 };

                TiLeTinhDonGiaBan.Value = await ParameterService.Ins.GetRatio();

                SoXeSuaChuaToiDa.Value = await ParameterService.Ins.SoXeSuaChuaTrongNgay();

                var IsCheck = await ParameterService.Ins.ApDungPhat();
                if (IsCheck)
                {
                    Check = 1;
                    ApDungQÐKiemTraSoTienThu.Value = 1;
                }
                else
                {
                    Check = 0;
                    ApDungQÐKiemTraSoTienThu.Value = 0;
                }
                

            });


            UpdateParameter = new RelayCommand<object>(_ => true, async (p) =>
            {
                string resultmessage = "";
                // SoXeSuaChuaToiDa
                //var cultureInfo = CultureInfo.InvariantCulture;
                var (xesuccess, xemessage) = await ParameterService.Ins.UpdateParameter("SoXeSuaChuaToiDa", (float)(SoXeSuaChuaToiDa.Value));
                if (xesuccess)
                {
                    resultmessage += "Cập nhật số xe sửa chữa tối đa: "+ xemessage + "\n";
                }
                else
                {
                    resultmessage += "Cập nhật số xe sửa chữa tối đa: " + xemessage + "\n";
                }

                // TiLeDonGiaBan
                var (tilesuccess, tilemessage) = await ParameterService.Ins.UpdateParameter("TiLeTinhDonGiaBan", (float)(TiLeTinhDonGiaBan.Value));
                if (tilesuccess)
                {
                    resultmessage += "Cập nhật tỉ lệ đơn giá bán: " + tilemessage + "\n";
                }
                else
                {
                    resultmessage += "Cập nhật tỉ lệ đơn giá bán: " + tilemessage + "\n";
                }

                // ApDungQuyDinhKiemTraSoTienThu
                var (success, message) = await ParameterService.Ins.UpdateParameter("ApDungQÐKiemTraSoTienThu", (float)(ApDungQÐKiemTraSoTienThu.Value));
                if(success)
                {
                    resultmessage += "Áp dụng quy định kiểm tra tiền thu: " + message ;
                }
                else
                {
                    resultmessage += "Áp dụng quy định kiểm tra tiền thu: " + message ;
                }

                if(success && tilesuccess && xesuccess)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Success, resultmessage);
                }
                else
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, resultmessage);
                }
            });


        }
    }
}
