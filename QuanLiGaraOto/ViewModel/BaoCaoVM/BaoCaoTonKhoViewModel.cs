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
    internal class BaoCaoTonKhoViewModel : BaseViewModel
    {
        private InventoryReportDTO _currentInventoryReport;
        public InventoryReportDTO CurrentInventoryReport
        {
            get => _currentInventoryReport;
            set => SetProperty(ref _currentInventoryReport, value);
        }

        private ObservableCollection<InventoryReportDetailDTO> _inventoryDetails;
        public ObservableCollection<InventoryReportDetailDTO> InventoryDetails
        {
            get => _inventoryDetails;
            set => SetProperty(ref _inventoryDetails, value);
        }

        //private Supply _selectedSupply;
        //public Supply SelectedSupply
        //{
        //    get => _selectedSupply;
        //    set => SetProperty(ref _selectedSupply, value);
        //}

        private int _month;
        public int Month
        {
            get { return _month; }
            set { SetProperty(ref _month, value); OnPropertyChanged(); }
        }

        private int _year;
        public int Year
        {
            get { return _year; }
            set { SetProperty(ref _year, value); OnPropertyChanged(); }
        }

        // Khai báo các lệnh (Commands)
        public ICommand InitInventoryReport { get; }
        //public ICommand AddNewSupplyCommand { get; }
        //public ICommand UpdatePhatSinhCommand { get; }
        //public ICommand UpdateTonCuoiCommand { get; }
        //public ICommand DeleteDetailCommand { get; }
        //public ICommand RecoveryInventoryCommand { get; }
        public ICommand GetCurrentInventoryReport {  get; }
        public ICommand GetInventoryReport {  get; }

        // Khởi tạo ViewModel
        public BaoCaoTonKhoViewModel()
        {

            InitInventoryReport = new RelayCommand<object>(_=>true, async _ =>
            {
                var (issuccess, message) = await InvetoryReportService.Ins.InitInventoryReport();
                if (issuccess)
                {
                    await LoadCurrentInventoryReport();
                    MessageBoxCustom.Show(MessageBoxCustom.Success,message);
                }
                else
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, message);
                }
            });

            GetCurrentInventoryReport = new RelayCommand<object>(_ => true, async _ =>
            {
                CurrentInventoryReport = await InvetoryReportService.Ins.GetCurrentInventoryReport();
            });

            GetInventoryReport = new RelayCommand<object>(_ => true, async (param) =>
            {
                var curDate = DateTime.Now;
                var curMonth = curDate.Month;
                var curYear = curDate.Year;
                while (Month > curMonth && Year > curYear)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Tháng và năm lớn hơn hiện tại, vui lòng nhập lại.");
                }
                while(Month < curMonth && Year == curYear)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Tháng lớn hơn hiện tại, vui lòng nhập lại.");
                } 

                CurrentInventoryReport = await InvetoryReportService.Ins.GetInventoryReport(Month, Year);
                await LoadCurrentInventoryReport();
            });

            //AddNewSupplyCommand = new RelayCommand<object>(_=> true,async (param) =>
            //{
            //    var newSupply = param as Supply;
            //    if (newSupply == null)
            //        return;

            //    var success = await InvetoryReportService.Ins.AddNewSupply(newSupply);
            //    if (success)
            //    {
            //        await LoadCurrentInventoryReport();
            //        MessageBoxCustom.Show(MessageBoxCustom.Success, "Thêm thành công!");
            //    }
            //    else
            //    {
            //        MessageBoxCustom.Show(MessageBoxCustom.Error, "Thêm không thành công!");
            //    }
            //});

            //UpdatePhatSinhCommand = new RelayCommand<object>(_=>true,async (param) =>
            //{
            //    var args = param as Tuple<int, int>;
            //    if (args == null)
            //        return;

            //    var (delta, supplyId) = args;
            //    var success = await InvetoryReportService.Ins.UpdatePhatSinh(delta, supplyId);
            //    if (success)
            //    {
            //        await LoadCurrentInventoryReport();
            //        MessageBoxCustom.Show(MessageBoxCustom.Success, "Cập nhật thành công!");
            //    }
            //    else
            //    {
            //        MessageBoxCustom.Show(MessageBoxCustom.Error, "Cập nhật không thành công!");
            //    }
            //});

            //UpdateTonCuoiCommand = new RelayCommand<object>(_=>true,async (param) =>
            //{
            //    var args = param as Tuple<int, int>;
            //    if (args == null)
            //        return;

            //    var (tonCuoi, supplyId) = args;
            //    var success = await InvetoryReportService.Ins.UpdateTonCuoi(tonCuoi, supplyId);
            //    if (success)
            //    {
            //        await LoadCurrentInventoryReport();
            //        MessageBoxCustom.Show(MessageBoxCustom.Success, "Cập nhật thành công!");
            //    }
            //    else
            //    {
            //        MessageBoxCustom.Show(MessageBoxCustom.Error, "Cập nhật không thành công!");
            //    }
            //});

            //DeleteDetailCommand = new RelayCommand<object>(_=>true,async (param) =>
            //{
            //    var supplyId = param as int?;
            //    if (!supplyId.HasValue)
            //        return;

            //    var success = await InvetoryReportService.Ins.DeleteDetail(supplyId.Value);
            //    if (success)
            //    {
            //        await LoadCurrentInventoryReport();
            //        MessageBoxCustom.Show(MessageBoxCustom.Success, "Đã Xóa thành công!");
            //    }
            //    else
            //    {
            //        MessageBoxCustom.Show(MessageBoxCustom.Error, "Xóa không thành công!");
            //    }
            //});

            //RecoveryInventoryCommand = new RelayCommand<object>(_=>true,async (param) =>
            //{
            //    var repair = param as Repair;
            //    if (repair == null)
            //        return;

            //    var success = await InvetoryReportService.Ins.RecoveryInventory(repair);
            //    if (success)
            //    {
            //        await LoadCurrentInventoryReport();
            //        MessageBoxCustom.Show(MessageBoxCustom.Success, "Phục hồi thành công!");
            //    }
            //    else
            //    {
            //        MessageBoxCustom.Show(MessageBoxCustom.Error, "Phục hồi không thành công!");
            //    }
            //});

            // Tải báo cáo tồn kho hiện tại khi ViewModel được khởi tạo
            LoadCurrentInventoryReport();
        }

        // Tải báo cáo tồn kho hiện tại
        private async Task LoadCurrentInventoryReport()
        {
            
            if (CurrentInventoryReport != null)
            {
                InventoryDetails = new ObservableCollection<InventoryReportDetailDTO>(CurrentInventoryReport.InventoryReportDetails);
            }
        }


    }
}
