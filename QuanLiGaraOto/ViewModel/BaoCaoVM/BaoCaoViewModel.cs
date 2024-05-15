using MaterialDesignThemes.Wpf;
using QuanLiGaraOto.DTOs;
using QuanLiGaraOto.Model;
using QuanLiGaraOto.Model.service;
using QuanLiGaraOto.Utils;
using QuanLiGaraOto.View.BaoCao;
using System.Collections.ObjectModel;
using System.Globalization;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using QuanLiGaraOto.View.MessageBox;
using System.Windows.Media;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Word;

namespace QuanLiGaraOto.ViewModel.BaoCaoVM
{
    internal class BaoCaoViewModel : BaseViewModel
    {
        private int _id;
        private int _month;
        private int _year;

        private TonKho curTonKho;
        private DoanhThu curDoanhThu;
        public int ID { get { return _id; } set {  _id = value; } }

        public int Month { get { return _month; } set { _month = value; OnPropertyChanged(); } }
        public int Year { get { return _year; } set { _year = value; OnPropertyChanged(); } }


        private ObservableCollection<int> _monthList;
        public ObservableCollection<int> MonthList
        {
            get { return _monthList; }
            set { _monthList = value; OnPropertyChanged(nameof(MonthList)); }
        }
        private ObservableCollection<int> _yearList;
        public ObservableCollection<int> YearList
        {
            get { return _yearList; }
            set { _yearList = value; OnPropertyChanged(nameof(YearList)); }
        }

        // Inventory
        private InventoryReportDTO _currentInventoryReport;
        public InventoryReportDTO CurrentInventoryReport
        {
            get { return _currentInventoryReport; }
            set { _currentInventoryReport = value; }
        }

        private ObservableCollection<InventoryReportDetailDTO> _inventoryDetails;
        public ObservableCollection<InventoryReportDetailDTO> InventoryDetails
        {
            get { return _inventoryDetails; }
            set { _inventoryDetails = value;  OnPropertyChanged(); }
        }

        private UserControl _currentUserControl;
        public UserControl CurrentUserControl
        {
            get => _currentUserControl;
            set => SetProperty(ref _currentUserControl, value);
        }

        private Visibility _isNullVisible;
        public Visibility IsNullVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; OnPropertyChanged(); }
        }
        // InventoryCommand---------------------------

        public ICommand GetInventoryReport { get; set; }

        // Revenue-----------------------------
        private RevenueDTO _revenue;
        public RevenueDTO RevenueReport
        {
            get { return _revenue; }
            set { _revenue = value; }
        }

        private ObservableCollection<RevenueDetailDTO> _revenueList;
        public ObservableCollection<RevenueDetailDTO> RevenueList
        {
            get { return _revenueList; }
            set { _revenueList = value; OnPropertyChanged(); }
        }

        private decimal _totalprice;
        public decimal TotalPrice { 
            get { return _totalprice; } 
            set { _totalprice = value; OnPropertyChanged(); }
        }

        private Visibility _isVisible;
        public Visibility IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; OnPropertyChanged(); }
        }

        // Revenue Command
        public ICommand GetRevenue { get; set; }

        //---------------------------------------------------
       
        public ICommand FirstLoad { get; set; }
        public ICommand OpenBaoCaoTonKho {  get; set; }
        public ICommand OpenBaoCaoDoanhThu { get; set; }
        public ICommand PrintBaoCao {  get; set; }

        
        public BaoCaoViewModel() { 
            IsVisible = Visibility.Hidden;
            IsNullVisible = Visibility.Hidden;
            // PageCommand
            FirstLoad = new RelayCommand<object>(_=> true, _=>
            {
                var curDate = DateTime.Now;
                var curYear = curDate.Year;
                var years = Enumerable.Range(2020, (curYear - 2019)).ToList();
                YearList = new ObservableCollection<int>(years);
                var curMonth = (curDate.Month-1);
                var months = Enumerable.Range(1, 12).ToList();
                MonthList = new ObservableCollection<int>(months);
                Year = curYear;
                Month = curMonth;
            });
            OpenBaoCaoTonKho = new RelayCommand<object>(_ => true, async (param) =>
            {
                curTonKho = new TonKho();
                IsVisible = Visibility.Hidden;
                IsNullVisible = Visibility.Hidden;
                GetInventoryReport.Execute(null);
                if(CurrentInventoryReport != null && CurrentInventoryReport.InventoryReportDetails.Count != 0)
                {
                    CurrentUserControl = curTonKho;
                }

            });
            OpenBaoCaoDoanhThu = new RelayCommand<object>(_ => true,async (param) =>
            {
                curDoanhThu = new DoanhThu();
                IsVisible = Visibility.Visible;
                IsNullVisible = Visibility.Hidden;
                GetRevenue.Execute(null);
                if (RevenueReport != null && RevenueReport.RevenueDetails.Count != 0)
                {
                    CurrentUserControl = curDoanhThu;
                }
            });

            PrintBaoCao = new RelayCommand<object>(_=> true, _ =>
            {
                // Kiểm tra xem UserControl hiện tại có phải là BaoCaoDoanhThu không
                if (CurrentUserControl is DoanhThu)
                {
                    //// Chụp ảnh chụp màn hình của UserControl
                    //RenderTargetBitmap rtb = new RenderTargetBitmap((int)curDoanhThu.ActualWidth, (int)curDoanhThu.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                    //rtb.Render(curDoanhThu);

                    //// Lưu hình ảnh vào tệp tạm thời
                    //string tempFilePath = Path.Combine(Path.GetTempPath(), "tempImage.png");
                    //using (var fileStream = new FileStream(tempFilePath, FileMode.Create))
                    //{
                    //    PngBitmapEncoder pngToFile = new PngBitmapEncoder();
                    //    pngToFile.Frames.Add(BitmapFrame.Create(rtb));
                    //    pngToFile.Save(fileStream);
                    //}

                    // Tạo một tài liệu Word mới
                    var wordApp = new Microsoft.Office.Interop.Word.Application();
                    var document = wordApp.Documents.Add();
                    // Thêm header
                    foreach (Microsoft.Office.Interop.Word.Section section in document.Sections)
                    {
                        // Lấy header của mỗi section
                        Microsoft.Office.Interop.Word.HeaderFooter header = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary];
                        header.Range.Text = "Báo cáo doanh thu tháng "+ Month.ToString() +" năm " + Year.ToString();
                        header.Range.Font.Size = 32;
                        header.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    }

                    //// Chèn hình ảnh từ tệp tạm thời vào tài liệu
                    //document.InlineShapes.AddPicture(tempFilePath, LinkToFile: false, SaveWithDocument: true);

                    //// Xóa tệp tạm thời
                    //File.Delete(tempFilePath);

                    // Tạo bảng trong tài liệu Word
                     var table = document.Tables.Add(document.Content, RevenueList.Count + 1, 5); // Số cột cố định là 5

                    // Điền dữ liệu từ RevenueList vào bảng
                    table.Cell(1, 1).Range.Text = "STT";
                    table.Cell(1, 2).Range.Text = "Hiệu xe";
                    table.Cell(1, 3).Range.Text = "Số lượt sửa chữa";
                    table.Cell(1, 4).Range.Text = "Thành tiền";
                    table.Cell(1, 5).Range.Text = "Tỉ lệ";
                    for (int i = 0; i < RevenueList.Count; i++)
                    {
                        var item = RevenueList[i];
                        table.Cell(i + 2, 1).Range.Text = item.STT.ToString();
                        table.Cell(i + 2, 2).Range.Text = item.BrandCar.Name;
                        table.Cell(i + 2, 3).Range.Text = item.RepairCount.ToString();
                        table.Cell(i + 2, 4).Range.Text = string.Format("{0:N0} VNĐ", item.Price);
                        table.Cell(i + 2, 5).Range.Text = ((double)item.Ratio).ToString("F2");
                    }
                    Microsoft.Office.Interop.Word.Paragraph totalRevenueParagraph = document.Content.Paragraphs.Add();
                    totalRevenueParagraph.Range.Text = "Tổng doanh thu: " + string.Format("{0:N0} VNĐ", TotalPrice);
                    totalRevenueParagraph.Range.InsertParagraphAfter();
                    // Lưu tài liệu
                    string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "BaoCaoDoanhThu.docx");
                    document.SaveAs2(filePath);
                    document.Close();

                    // Đóng ứng dụng Word
                    wordApp.Quit();

                    MessageBoxCustom.Show(MessageBoxCustom.Success,"Báo cáo doanh thu đã được tạo thành công tại: " + filePath);
                }
                else
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Vui lòng mở Báo cáo doanh thu trước khi in.");
                }
            });

            // InventoryCommand

            GetInventoryReport = new RelayCommand<object>(_ => true, async _ =>
            {
                var curDate = DateTime.Now;
                var curMonth = curDate.Month;
                var curYear = curDate.Year;
                if (Year > curYear)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Năm lớn hơn hiện tại, vui lòng nhập lại.");
                    return;
                }
                else if (Month > (curMonth-1) && Year == curYear)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Chỉ có thể xem dữ liệu các tháng trước, vui lòng nhập lại.");
                    return ;
                }

                CurrentInventoryReport = await InvetoryReportService.Ins.GetInventoryReport(this.Month, this.Year);
                if (CurrentInventoryReport != null)
                {
                    IsNullVisible = Visibility.Hidden;
                    Console.WriteLine(CurrentInventoryReport.InventoryReportDetails.Count);
                    InventoryDetails = new ObservableCollection<InventoryReportDetailDTO>(CurrentInventoryReport.InventoryReportDetails);
                }
                if(CurrentInventoryReport == null || CurrentInventoryReport.InventoryReportDetails.Count == 0) { IsNullVisible = Visibility.Visible; }
            });

            // Revenue Command

            GetRevenue = new RelayCommand<object>(_ => true, async _ =>
            {
                var curDate = DateTime.Now;
                var curMonth = curDate.Month;
                var curYear = curDate.Year;
                if (Year > curYear)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Năm lớn hơn năm hiện tại, vui lòng nhập lại.");
                    return;
                }
                else if (Month > (curMonth - 1) && Year == curYear)
                {
                    MessageBoxCustom.Show(MessageBoxCustom.Error, "Chỉ có thể xem dữ liệu các tháng trước, vui lòng nhập lại.");
                    return;
                }

                RevenueReport = await RevenueService.Ins.GetRevenue(this.Month, this.Year);
                Console.WriteLine("Pass");
                if (RevenueReport != null)
                {
                    IsNullVisible = Visibility.Hidden;
                    TotalPrice = (decimal)RevenueReport.TotalPrice;
                    RevenueList = new ObservableCollection<RevenueDetailDTO>(RevenueReport.RevenueDetails);
                    for (int i = 0; i < RevenueList.Count; i++)
                    {
                        RevenueList[i].STT = i + 1;
                    }
                } 
                if(RevenueReport == null || RevenueReport.RevenueDetails.Count == 0) { IsNullVisible = Visibility.Visible; }
            });

        }
        

    }
}
