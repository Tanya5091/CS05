using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using CS05.Managers;
using CS05.Navigation;
using CS05.ViewModels;

namespace CS05
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, IContentOwner
	{
		public static event Action StopThreads;
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new MainWindowViewModel();
			NavigationManager.Instance.Initialize(new InitializationNavigationModel(this));
			NavigationManager.Instance.Navigate(ViewType.ProcessList, null);
		}

		public ContentControl ContentControl
		{
			get { return _contentControl; }
		}
		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			StopThreads?.Invoke();
			Environment.Exit(0);
		}
	}
}
