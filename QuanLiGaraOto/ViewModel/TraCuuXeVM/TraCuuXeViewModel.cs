using QuanLiGaraOto.DTOs;
using QuanLiGaraOto.Model;
using QuanLiGaraOto.Model.service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiGaraOto.ViewModel.TraCuuXeVM
{
	public class TraCuuXeViewModel : BaseViewModel
	{

		private List<ReceptionDTO> xeList;

		public List<ReceptionDTO> XeList
		{
			get { return xeList; }
			set { xeList = value; }
		}

		private ObservableCollection<ReceptionDTO> xeCollection;

		public ObservableCollection<ReceptionDTO> XeCollection
		{
			get { return xeCollection; }
			set { xeCollection = value; OnPropertyChanged(); }
		}

		public ICommand FirstLoad { get; set; }
		public ICommand TimKiemXe { get; set; }
		public TraCuuXeViewModel()
		{
			FirstLoad = new RelayCommand<object>(_ => true, async _ =>
			{
				XeList = await ReceptionService.Ins.GetAllReception();
				XeCollection = new ObservableCollection<ReceptionDTO>(XeList);
			});

			TimKiemXe = new RelayCommand<object>(_ => true, p =>
			{
				string searchText = ((TextBox)p).Text.ToLower();
				if (string.IsNullOrEmpty(searchText))
				{
					XeCollection = new ObservableCollection<ReceptionDTO>(XeList);
				}
				else
				{
					XeCollection = new ObservableCollection<ReceptionDTO>(XeList.FindAll(x => x.Customer.Name.ToLower().Contains(searchText)
					|| x.LicensePlate.ToLower().Contains(searchText)));
				}
			});

		}
	}
}
