using QuanLiGaraOto.DTOs;
using QuanLiGaraOto.Model;
using QuanLiGaraOto.Model.service;
using QuanLiGaraOto.View.MessageBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLiGaraOto.ViewModel.PhieuThuTienVM
{
	internal class ThemPhieuThuTienViewModel : BaseViewModel
	{
		private string hoTenChuXe;

		public string HoTenChuXe
		{
			get { return hoTenChuXe; }
			set
			{
				hoTenChuXe = value;
				OnPropertyChanged();
			}
		}

		private string dienThoai;

		public string DienThoai
		{
			get { return dienThoai; }
			set { dienThoai = value; }
		}

		private string email;

		public string Email
		{
			get { return email; }
			set { email = value; OnPropertyChanged(); }
		}

		private string bienSoXe;

		public string BienSoXe
		{
			get { return bienSoXe; }
			set { bienSoXe = value; }
		}

		private DateTime ngayThuTien;

		public DateTime NgayThuTien
		{
			get { return ngayThuTien; }
			set { ngayThuTien = value; OnPropertyChanged(); }
		}

		private string tienNo;

		public string TienNo
		{
			get { return tienNo; }
			set { tienNo = value; OnPropertyChanged(); }
		}

		private decimal? tienNoNumber = -1;

		private string tienThu;

		public string TienThu
		{
			get { return tienThu; }
			set
			{
				if (tienThu != value)
				{
					tienThu = value;
					OnPropertyChanged("TienThu");
				}
			}
		}

		private string noConLai;

		public string NoConLai
		{
			get { return noConLai; }
			set { noConLai = value; OnPropertyChanged(); }
		}

		private ObservableCollection<string> bienSoXeList;
		private ObservableCollection<string> soDienThoaiList;

		public ObservableCollection<string> SoDienThoaiList
		{
			get { return soDienThoaiList; }
			set { soDienThoaiList = value; OnPropertyChanged(); }
		}

		public ObservableCollection<string> BienSoXeList
		{
			get { return bienSoXeList; }
			set { bienSoXeList = value; OnPropertyChanged(); }
		}

		public static List<ReceptionDTO> receptions;
		List<CustomerDTO> customersTrungTen;


		public ICommand ThemPhieuThuTien { get; set; }
		public ICommand FirstLoad { get; set; }

		public ICommand SelectionChangedSoDienThoai { get; set; }
		public ICommand LostFocusHoTenChuXe { get; set; }
		public ICommand LostFocusTienThu { get; set; }
		public ICommand SelectionChangedBienSoXe { get; set; }
		public ICommand KeyUpTienThu { get; set; }
		private ReceptionDTO reception;

		public bool IsValidEmail(string email)
		{
			try
			{
				var addr = new System.Net.Mail.MailAddress(email);
				return addr.Address == email;
			}
			catch
			{
				return false;
			}
		}

		public ThemPhieuThuTienViewModel()
		{
			NgayThuTien = DateTime.Now;

			FirstLoad = new RelayCommand<object>((p) => { return true; }, async (p) =>
			{
				try
				{
					receptions = await ReceptionService.Ins.GetAllReception();
					if (receptions == null)
					{
						MessageBoxCustom.Show(MessageBoxCustom.Error, "Có lỗi xảy ra khi kết nối với cơ sở dữ liệu");
					}
				}
				catch (Exception)
				{
					MessageBoxCustom.Show(MessageBoxCustom.Error, "Có lỗi xảy ra khi kết nối với cơ sở dữ liệu");
				}
			});

			LostFocusHoTenChuXe = new RelayCommand<object>((p) => { return true; }, (p) =>
			{
				TextBox textBox = p as TextBox;
				if (!string.IsNullOrEmpty(textBox.Text))
				{
					customersTrungTen = new List<CustomerDTO>();
					if (receptions != null)
					{
						foreach (ReceptionDTO reception in receptions)
						{
							if (reception.Customer.Name == textBox.Text)
							{
								customersTrungTen.Add(reception.Customer);
							}
						}
						if (customersTrungTen.Count == 0)
						{
							MessageBoxCustom.Show(MessageBoxCustom.Error, "Không tìm thấy khách hàng");
						}
						else
						{
							SoDienThoaiList = new ObservableCollection<string>();
							foreach (CustomerDTO customer in customersTrungTen)
							{
								if (!SoDienThoaiList.Contains(customer.PhoneNumber))
									SoDienThoaiList.Add(customer.PhoneNumber);
							}
						}
					}
				}
			});

			SelectionChangedSoDienThoai = new RelayCommand<object>((p) => { return true; }, (p) =>
			{
				ComboBox comboBox = p as ComboBox;
				if (comboBox.SelectedValue != null)
				{
					DienThoai = (string)comboBox.SelectedValue;
					string emailMatch = customersTrungTen.Find(x => x.PhoneNumber == (string)comboBox.SelectedValue).Email;
					if (emailMatch != null)
					{
						Email = emailMatch;
					}
					BienSoXeList = new ObservableCollection<string>();
					foreach (ReceptionDTO reception in receptions)
					{
						if (reception.Customer.PhoneNumber == (string)comboBox.SelectedValue && !BienSoXeList.Contains(reception.LicensePlate))
						{
							BienSoXeList.Add(reception.LicensePlate);
						}
					}
				}


			});

			SelectionChangedBienSoXe = new RelayCommand<object>((p) => { return true; }, (p) =>
			{
				ComboBox comboBox = p as ComboBox;
				BienSoXe = (string)comboBox.SelectedValue;
				foreach (ReceptionDTO reception in receptions)
				{
					if (reception.LicensePlate == (string)comboBox.SelectedValue)
					{
						int integerPart = (int)Math.Floor(reception.Debt.GetValueOrDefault());
						System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();
						nfi.NumberGroupSeparator = ",";
						nfi.NumberDecimalSeparator = ".";
						TienNo = integerPart.ToString("N0", nfi);
						this.reception = reception;
						break;
					}
				}
			});

			KeyUpTienThu = new RelayCommand<object>((p) => { return true; }, (p) =>
			{
				TextBox textBox = p as TextBox;
				if (!string.IsNullOrEmpty(textBox.Text))
				{
					System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo
					{
						NumberGroupSeparator = ",",
						NumberDecimalSeparator = "."
					};
					string tienThuNumber = textBox.Text.Replace(",", "");
					if (int.Parse(tienThuNumber) > int.Parse(TienNo.Replace(",", "")))
					{
						NoConLai = "";
						MessageBoxCustom.Show(MessageBoxCustom.Error, "Số tiền thu không được lớn hơn số tiền nợ");
						return;
					}
					int integerPart = (int.Parse(TienNo.Replace(",", "")) - int.Parse(tienThuNumber));
					NoConLai = integerPart.ToString("N0", nfi);
				}
			});

			ThemPhieuThuTien = new RelayCommand<object>((p) => { return true; }, async (p) =>
			{
				if(NgayThuTien > DateTime.Now)
				{
					MessageBoxCustom.Show(MessageBoxCustom.Error, "Ngày thu tiền không được lớn hơn ngày hiện tại");
					return;
				}
				if (string.IsNullOrEmpty(HoTenChuXe) || string.IsNullOrEmpty(DienThoai) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(BienSoXe) || string.IsNullOrEmpty(TienNo) || string.IsNullOrEmpty(TienThu))
				{
					MessageBoxCustom.Show(MessageBoxCustom.Error, "Vui lòng nhập đầy đủ thông tin");
					return;
				}
				if (!IsValidEmail(Email))
				{
					MessageBoxCustom.Show(MessageBoxCustom.Error, "Email không hợp lệ");
					return;
				}
				if (int.Parse(TienThu.Replace(",", "")) > int.Parse(TienNo.Replace(",", "")))
				{
					MessageBoxCustom.Show(MessageBoxCustom.Error, "Số tiền thu không được lớn hơn số tiền nợ");
					return;
				}	
				if (Email != reception.Customer.Email)
				{
					reception.Customer.Email = Email;
					var (isSuccess, message) = await CustomerService.Ins.updateUserEmail(reception.Customer.ID, Email);
					if (!isSuccess)
					{
						MessageBoxCustom.Show(MessageBoxCustom.Error, "Cập nhật email không thành công");
						return;
					}
				}
				BillDTO newBill = new BillDTO
				{
					Reception = reception,
					CreateAt = NgayThuTien,
					Proceeds = decimal.Parse(TienThu.Replace(",", ""))
				};
				try
				{
					var (isSuccess, message) = await BillService.Ins.AddNewBill(newBill);

					if (isSuccess)
					{
						MessageBoxCustom.Show(MessageBoxCustom.Success, message);
					}
					else
					{
						MessageBoxCustom.Show(MessageBoxCustom.Error, message);
					}

				}
				catch (Exception)
				{
					MessageBoxCustom.Show(MessageBoxCustom.Error, "Có lỗi xảy ra khi thêm phiếu thu tiền");
				}
				finally
				{
					HoTenChuXe = "";
					SoDienThoaiList = new ObservableCollection<string>();
					Email = "";
					BienSoXeList = new ObservableCollection<string>();
					NgayThuTien = DateTime.Now;
					TienNo = "";
					TienThu = "";
					NoConLai = "";
				}
			});
		}
	}
}
