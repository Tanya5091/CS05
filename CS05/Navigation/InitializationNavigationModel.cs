using System;
using CS05;
using CS05.Models;
using CS05.Navigation;
using CS05.Views;


namespace CS05.Navigation
{
	internal class InitializationNavigationModel : BaseNavigationModel
	{
		public InitializationNavigationModel(IContentOwner contentOwner) : base(contentOwner)
		{

		}

		protected override void InitializeView(ViewType viewType, ProcessModel pm=null)
		{
			switch (viewType)
			{
				case ViewType.ProcessList:
					ViewsDictionary.Add(viewType, new ProcessListView());
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
			}
		}
	}
}
