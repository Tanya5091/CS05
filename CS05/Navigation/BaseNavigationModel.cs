using System.Collections.Generic;
using System.Diagnostics;
using CS05.Models;

namespace CS05.Navigation
{
	internal abstract class BaseNavigationModel : INavigationModel
	{
		private readonly IContentOwner _contentOwner;
		private readonly Dictionary<ViewType, INavigatable> _viewsDictionary;

		protected BaseNavigationModel(IContentOwner contentOwner)
		{
			_contentOwner = contentOwner;
			_viewsDictionary = new Dictionary<ViewType, INavigatable>();
		}

		protected IContentOwner ContentOwner
		{
			get { return _contentOwner; }
		}

		protected Dictionary<ViewType, INavigatable> ViewsDictionary
		{
			get { return _viewsDictionary; }
		}

		public void Navigate(ViewType viewType, ProcessModel pm)
		{
			if (!ViewsDictionary.ContainsKey(viewType))
				InitializeView(viewType, pm);
			ContentOwner.ContentControl.Content = ViewsDictionary[viewType];
		}

		protected abstract void InitializeView(ViewType viewType, ProcessModel pm);

	}
}
