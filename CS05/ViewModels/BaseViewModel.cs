using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CS05.ViewModels

{
	internal abstract class BaseViewModel : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)

		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion

	}

}