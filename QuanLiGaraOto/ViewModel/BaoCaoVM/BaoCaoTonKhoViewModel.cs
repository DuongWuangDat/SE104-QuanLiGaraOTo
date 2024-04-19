using QuanLiGaraOto.DTOs;
using QuanLiGaraOto.Model.service;
using QuanLiGaraOto.Model;
using QuanLiGaraOto.View.BaoCao;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using QuanLiGaraOto.Utils;
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
        private int _id;
        private int _month;
        private int _year;
        
        public int ID { get { return _id; } set { _id = value; } }
        public int Month { get { return _month; } set { _month = value; } }
        public int Year { get { return _year; } set { _year = value; } }

        private ObservableCollection<InventoryReportDetailDTO> _reportList;
        public ObservableCollection<InventoryReportDetailDTO> ReportList { 
            get { return _reportList; } 
            set { _reportList = value; OnPropertyChanged(); }
        }

        private InventoryReportDetailDTO _selectedItem;
        public InventoryReportDetailDTO SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        private InventoryReportDTO _report;
        public InventoryReportDTO Report
        {
            get { return _report; }
            set { _report = value; OnPropertyChanged(); }
        }

        //-------------------------------------------------------------

        public ICommand CreateReport {  get; set; }
        public ICommand GetReportList { get; set;}

        public BaoCaoTonKhoViewModel() {

            // Create Report
            CreateReport = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                Report = await InvetoryReportService.Ins.GetInventoryReport(Month, Year);
            });

            // Get Report List - InventoryReportDetail

            GetReportList = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                var (IsSuccess, message) = await InvetoryReportService.Ins.InitInventoryReport();
                if (IsSuccess)
                {
                    MessageBox.Show(message);
                }
                else
                {
                    MessageBox.Show(message);
                }

            });
        }
    }
}
