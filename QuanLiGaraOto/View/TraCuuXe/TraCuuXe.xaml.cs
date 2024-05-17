using QuanLiGaraOto.ViewModel.TraCuuXeVM;
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

namespace QuanLiGaraOto.View.TraCuuXe
{
	/// <summary>
	/// Interaction logic for TraCuuXe.xaml
	/// </summary>
	public partial class TraCuuXe : Page
	{
		public TraCuuXe()
		{
			InitializeComponent();
		}

		private void Edit_Click(object sender, RoutedEventArgs e)
		{
			(DataContext as TraCuuXeViewModel).OpenEditXe.Execute(traCuuXeWindow);
		}

		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			(DataContext as TraCuuXeViewModel).DeleteXe.Execute(traCuuXeWindow);
		}
	}
}
