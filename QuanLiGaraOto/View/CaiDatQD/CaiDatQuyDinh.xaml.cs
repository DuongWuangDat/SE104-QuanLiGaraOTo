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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLiGaraOto.View.CaiDatQD
{
    /// <summary>
    /// Interaction logic for CaiDatQuyDinh.xaml
    /// </summary>
    public partial class CaiDatQuyDinh : Page
    {
        public CaiDatQuyDinh()
        {
            InitializeComponent();
        }
        private void TiLeTinhDonGiaBan_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Kiểm tra xem ký tự nhập vào có phải là số hay không
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != ".")
            {
                e.Handled = true;
                MessageBoxCustom.Show(MessageBoxCustom.Error,"Chỉ được nhập số nguyên và số thập phân");
            }
        }
        private void SoXeSuaChua_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Kiểm tra xem ký tự nhập vào có phải là số hay không
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text == ".")
            {
                e.Handled = true;
                MessageBoxCustom.Show(MessageBoxCustom.Error, "Chỉ được nhập số nguyên");
            }
        }
    }
}
