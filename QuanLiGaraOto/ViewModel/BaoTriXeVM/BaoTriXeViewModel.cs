using MaterialDesignThemes.Wpf;
using QuanLiGaraOto.DTOs;
using QuanLiGaraOto.Model;
using QuanLiGaraOto.Model.service;
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
            set { _receptionCount = value; }
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
            set { _cusName = value; }
        }

        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        private string _licensePlate;

        public string LicensePlate
        {
            get { return _licensePlate; }
            set { _licensePlate = value; }
        }

        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; }
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

        public BaoTriXeViewModel()
        {
            OpenManageBrandWD = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                QuanLiHieuXeWD quanLiHieuXeWD = new QuanLiHieuXeWD();
                quanLiHieuXeWD.ShowDialog();
            });

            FirstLoadBrandCar = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                BrandCarList = new ObservableCollection<BrandCarDTO>(await BrandCarService.Ins.GetListBrandCar());
            });

            AddBrandCar = new RelayCommand<TextBox>((p) => { return true; }, async (p) =>
            {
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
                    MessageBoxCustom.Show(MessageBoxCustom.Success, "Thêm dữ liệu thành công");
                }
                else
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Khong the them du lieu");
                }
            });
        }

    }
}
