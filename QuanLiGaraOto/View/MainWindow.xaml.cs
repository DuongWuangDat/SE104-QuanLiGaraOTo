using QuanLiGaraOto.Model;
using QuanLiGaraOto.Model.service;
using QuanLiGaraOto.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLiGaraOto.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		public MainWindow()
		{
			InitializeComponent();
			this.Left = 0; // X position
			this.Top = 0; // Y position
			this.Width = SystemParameters.WorkArea.Width;
			this.Height = SystemParameters.WorkArea.Height;
		}

		private void Overlay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BeginStoryboard((Storyboard)Resources["MenuClose"]);
        }

        private void AdminWD_Closed(object sender, System.EventArgs e)
        {
           // this.Owner.Visibility = Visibility.Visible;
        }

        private async void MainWD_Loaded(object sender, RoutedEventArgs e)
        {
            //var (isSuccessInvenLoaded, message) = await InvetoryReportService.Ins.InitInventoryReport();
            //var isSuccessRevenueLoaded= await RevenueService.Ins.InitRevenue();
            //if(!isSuccessInvenLoaded || !isSuccessRevenueLoaded)
            //{
            //    MessageBoxCustom.Show(MessageBoxCustom.Error, "Không thể khởi tạo dữ liệu");
            //}
        }

		private void closeButton_Click(object sender, RoutedEventArgs e)
		{
            Window window = Window.GetWindow(this);
            window.Close();
        }
    }
}
