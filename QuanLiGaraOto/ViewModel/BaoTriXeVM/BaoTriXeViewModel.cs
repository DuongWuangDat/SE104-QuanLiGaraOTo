using MaterialDesignThemes.Wpf;
using QuanLiGaraOto.Model;
using QuanLiGaraOto.View.BaoTriXePage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLiGaraOto.ViewModel.BaoTriXeVM
{
    public class BaoTriXeViewModel : BaseViewModel
    {
		private int _receptionCount;

		public int ReceptionCount
        {
			get { return _receptionCount; }
			set { _receptionCount = value; }
		}

		private List<String> _brandCarList;

		public List<String> BrandCarList
		{
            get { return new List<String> { 
				"Audi", "BMW", "Chevrolet", "Ford", "Honda", "Hyundai", "Kia", "Lexus", "Mazda", "Mercedes-Benz", "Mitsubishi", "Nissan", "Peugeot", "Subaru", "Suzuki", "Toyota", "Volkswagen", "Volvo"
			}; }
            set { _brandCarList = value; }
        }

		private DateTime selectedDate = DateTime.Now;

		public DateTime SelectedDate
		{
			get { return selectedDate; }
			set { selectedDate = value; OnPropertyChanged(); }
		}

		private ObservableCollection<BrandCar> _brandCars;

		public ObservableCollection<BrandCar> BrandCarS
        {
			get { return new ObservableCollection<BrandCar> { 
				new BrandCar("Audi"),
                new BrandCar("Audi")
            }; }
			set { _brandCars = value; OnPropertyChanged(); }
		}

		public ICommand FirstLoadBrandCar { get; set;}
		public ICommand OpenManageBrandWD { get; set;}
		public BaoTriXeViewModel()
		{
			OpenManageBrandWD =	new RelayCommand<object>((p) => { return true; }, (p) =>
			{
				QuanLiHieuXeWD quanLiHieuXeWD = new QuanLiHieuXeWD();
				quanLiHieuXeWD.ShowDialog();
			});
			
           
        }

	}
}
