using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CS05.Managers;
using CS05.Models;
using CS05.Navigation;
using CS05.Tools;
using Path = System.IO.Path;

namespace CS05.ViewModels
{
	
	internal class ProcessListViewModel: INotifyPropertyChanged
	{
		
		private ProcessModel _chosenProcess;
		private string _propertyChoice;
		private ObservableCollection<ProcessModel> _processCollection;
		private ProcessModuleCollection _moduleCollection;
		private ProcessThreadCollection _threadCollection;
		private RelayCommand<object> _closeProcessCommand;
		private RelayCommand<object> _openFolderCommand;
		public ProcessListViewModel()
		{
			ProcessCollection=new ObservableCollection<ProcessModel>();
			Process[] processes = Process.GetProcesses();
			foreach (Process p in processes)
			{
				ProcessCollection.Add(new ProcessModel(p));
			}
			new Thread(RefreshProcesses).Start();
			new Thread(RefreshMeta).Start();

			try
			{
				ThreadCollection = ChosenProcess.CurrentProcess.Threads;

			}
			catch
			{
				ThreadCollection = null;
			}

			try
			{
				ModuleCollection = ChosenProcess.CurrentProcess.Modules;
			}
			catch
			{
				ModuleCollection = null;
			}
		}

		public ProcessModuleCollection ModuleCollection
		{
			get { return _moduleCollection; }
			private set { _moduleCollection = value; OnPropertyChanged(); }
		}
		public ProcessThreadCollection ThreadCollection
		{
			get { return _threadCollection; }
			private set { _threadCollection = value; OnPropertyChanged(); }
		}

		

	
		public ObservableCollection<ProcessModel> ProcessCollection
		{
			get { return _processCollection; }
			set { _processCollection = value; OnPropertyChanged();}
		}

		public  ProcessModel ChosenProcess
		{
			get { return _chosenProcess; }
			set
			{
				_chosenProcess = value;
				try
				{
					ThreadCollection = ChosenProcess.CurrentProcess.Threads;

				}
				catch
				{
					ThreadCollection = null;
				}

				try
				{
					ModuleCollection = ChosenProcess.CurrentProcess.Modules;
				}
				catch
				{
					ModuleCollection = null;
				}
				OnPropertyChanged();
			}


		}

		public string PropertyChoice
		{
			get { return _propertyChoice; }
			set
			{
				_propertyChoice = value;
				SortCollection(ProcessCollection);
			}
		}


		private void RefreshMeta()
		{
			while (true)
			{
				foreach (ProcessModel process in ProcessCollection)
					process.Refresh();
				ProcessCollection = new ObservableCollection<ProcessModel>(ProcessCollection);
				for(int i=0; i<10; i++)
					Thread.Sleep(100);
			}
		}
		private void RefreshProcesses()
		{
			while (true)
			{
				

				ObservableCollection<ProcessModel> collect=new ObservableCollection<ProcessModel>();
				Process[] processes = Process.GetProcesses();
				foreach (Process process in processes)
				{
					ProcessModel added = new ProcessModel(process);
					collect.Add(added);
					
				}

				SortCollection(ProcessCollection);
				for(int i=0; i<20; i++)
				Thread.Sleep(100);
			}
		}


		private async void SortCollection(ObservableCollection<ProcessModel> processes)
		{
		
			if (PropertyChoice == null)
				return;
			if (PropertyChoice.Contains("ID"))
			{
				await Task.Run(() =>
								ProcessCollection = new ObservableCollection<ProcessModel>(_processCollection.OrderBy(p => p.Id)));
			}
			else if (PropertyChoice.Contains("Name"))
			{
				await Task.Run(() =>
					ProcessCollection = new ObservableCollection<ProcessModel>(_processCollection.OrderBy(p => p.Name)));
			}
			else if (PropertyChoice.Contains("Start Time"))
			{
				await Task.Run(() =>
				ProcessCollection =
					new ObservableCollection<ProcessModel>(_processCollection.OrderBy(p => p.StartTime)));
			}
			else if (PropertyChoice.Contains("CPU (%)"))
			{
				await Task.Run(() =>
				   ProcessCollection =
					new ObservableCollection<ProcessModel>(_processCollection.OrderBy(p => p.Cpu)));
			}
			else if (PropertyChoice.Contains("RAM (%)"))
			{
				await Task.Run(() =>
				   ProcessCollection =
					new ObservableCollection<ProcessModel>(_processCollection.OrderBy(p => p.RamPercentage)));
			}
			else if (PropertyChoice.Contains("RAM"))
			{
				await Task.Run(() =>
				   ProcessCollection =
					new ObservableCollection<ProcessModel>(_processCollection.OrderBy(p => p.Ram)));
			}
			else if (PropertyChoice.Contains("Path"))
			{
				await Task.Run(() =>
				   ProcessCollection = new ObservableCollection<ProcessModel>(_processCollection.OrderBy(p => p.Path)));
			}
			else if (PropertyChoice.Contains("User"))
			{
				await Task.Run(() =>
				   ProcessCollection =
					new ObservableCollection<ProcessModel>(_processCollection.OrderBy(p => p.User)));
			}
			else if (PropertyChoice.Contains("Threads"))
			{
				await Task.Run(() =>
				ProcessCollection =
					new ObservableCollection<ProcessModel>(_processCollection.OrderBy(p => p.Threads)));
			}
			else
			{
				return;
			}

	}
		public RelayCommand<object> CloseProcessCommand =>
			_closeProcessCommand ?? (_closeProcessCommand = new RelayCommand<object>(CloseProcessImplementation));

		private void CloseProcessImplementation(object obj)
		{
			try
			{
				ChosenProcess.CurrentProcess.Kill();

			}
			catch
			{
				MessageBox.Show("Could not close this process", "Fail");
			}
			
		}

		public RelayCommand<object> OpenFolderCommand =>
			_openFolderCommand ?? (_openFolderCommand = new RelayCommand<object>(OpenFolderImplementation));

		private void OpenFolderImplementation(object obj)
		{
			try
			{
				if (ChosenProcess.Path.Equals("N/A"))
				throw new FileNotFoundException();
				Process.Start(Path.GetDirectoryName(ChosenProcess.Path));
			}
			catch
			{
				MessageBox.Show("Folder path is unavailable", "Couldn`t open folder");
			}
			
		}



		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
