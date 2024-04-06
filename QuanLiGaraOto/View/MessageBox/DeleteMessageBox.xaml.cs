using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace QuanLiGaraOto.View.MessageBox
{
	/// <summary>
	/// Interaction logic for DeleteMessage.xaml
	/// </summary>
	public partial class DeleteMessageBox : Window
	{

		public DeleteMessageBox(string s = "Bạn có chắc chắn muốn xóa không")
		{
			InitializeComponent();
			TextboxShow.Text = s;
		}

		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				DragMove();
			}
		}

		private void No_btn_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
			Window.GetWindow(this).Close();
		}

		private void Yes_btn_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
			this.Close();
		}
	}
}
