using QuanLiGaraOto.DTOs;
using QuanLiGaraOto.Model;
using QuanLiGaraOto.Model.service;
using QuanLiGaraOto.View.MessageBox;
using QuanLiGaraOto.View.TraCuuXe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
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

		private ReceptionDTO selectedItem;

		public ReceptionDTO SelectedItem
		{
			get { return selectedItem; }
			set { selectedItem = value; }
		}

		#region EditXe

		private string editBienSo;
		public string EditBienSo
		{
			get
			{
				return editBienSo;
			}
			set
			{
				editBienSo = value;
				OnPropertyChanged();
			}
		}

		private ObservableCollection<BrandCarDTO> hieuXeList;

		public ObservableCollection<BrandCarDTO> HieuXeList
		{
			get { return hieuXeList; }
			set
			{
				hieuXeList = value;
				OnPropertyChanged();
			}
		}

		private BrandCarDTO selectedBrandCar;

		public BrandCarDTO SelectedBrandCar
		{
			get { return selectedBrandCar; }
			set { selectedBrandCar = value; OnPropertyChanged(); }
		}

		private string editEmail;

		public string EditEmail
		{
			get { return editEmail; }
			set { editEmail = value; OnPropertyChanged(); }
		}


		private string editChuXe;

		public string EditChuXe
		{
			get { return editChuXe; }
			set { editChuXe = value; OnPropertyChanged(); }
		}

		private string editSoDienThoai;
		public string EditSoDienThoai
		{
			get { return editSoDienThoai; }
			set { editSoDienThoai = value; OnPropertyChanged(); }
		}

		private string editDiaChi;
		public string EditDiaChi
		{
			get { return editDiaChi; }
			set { editDiaChi = value; OnPropertyChanged(); }
		}

		private string editTienNo;

		public string EditTienNo
		{
			get { return editTienNo; }
			set { editTienNo = value; OnPropertyChanged(); }
		}

		#endregion


		public ICommand FirstLoad { get; set; }
		public ICommand TimKiemXe { get; set; }
		public ICommand OpenEditXe { get; set; }
		public ICommand DeleteXe { get; set; }
		public ICommand SuaThongTinXe { get; set; }

		public TraCuuXeViewModel()
		{
			FirstLoad = new RelayCommand<object>(_ => true, async _ =>
			{
				XeList = await ReceptionService.Ins.GetAllReception();
				if(XeList == null)
				{
					return;
				}
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

			OpenEditXe = new RelayCommand<object>(_ => true, async p =>
			{
				Page traCuuXePage = p as Page;
				if (traCuuXePage != null)
				{
					traCuuXePage.Opacity = 0.5;
				}

				EditBienSo = SelectedItem.LicensePlate;

				List<BrandCarDTO> brandCarDTOs = await BrandCarService.Ins.GetListBrandCar();
				HieuXeList = new ObservableCollection<BrandCarDTO>(brandCarDTOs);

				SelectedBrandCar = HieuXeList.FirstOrDefault(x => x.ID == SelectedItem.BrandCar.ID);

				EditChuXe = SelectedItem.Customer.Name;

				EditEmail = SelectedItem.Customer.Email;

				EditSoDienThoai = SelectedItem.Customer.PhoneNumber;

				EditDiaChi = SelectedItem.Customer.Address;

				int integerPart = (int)Math.Floor((decimal)SelectedItem.Debt);
				System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();
				nfi.NumberGroupSeparator = ",";
				nfi.NumberDecimalSeparator = ".";
				EditTienNo = integerPart.ToString("N0", nfi);

				EditXe editXe = new EditXe();
				editXe.ShowDialog();
				if (traCuuXePage != null)
				{
					traCuuXePage.Opacity = 1;
				}
			});

			SuaThongTinXe = new RelayCommand<object>(_ => true, async p =>
			{
				// Check if all fields are filled
				if (string.IsNullOrEmpty(EditBienSo) || SelectedBrandCar == null || string.IsNullOrEmpty(EditChuXe) || string.IsNullOrEmpty(EditEmail) || string.IsNullOrEmpty(EditSoDienThoai) || string.IsNullOrEmpty(EditDiaChi) || string.IsNullOrEmpty(EditTienNo))
				{
					MessageBoxCustom.Show(MessageBoxCustom.Error, "Vui lòng điền đầy đủ thông tin");
					return;
				}

				// Validate email
				try
				{
					var addr = new System.Net.Mail.MailAddress(EditEmail);
					if (addr.Address != EditEmail)
					{
						MessageBoxCustom.Show(MessageBoxCustom.Error, "Email không hợp lệ");
						return;
					}
				}
				catch
				{
					MessageBoxCustom.Show(MessageBoxCustom.Error, "Email không hợp lệ");
					return;
				}

				// Update the new information
				var (isSucess, message) = await ReceptionService.Ins.UpdateReception(SelectedItem.ID, new ReceptionDTO
				{
					ID = SelectedItem.ID,
					LicensePlate = EditBienSo,
					BrandCar = SelectedBrandCar,
					Customer = new CustomerDTO
					{
						ID = SelectedItem.Customer.ID,
						Name = EditChuXe,
						Email = EditEmail,
						PhoneNumber = EditSoDienThoai,
						Address = EditDiaChi
					},
					Debt = Convert.ToDecimal(EditTienNo.Replace(",", ""))
				});

				if (isSucess)
				{
					MessageBoxCustom.Show(MessageBoxCustom.Success, "Cập nhật thành công");
					XeList = await ReceptionService.Ins.GetAllReception();
					XeCollection = new ObservableCollection<ReceptionDTO>(XeList);
				}
				else
				{
					MessageBoxCustom.Show(MessageBoxCustom.Error, "Có lỗi xảy ra khi cập nhật");
				}
				(p as Window).Close();
			});

			DeleteXe = new RelayCommand<object>(_ => true, async p =>
			{
				Page traCuuXePage = p as Page;
				if (traCuuXePage != null)
				{
					traCuuXePage.Opacity = 0.5;
				}
				DeleteMessageBox deleteMessageBox = new DeleteMessageBox();
				deleteMessageBox.ShowDialog();
				if (traCuuXePage != null)
				{
					traCuuXePage.Opacity = 1;
				}
				if (deleteMessageBox.DialogResult == true)
				{
					var (isSucess, message) = await ReceptionService.Ins.DeleteReception(SelectedItem.ID);
					if (isSucess)
					{
						MessageBoxCustom.Show(MessageBoxCustom.Success, "Xóa thành công");
					}
					else
					{
						MessageBoxCustom.Show(MessageBoxCustom.Error, "Có lỗi xảy ra khi xóa");
					}
					XeList = await ReceptionService.Ins.GetAllReception();
					XeCollection = new ObservableCollection<ReceptionDTO>(XeList);
				}
			});

		}
	}
}
