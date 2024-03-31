using QuanLiGaraOto.ViewModel.MessageBoxVM;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiGaraOto.View.MessageBox
{
	/// <summary>
	/// Interaction logic for MessageBox.xaml
	/// </summary>
	public partial class Success : Window
	{

		public Success(string text)
		{
			InitializeComponent();
			DataContext = new MessageBoxViewModel(text);
		}

		private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				DragMove();
			}
		}

		private void Ok_btn_Click(object sender, RoutedEventArgs e)
		{
			Window.GetWindow(this).Close();
		}
	}
}
