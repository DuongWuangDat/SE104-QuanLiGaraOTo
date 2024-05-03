using MaterialDesignThemes.Wpf;
using QuanLiGaraOto.DTOs;
using QuanLiGaraOto.Model;
using QuanLiGaraOto.Model.service;
using QuanLiGaraOto.Utils;
using QuanLiGaraOto.View.BaoTriXePage;
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

namespace QuanLiGaraOto.ViewModel.BaoTriXeVM
{
    public class BaoTriXeViewModel : BaseViewModel
    {
        private int _receptionCount;

        public int ReceptionCount
        {
            get { return _receptionCount; }
            set { _receptionCount = value; OnPropertyChanged(); }
        }

        private ObservableCollection<BrandCarDTO> _brandCarList;

        public ObservableCollection<BrandCarDTO> BrandCarList
        {
            get { return _brandCarList; }
            set { _brandCarList = value; OnPropertyChanged(); }
        }


        private BrandCarDTO _selectedBrand;

        public BrandCarDTO SelectedBrand
        {
            get { return _selectedBrand; }
            set { _selectedBrand = value; OnPropertyChanged(); }
        }
        //Add reception

        private BrandCarDTO _selectedBrandItem;

        public BrandCarDTO SelectedBrandItem
        {
            get { return _selectedBrandItem; }
            set { _selectedBrandItem = value; OnPropertyChanged(); }
        }


        private string _cusName;

        public string CusName
        {
            get { return _cusName; }
            set { _cusName = value; OnPropertyChanged(); }
        }

        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; OnPropertyChanged(); }
        }

        private string _licensePlate;

        public string LicensePlate
        {
            get { return _licensePlate; }
            set { _licensePlate = value; OnPropertyChanged(); }
        }

        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(); }
        }



        private DateTime selectedDate = DateTime.Now;

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set { selectedDate = value; OnPropertyChanged(); }
        }


        //----------------------------------
        public ICommand FirstLoadBrandCar { get; set; }

        public ICommand AddBrandCar { get; set; }

        public ICommand CancelBrandCar { get; set; }

        public ICommand DeleteBrandCar { get; set; }
        public ICommand OpenManageBrandWD { get; set; }

        public ICommand AddReceptionCM { get; set; }
        public ICommand ResetReceptionCM { get; set; }
        public BaoTriXeViewModel()
        {
            OpenManageBrandWD = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                QuanLiHieuXeWD quanLiHieuXeWD = new QuanLiHieuXeWD();
                quanLiHieuXeWD.ShowDialog();
            });

            FirstLoadBrandCar = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                ReceptionCount = await ReceptionService.Ins.CountByDate(DateTime.Now);
                Console.WriteLine(ReceptionCount);
                BrandCarList = new ObservableCollection<BrandCarDTO>(await BrandCarService.Ins.GetListBrandCar());
            });

            AddBrandCar = new RelayCommand<TextBox>((p) => { return true; }, async (p) =>
            {
                if(p.Text == null || p.Text == "")
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Vui lòng nhập tên hãng xe");
                    return;
                }
                BrandCarDTO newBrand = new BrandCarDTO
                {
                    Name = p.Text
                };
                var (isSuccess, message) = await BrandCarService.Ins.AddNewBrandCar(newBrand);
                if (isSuccess)
                {
                    BrandCarList.Add(newBrand);
                    p.Text = "";
                    MessageBoxCustom.Show(MessageBoxCustom.Success, "Thêm dữ liệu thành công");
                }
                else
                {
                    p.Text = "";
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Khong the them du lieu");
                }
            });



            CancelBrandCar = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            DeleteBrandCar = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                var (isSuccess, message) = await BrandCarService.Ins.DeleteBrandCar(SelectedBrand.ID);
                if (isSuccess)
                {
                    BrandCarList = new ObservableCollection<BrandCarDTO>(await BrandCarService.Ins.GetListBrandCar());
                    MessageBoxCustom.Show(MessageBoxCustom.Success, "Xóa dữ liệu thành công");
                }
                else
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Khong the xoa du lieu");
                }
            });

            AddReceptionCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (LicensePlate == null || CusName == null || PhoneNumber == null || Address == null || LicensePlate == ""|| CusName == ""|| PhoneNumber == "" || Address == "" || SelectedBrandItem == null)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Vui lòng nhập đầy đủ thông tin");
                    return;
                }
                if(Helper.IsPhoneNumber(PhoneNumber) == false)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Số điện thoại không hợp lệ");
                    return;
                }
                ReceptionDTO recpt = new ReceptionDTO
                {
                    LicensePlate = LicensePlate,
                    CreatedAt = SelectedDate,
                    Customer = new CustomerDTO
                    {
                        Name = CusName,
                        PhoneNumber = PhoneNumber,
                        Address = Address
                    },
                    BrandCar = SelectedBrandItem,
                    Debt = 0

                };
                var (isSuccess, message) = await ReceptionService.Ins.AddNewReception(recpt);
                if (isSuccess)
                {
                    ReceptionCount = await ReceptionService.Ins.CountByDate(DateTime.Now);
                    MessageBoxCustom.Show(MessageBoxCustom.Success, "Thêm dữ liệu thành công");
                }
                else
                {
                    if(message == "Number of car in a day is over the limit!")
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Số lượng xe sửa chữa trong ngày đã vượt quá giới hạn");
                    }
                    else
                    {
                        MessageBoxCustom.Show(MessageBoxCustom.Error, "Khong the them du lieu");
                    }
                    
                }
            });

            ResetReceptionCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LicensePlate = "";
                CusName = "";
                PhoneNumber = "";
                Address = "";
                SelectedBrandItem = null;
            });
        }

    }
}
