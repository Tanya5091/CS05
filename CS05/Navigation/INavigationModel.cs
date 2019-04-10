using CS05.Models;

namespace CS05.Navigation
{
	internal enum ViewType
	{
		ProcessList
	}

	interface INavigationModel
	{
		void Navigate(ViewType viewType, ProcessModel pm);
	}
}
