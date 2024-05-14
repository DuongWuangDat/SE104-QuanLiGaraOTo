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

namespace QuanLiGaraOto.View.PhieuThuTien
{
	/// <summary>
	/// Interaction logic for TempPhieuThuTien.xaml
	/// </summary>
	public partial class TempPhieuThuTien : Page
	{
		public TempPhieuThuTien()
		{
			InitializeComponent();
		}

		private void PaymentTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox textBox = sender as TextBox;
			string number = textBox.Text.Replace(",", "");
			if (!string.IsNullOrEmpty(textBox.Text))
			{
				if (Decimal.TryParse(number, out decimal parsedNumber))
				{
					int cursorPosition = textBox.SelectionStart;
					System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo
					{
						NumberGroupSeparator = ",",
						NumberDecimalSeparator = "."
					};
					textBox.Text = parsedNumber.ToString("N0", nfi);
					textBox.SelectionStart = textBox.Text.Length;
				}
				else
				{
					MessageBoxCustom.Show(MessageBoxCustom.Error, "Tiền thu không hợp lệ");
				}
			}
		}

		private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (!((e.Key >= Key.D0 && e.Key <= Key.D9) ||  // Số từ 0 đến 9
			(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||  // Số từ bàn phím số
			e.Key == Key.Delete ||  // Phím xóa
			e.Key == Key.Back ||  // Phím backspace
			e.Key == Key.Left ||  // Phím mũi tên trái
			e.Key == Key.Right ||  // Phím mũi tên phải
			e.Key == Key.Tab ||  // Phím tab
			(Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.A)))
			{
				e.Handled = true; // Ngăn chặn ký tự nếu không phải số từ bàn phím
			}
		}
	}
}
