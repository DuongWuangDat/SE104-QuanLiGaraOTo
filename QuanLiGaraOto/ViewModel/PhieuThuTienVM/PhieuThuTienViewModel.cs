using System.Timers;
using System.Windows.Input;

namespace QuanLiGaraOto.ViewModel.PhieuThuTienVM
{
	internal class PhieuThuTienViewModel : BaseViewModel
	{
		private Timer debounceTimer;
		private string searchText;

		public ICommand ThemPhieuThuTien { get; set; }

		public PhieuThuTienViewModel()
		{
			ThemPhieuThuTien = new RelayCommand<object>((p) => { return true; }, (p) =>
			{
				// Debounce search
				searchText = p as string;
				if (debounceTimer != null)
				{
					debounceTimer.Dispose();
				}

				//debounceTimer = new Timer(PerformSearch, null, 500, System.Threading.Timeout.Infinite);
				debounceTimer = new Timer
				{

				};
			});
		}

		private void PerformSearch(object state)
		{
			// Perform search logic here
			// using the searchText variable
		}
	}
}
