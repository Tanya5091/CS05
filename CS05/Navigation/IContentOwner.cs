using System.Windows.Controls;

namespace CS05.Navigation
{
	internal interface IContentOwner
	{
		ContentControl ContentControl { get; }
	}
}
