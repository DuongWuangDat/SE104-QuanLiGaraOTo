using QuanLiGaraOto.View.MessageBox;
using QuanLiGaraOto.ViewModel.PhieuThuTienVM;
using System;
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

		private void TextBox_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = sender as TextBox;
			try
			{
				if (!string.IsNullOrEmpty(textBox.Text))
				{
					System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("vi-VN");
					var valueBefore = Int64.Parse(textBox.Text, System.Globalization.NumberStyles.AllowThousands);

					string formattedValue = valueBefore.ToString("#,##0", culture);

					textBox.Text = formattedValue;
					textBox.Select(textBox.Text.Length, 0);
				}
			}
			catch (Exception)
			{
				MessageBoxCustom.Show(MessageBoxCustom.Error, "Lương không hợp lệ");
			}
		}



		private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (!((e.Key >= Key.D0 && e.Key <= Key.D9) ||  // Số từ 0 đến 9
			(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||  // Số từ bàn phím số
			e.Key == Key.Delete ||  // Phím xóa
			e.Key == Key.Back ||  // Phím backspace
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
	}
}
