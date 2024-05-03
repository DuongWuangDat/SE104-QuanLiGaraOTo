﻿using QuanLiGaraOto.DTOs;
using QuanLiGaraOto.Model;
using QuanLiGaraOto.Model.service;
using QuanLiGaraOto.View.SuaChuaXe;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MaterialDesignThemes.Wpf;
using System;
using QuanLiGaraOto.View.MessageBox;

namespace QuanLiGaraOto.ViewModel.SuaChuaXeVM
{
    internal class SuaChuaXeViewModel : BaseViewModel
    {
        private ObservableCollection<RepairDetailDTO> noiDungSuaChuaCollection;
        public ObservableCollection<RepairDetailDTO> NoiDungSuaChuaCollection
        {
            get { return noiDungSuaChuaCollection; }
            set { noiDungSuaChuaCollection = value; OnPropertyChanged(); }
        }

        //-------------Them vat cong------------------------//
        private string _wageName;

        public string WageName
        {
            get { return _wageName; }
            set { _wageName = value; }
        }

        private string _price;

        public string Price
        {
            get { return _price; }
            set { _price = value; }
        }

        private ObservableCollection<WageDTO> _wageCollection;

        public ObservableCollection<WageDTO> wageCollection
        {
            get { return _wageCollection; }
            set { _wageCollection = value; OnPropertyChanged(); }
        }

        private WageDTO _selectedWage;

        public WageDTO SelectedWage
        {
            get { return _selectedWage; }
            set { _selectedWage = value; }
        }



        //--------------------------------------------------//

        private string _content;
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        private string _licensePlate;

        public string LicensePlate
        {
            get { return _licensePlate; }
            set { _licensePlate = value; }
        }

        private string _wage;

        public string Wage
        {
            get { return _wage; }
            set { _wage = value; }
        }

        private DateTime ngaySuaChua;

        public DateTime NgaySuaChua
        {
            get { return ngaySuaChua; }
            set { ngaySuaChua = value; OnPropertyChanged(); }
        }

        public ICommand AddWage { get; set; }

        public ICommand DeleteWage { get; set; }
        public ICommand wageClose { get; set; }
        public ICommand FirstLoadRepairCar {  get; set; }

        public ICommand QuanLiVatTuPhuTungOpen { get; set; }

        public ICommand QuanLiTienCongOpen { get; set; }

        public ICommand ThemLoaiVatTuOpen { get; set; }

        public ICommand ThemNoiDungSuaChuaOpen { get; set; }

        public ICommand NhapVatTuPhuTungOpen { get; set; }

        public ICommand CancelAddingSupplies { get; set; }

        public ICommand XongClose { get; set; }

        public ICommand ThemNoiDungVaoBang { get; set; }

        // public ICommand HoanTatClose { get; set; }

        public SuaChuaXeViewModel()
        {
            FirstLoadRepairCar = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                wageCollection = new ObservableCollection<WageDTO>(await WageService.Ins.GetAllWage());
            });

            AddWage = new RelayCommand<object>(p => { return true; }, async (p) =>
            {
                WageDTO newWage = new WageDTO
                {
                    Name = WageName,
                    Price = decimal.Parse(Price)
                };
                var (isSucess, msg) = await WageService.Ins.AddNewWage(newWage);
                if (isSucess)
                {
                    wageCollection.Add(newWage);
                    MessageBoxCustom.Show(MessageBoxCustom.Success, "Thêm thành công");
                }
                else
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Khong them duoc du lieu");
                }
            });

            DeleteWage = new RelayCommand<object>(p => { return true; }, async (p) =>
            {
                var (isSuccess, msg) = await WageService.Ins.DeleteWage(SelectedWage.ID);
                if(isSuccess)
                {
                    wageCollection.Remove(SelectedWage);
                    MessageBoxCustom.Show(MessageBoxCustom.Success, "Xóa thành công");
                }
                else
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Khong xoa duoc du lieu");
                }
            });

            wageClose = new RelayCommand<Window>(p => { return true; }, (p) =>
            {
                WageName = null;
                Price = null;
                p.Close();
            });

            QuanLiTienCongOpen = new RelayCommand<object>(_ => true, async _ =>
            {
                Window mainWindow = Application.Current.MainWindow; 
                mainWindow.Opacity = 0.5; 

                Window dialogWindow = new QuanLiTienCong();
                dialogWindow.ShowDialog(); // Hiển thị cửa sổ dialog

                mainWindow.Opacity = 1.0;
            });

            QuanLiVatTuPhuTungOpen = new RelayCommand<object>(_ => true, async _ =>
            {
                Window mainWindow = Application.Current.MainWindow; // Lấy cửa sổ chính
                mainWindow.Opacity = 0.5; // Làm mờ cửa sổ chính

                Window dialogWindow = new QuanLiVatTuPhuTung();
                dialogWindow.ShowDialog(); // Hiển thị cửa sổ dialog

                mainWindow.Opacity = 1.0;
            });

            ThemLoaiVatTuOpen = new RelayCommand<object>(_ => true, async _ =>
            {
                Window mainWindow = Application.Current.MainWindow; // Lấy cửa sổ chính
                mainWindow.Opacity = 0.5; // Làm mờ cửa sổ chính

                Window dialogWindow = new ThemLoaiVatTu();
                dialogWindow.ShowDialog(); // Hiển thị cửa sổ dialog
            });

            ThemNoiDungSuaChuaOpen = new RelayCommand<object>(_ => true, async _ =>
            {
                Window mainWindow = Application.Current.MainWindow; // Lấy cửa sổ chính
                mainWindow.Opacity = 0.5; // Làm mờ cửa sổ chính

                Window dialogWindow = new ThemNoiDungSuaChua();
                dialogWindow.ShowDialog(); // Hiển thị cửa sổ dialog
            });

            NhapVatTuPhuTungOpen = new RelayCommand<object>(_ => true, async _ =>
            {
                Window mainWindow = Application.Current.MainWindow; // Lấy cửa sổ chính
                mainWindow.Opacity = 0.5; // Làm mờ cửa sổ chính

                Window dialogWindow = new NhapVatTuPhuTung();
                dialogWindow.ShowDialog(); // Hiển thị cửa sổ dialog
            });

            XongClose = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            // HoanTatClose = new RelayCommand<Window>((p) => { return true; }, (p) =>
            // {
                // p.Close();
            // });

            CancelAddingSupplies = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
        }
    }
}
