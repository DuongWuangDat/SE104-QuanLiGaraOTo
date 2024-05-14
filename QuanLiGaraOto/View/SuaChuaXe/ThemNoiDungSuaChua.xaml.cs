using QuanLiGaraOto.View.MessageBox;
using QuanLiGaraOto.ViewModel.SuaChuaXeVM;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiGaraOto.View.SuaChuaXe
{
    /// <summary>
    /// Interaction logic for ThemNoiDungSuaChua.xaml
    /// </summary>
    public partial class ThemNoiDungSuaChua : Window
    {
        public ThemNoiDungSuaChua()
        {
            InitializeComponent();
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

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as SuaChuaXeViewModel).DeleteRpSpDt.Execute(new object());
        }
    }
}
