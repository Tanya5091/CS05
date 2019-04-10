﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CS05.Navigation;
using CS05.ViewModels;

namespace CS05.Views
{
	/// <summary>
	/// Interaction logic for ProcessListView.xaml
	/// </summary>
	public partial class ProcessListView : UserControl, INavigatable
	{
		public ProcessListView()
		{
			InitializeComponent();
			DataContext = new ProcessListViewModel();
		}
	}
}
