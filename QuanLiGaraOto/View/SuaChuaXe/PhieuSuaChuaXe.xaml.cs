using QuanLiGaraOto.ViewModel.SuaChuaXeVM;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLiGaraOto.View.SuaChuaXe
{
    /// <summary>
    /// Interaction logic for PhieuSuaChuaXe.xaml
    /// </summary>
    public partial class PhieuSuaChuaXe : Page
    {
        public PhieuSuaChuaXe()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            (DataContext as SuaChuaXeViewModel).DeleteRpdt.Execute(new object());
        }
    }
}
