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
        private ParameterDTO tiLeTinhDonGiaBan;
        public ParameterDTO TiLeTinhDonGiaBan
        {
            get { return tiLeTinhDonGiaBan; }
            set { tiLeTinhDonGiaBan = value;  }
        }
        private ParameterDTO apDungQÐKiemTraSoTienThu;
        public ParameterDTO ApDungQÐKiemTraSoTienThu
        {
            get { return apDungQÐKiemTraSoTienThu; }
            set { apDungQÐKiemTraSoTienThu = value; }
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

        private ParameterDTO soXeSuaChuaToiDa;
        public ParameterDTO SoXeSuaChuaToiDa
        {
            get { return soXeSuaChuaToiDa; }
            set { soXeSuaChuaToiDa = value; }
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

                TiLeTinhDonGiaBan = new ParameterDTO();
                TiLeTinhDonGiaBan.Name = "TiLeTinhDonGiaBan";
                TiLeTinhDonGiaBan.Value = await ParameterService.Ins.GetRatio();

                SoXeSuaChuaToiDa = new ParameterDTO();
                soXeSuaChuaToiDa.Name = "SoXeSuaChuaToiDa";
                SoXeSuaChuaToiDa.Value = await ParameterService.Ins.SoXeSuaChuaTrongNgay();

                ApDungQÐKiemTraSoTienThu = new ParameterDTO();
                ApDungQÐKiemTraSoTienThu.Name = "ApDungQÐKiemTraSoTienThu";

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
                if (p is TextBox parameter)
                {
                  
                    
                        var cultureInfo = CultureInfo.InvariantCulture;
                        if (double.TryParse(parameter.Text, NumberStyles.Float, cultureInfo,out var number))
                        {
                            var (success, message) = await ParameterService.Ins.UpdateParameter(parameter.Name, (float)double.Parse(parameter.Text,cultureInfo));
                            if (success)
                            {
                                MessageBoxCustom.Show(MessageBoxCustom.Success, message);
                            }
                            else
                            {
                                MessageBoxCustom.Show(MessageBoxCustom.Error, message);
                            }
                        }

                    
                       

                }
                if(p is ComboBox comboBox)
                {
                    var ischeck = comboBox.SelectedItem;
                    if ((int)ischeck == 1)
                    {
                        Check = 1;
                        ApDungQÐKiemTraSoTienThu.Value = 1;
                    }
                    else
                    {
                        Check = 0;
                        ApDungQÐKiemTraSoTienThu.Value = 0;
                    }
                }
                TiLeTinhDonGiaBan.Value = await ParameterService.Ins.GetRatio();
                SoXeSuaChuaToiDa.Value = await ParameterService.Ins.SoXeSuaChuaTrongNgay();
            });


        }
    }
}
