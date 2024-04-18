using QuanLiGaraOto.View.MessageBox;
using QuanLiGaraOto.ViewModel.PhieuThuTienVM;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiGaraOto.View.PhieuThuTien
{
	/// <summary>
	/// Interaction logic for ThemPhieuThuTien.xaml
	/// </summary>
	public partial class ThemPhieuThuTien : Window
	{
		public ThemPhieuThuTien()
		{
			InitializeComponent();
			DataContext = new ThemPhieuThuTienViewModel();
		}

		private void PaymentTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox textBox = sender as TextBox;
			string number = textBox.Text.Replace(",", "");
			if(!string.IsNullOrEmpty(textBox.Text))
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

		private void Huy_Click(object sender, RoutedEventArgs e)
		{
			Window.GetWindow(this)?.Close();
		}

		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				DragMove();
			}
		}

		private void Window_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}

		private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
		{

        }
    }
}
