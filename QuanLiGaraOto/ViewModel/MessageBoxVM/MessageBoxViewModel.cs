namespace QuanLiGaraOto.ViewModel.MessageBoxVM
{
	internal class MessageBoxViewModel
	{
		private string text;

		public string Text
		{
			get { return text; }
			set { text = value; }
		}
		public MessageBoxViewModel(string _text)
		{
			text = _text;
		}
	}
}
