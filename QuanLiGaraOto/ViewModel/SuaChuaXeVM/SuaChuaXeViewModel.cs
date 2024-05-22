using QuanLiGaraOto.DTOs;
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


        //-------------Them vat tu------------------------//

        private string _supplyName;

        public string SupplyName
        {
            get { return _supplyName; }
            set { _supplyName = value; }
        }

        private ObservableCollection<SupplyDTO> _supplyColection;

        public ObservableCollection<SupplyDTO> supplyColection
        {
            get { return _supplyColection; }
            set { _supplyColection = value; OnPropertyChanged(); }
        }

        private SupplyDTO _selectedSupply;

        public SupplyDTO SelectedSupply
        {
            get { return _selectedSupply; }
            set { _selectedSupply = value; }
        }

        private ObservableCollection<SuppliesInputDetailDTO> _spDetailCollection;

        public ObservableCollection<SuppliesInputDetailDTO> spDetailCollection
        {
            get { return _spDetailCollection; }
            set { _spDetailCollection = value; OnPropertyChanged(); }
        }

        private string _count;

        public string Count
        {
            get { return _count; }
            set { _count = value;  OnPropertyChanged(); }
        }

        private string _priceSp;

        public string PriceSp
        {
            get { return _priceSp; }
            set { _priceSp = value; OnPropertyChanged(); }
        }

        private SupplyDTO _selectedSp;

        public SupplyDTO SelectedSp
        {
            get { return _selectedSp; }
            set { _selectedSp = value; OnPropertyChanged(); }
        }

        public SuppliesInputDetailDTO SelectedSpDetail { get; set; }

        //--------------------------------------------------//
        //-------------Them phieu sua chua------------------------//

        private WageDTO _wageSelected;

        public WageDTO wageSelected
        {
            get { return _wageSelected; }
            set { _wageSelected = value; OnPropertyChanged(); }
        }
        private ObservableCollection<RepairDetailDTO> _rpdtCollection;

        public ObservableCollection<RepairDetailDTO> rpdtCollection
        {
            get { return _rpdtCollection; }
            set { _rpdtCollection = value; OnPropertyChanged(); }
        }

        private RepairDetailDTO _selectedRpdt;

        public RepairDetailDTO SelectedRpdt
        {
            get { return _selectedRpdt; }
            set { _selectedRpdt = value; OnPropertyChanged(); }
        }

        //-------------Them phieu supply tung noi dung------------------------//

        private string _spWage;

        public string SpWage
        {
            get { return _spWage; }
            set { _spWage = value; }
        }

        private string _spContent;

        public string SpContent
        {
            get { return _spContent; }
            set { _spContent = value; }
        }

        private ObservableCollection<RepairSuppliesDetailDTO> _repairSuppliesDtCollection;

        public ObservableCollection<RepairSuppliesDetailDTO> RpSpDtCollection
        {
            get { return _repairSuppliesDtCollection; }
            set { _repairSuppliesDtCollection = value; OnPropertyChanged(); }
        }

        private string _countSupply;

        public string CountSupply
        {
            get { return _countSupply; }
            set { _countSupply = value;OnPropertyChanged(); }
        }

        private SupplyDTO _selectedSupplyDt;

        public SupplyDTO SelectedSupplyDt
        {
            get { return _selectedSupplyDt; }
            set { _selectedSupplyDt = value;OnPropertyChanged(); }
        }

        private RepairSuppliesDetailDTO _selectedRpSpDt;

        public RepairSuppliesDetailDTO SelectedSpRpDt
        {
            get { return _selectedRpSpDt; }
            set { _selectedRpSpDt = value; }
        }


        //--------------------------------------------------//

        private string _content;
        public string Content
        {
            get { return _content; }
            set { _content = value; OnPropertyChanged(); }
        }

        private string _licensePlate;

        public string LicensePlate
        {
            get { return _licensePlate; }
            set { _licensePlate = value; OnPropertyChanged(); }
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
        public ICommand AddSuppliesName { get; set; }
        public ICommand DeleteSupplies { get; set; }
        public ICommand FirstLoadRepairCar {  get; set; }
        public ICommand AddSpDetail { get; set; }
        public ICommand DeleteSpDetail { get; set; }
        public ICommand AddSpInput { get; set; }
        public ICommand QuanLiVatTuPhuTungOpen { get; set; }

        public ICommand QuanLiTienCongOpen { get; set; }

        public ICommand ThemLoaiVatTuOpen { get; set; }

        public ICommand ThemNoiDungSuaChuaOpen { get; set; }

        public ICommand NhapVatTuPhuTungOpen { get; set; }

        public ICommand CancelAddingSupplies { get; set; }

        public ICommand XongClose { get; set; }

        public ICommand AddContent { get; set; }

        public ICommand FirstLoadSupplyInput { get; set; }

        public ICommand NhapVatTuPhuTungClose { get; set; }

        public ICommand DeleteRpdt { get; set; }
        public ICommand OpenAddSupplyRepair { get; set; }
        public ICommand AddRpSpDt { get; set; }
        public ICommand AddRecordRpSpDt { get; set; }
        public ICommand OnCloseRpSpDtWd { get; set; }
        public ICommand ResetRepair { get; set; }
        public ICommand AddRepair { get; set; }
        public ICommand DeleteRpSpDt { get; set; }
        // public ICommand HoanTatClose { get; set; }

        public SuaChuaXeViewModel()
        {
            FirstLoadRepairCar = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                wageCollection = new ObservableCollection<WageDTO>(await WageService.Ins.GetAllWage());
                supplyColection = new ObservableCollection<SupplyDTO>(await SuppliesService.Ins.GetAllSupply());
                rpdtCollection = new ObservableCollection<RepairDetailDTO>();
                NgaySuaChua = DateTime.Now;
                Console.WriteLine(supplyColection.ToArray().Length);
            });

            FirstLoadSupplyInput = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                spDetailCollection = new ObservableCollection<SuppliesInputDetailDTO>();
            });

            AddWage = new RelayCommand<object>(p => { return true; }, async (p) =>
            {
                if (WageName == null || WageName == "" || Price == null || Price == "")
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Vui lòng nhập đủ thông tin"); return;
                }
                if (decimal.TryParse(Price, out decimal price) == false)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Vui lòng nhập đúng định dạng"); return;
                }
                WageDTO newWage = new WageDTO
                {
                    Name = WageName,
                    Price = decimal.Parse(Price)
                };
                var (isSucess, msg) = await WageService.Ins.AddNewWage(newWage);
                if (isSucess)
                {
                    wageCollection = new ObservableCollection<WageDTO>(await WageService.Ins.GetAllWage());
                    MessageBoxCustom.Show(MessageBoxCustom.Success, "Thêm thành công");
                }
                else
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Khong them duoc du lieu");
                }
            });

            DeleteWage = new RelayCommand<object>(p => { return true; }, async (p) =>
            {
                Window wd = new DeleteMessageBox();
                wd.ShowDialog();
                if (wd.DialogResult == false)
                {
                    return;
                }
                var (isSuccess, msg) = await WageService.Ins.DeleteWage(SelectedWage.ID);
                if (isSuccess)
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

            AddSuppliesName = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (SupplyName == null || SupplyName == "")
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Vui lòng nhập tên vật tư");
                }
                SupplyDTO newSupply = new SupplyDTO
                {
                    Name = SupplyName,
                    CountInStock = 0,
                    InputPrices = 0,
                    OutputPrices = 0
                };
                var (isSuccess, msg) = await SuppliesService.Ins.AddNewSupply(newSupply);
                if (isSuccess)
                {
                    SupplyName = null;
                    supplyColection = new ObservableCollection<SupplyDTO>(await SuppliesService.Ins.GetAllSupply());
                    MessageBoxCustom.Show(MessageBoxCustom.Success, "Thêm thành công");
                }
                else
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Khong them duoc du lieu");
                }
            });

            DeleteSupplies = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                Window wd = new DeleteMessageBox();
                wd.ShowDialog();
                if (wd.DialogResult == false)
                {
                    return;
                }
                var (isSuccess, msg) = await SuppliesService.Ins.DeleteSupply(SelectedSupply.ID);
                if (isSuccess)
                {
                    supplyColection.Remove(SelectedSupply);
                    MessageBoxCustom.Show(MessageBoxCustom.Success, "Xóa thành công");
                }
                else
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Khong xoa duoc du lieu");
                }
            });

            AddSpDetail = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedSp == null || Count == null || PriceSp == null)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Vui lòng nhập đủ thông tin"); return;
                }
                if (int.TryParse(Count, out int count) == false || decimal.TryParse(PriceSp, out decimal price) == false)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Vui lòng nhập đúng định dạng"); return;
                }
                var newSuppliesDetail = new SuppliesInputDetailDTO
                {
                    Supply = SelectedSp,
                    Count = int.Parse(Count),
                    PriceItem = decimal.Parse(PriceSp)
                };
                spDetailCollection.Add(newSuppliesDetail);
                //MessageBoxCustom.Show(MessageBoxCustom.Success, "Thêm thành công");
                //SelectedSp = null;
                //Count = null;
                //PriceSp = null;
            });

            AddSpInput = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                List<SuppliesInputDetailDTO> suppliesInputDetails = new List<SuppliesInputDetailDTO>(spDetailCollection);
                if (suppliesInputDetails.ToArray().Length == 0)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Vui lòng thêm vật tư"); return;
                }
                var suppliesInput = new SuppliesInputDTO
                {
                    DateInput = DateTime.Now,
                    SuppliesInputDetails = suppliesInputDetails,
                    TotalMoney = 0
                };
                var (isSuccess, msg) = await SuppliesService.Ins.AddSupplyInput(suppliesInput);
                if (isSuccess)
                {

                    supplyColection = new ObservableCollection<SupplyDTO>(await SuppliesService.Ins.GetAllSupply());
                    spDetailCollection= new ObservableCollection<SuppliesInputDetailDTO>();
                    SelectedSp = null;
                    Count = null;
                    PriceSp = null;
                    MessageBoxCustom.Show(MessageBoxCustom.Success, "Thêm thành công");

                }
                else
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Khong them duoc du lieu");
                }
            });

            DeleteSpDetail = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Window wd = new DeleteMessageBox();
                wd.ShowDialog();
                if (wd.DialogResult == false)
                {
                    return;
                }
                spDetailCollection.Remove(SelectedSpDetail);
            });

            NhapVatTuPhuTungClose = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                SelectedSp = null;
                Count = null;
                PriceSp = null;
                p.Close();
            });

            AddContent = new RelayCommand<object>(p => { return true; }, async (p) =>
            {
                if(wageSelected == null || Content == null || Content == "")
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Vui lòng nhập đủ thông tin"); return;
                }
                var price = wageSelected.Price;
                var newRepairDetail = new RepairDetailDTO
                {
                    Content = Content,
                    WageId = wageSelected.ID,
                    WageName = wageSelected.Name,
                    WagePrice = wageSelected.Price,
                    Price = price,
                    RepairSuppliesDetails = new List<RepairSuppliesDetailDTO>()
                };

                rpdtCollection.Add(newRepairDetail);
            });

            DeleteRpdt = new RelayCommand<object>(p => { return true; }, (p) =>
            {
                Window wd = new DeleteMessageBox();
                wd.ShowDialog();
                if (wd.DialogResult == false)
                {
                    return;
                }
                rpdtCollection.Remove(SelectedRpdt);
            });

            OpenAddSupplyRepair = new RelayCommand<object>(p => { return true; }, (p) =>
            {
                SpContent = SelectedRpdt.Content;
                SpWage = SelectedRpdt.WageName;
                RpSpDtCollection = new ObservableCollection<RepairSuppliesDetailDTO>(SelectedRpdt.RepairSuppliesDetails);
                Window mainWindow = Application.Current.MainWindow; // Lấy cửa sổ chính
                mainWindow.Opacity = 0.5; // Làm mờ cửa sổ chính
                ThemNoiDungSuaChua dialogWindow = new ThemNoiDungSuaChua();
                dialogWindow.ShowDialog();
                // Hiển thị cửa sổ dialog
                mainWindow.Opacity = 1.0;
            });



            AddRpSpDt = new RelayCommand<Window>(p => { return true; }, (p) =>
            {
                SelectedRpdt.RepairSuppliesDetails = new List<RepairSuppliesDetailDTO>(RpSpDtCollection);
                decimal price = 0;
                foreach (RepairSuppliesDetailDTO i in RpSpDtCollection)
                {
                    price += (decimal)(i.PriceItem * i.Count);
                }
                SelectedRpdt.Price = SelectedRpdt.WagePrice + price;
                SelectedSupplyDt = null;
                CountSupply = null;
                p.Close();

            });

            OnCloseRpSpDtWd = new RelayCommand<Window>(p => { return true; }, (p) =>
            {
                SelectedSupplyDt = null;
                CountSupply = null;
                p.Close();

            });

            AddRecordRpSpDt = new RelayCommand<object>(p => { return true; }, (p) =>
            {
                if(CountSupply == "" || CountSupply==null || SelectedSupplyDt == null)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Vui lòng nhập đủ thông tin"); return;
                }
                if (int.TryParse(CountSupply, out int rs) == false)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Định dạng dữ liệu không đúng");
                    return;
                }
                var newRpSpDt = new RepairSuppliesDetailDTO
                {
                    Count = int.Parse(CountSupply),
                    PriceItem = SelectedSupplyDt.OutputPrices,
                    Supply = SelectedSupplyDt
                };
                var record = RpSpDtCollection.FirstOrDefault(x => x.Supply.ID == SelectedSupplyDt.ID);
                if (record != null)
                {
                    record.Count += newRpSpDt.Count;
                }
                else
                {
                    RpSpDtCollection.Add(newRpSpDt);
                }
            });

            DeleteRpSpDt = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Window wd = new DeleteMessageBox();
                wd.ShowDialog();
                if (wd.DialogResult == false)
                {
                    return;
                }
                RpSpDtCollection.Remove(SelectedSpRpDt);
            });

            ResetRepair = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LicensePlate = null;
                Content = null;
                wageSelected = null;
                NgaySuaChua = DateTime.Now;
                rpdtCollection = new ObservableCollection<RepairDetailDTO>();
            });

            AddRepair = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (LicensePlate == null || LicensePlate == "" || rpdtCollection.ToArray().Length == 0 )
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Vui lòng nhập đủ thông tin"); return;
                }
                var recept = await ReceptionService.Ins.GetReceptionbyPlate(LicensePlate);
                if (recept==null)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Không tìm thấy phiếu tiếp nhận phù hợp"); return;
                }
                var repairDetails = new List<RepairDetailDTO>(rpdtCollection);
                var newRepair = new RepairDTO
                {

                    RepairDate = NgaySuaChua,
                    TotalPrice = 0,
                    Reception = recept,
                    RepairDetails = repairDetails
                };
                var (isSuccess, msg) = await RepairService.Ins.AddNewRepair(newRepair);
                if(isSuccess)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Success, "Thêm thành công");
                }
                else
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Khong them duoc du lieu");
                }
            });
        }
    }
}
