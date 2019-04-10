using System;
using Microsoft.VisualBasic.Devices;
using System.Diagnostics;

namespace CS05.Models
{	
	internal class ProcessModel
	{
		private static readonly long _deviceCPU = Environment.ProcessorCount;
		private static ComputerInfo CI = new ComputerInfo();
		private static readonly ulong _deviceRAM =CI.TotalPhysicalMemory;
		private Process _currentProcess;
		private string _name;
		private bool _isActive;
		private double _cpu;
		private double _ramPercentage;
		private double _ram;
		private int _threads;
		private string _user;
		private string _path;
		private string _startTime;
		private PerformanceCounter _randAcc;
		private PerformanceCounter _centralProc;


		internal ProcessModel(Process process)
		{
			CurrentProcess = process;
			Name = CurrentProcess.ProcessName;
			Id = CurrentProcess.Id;
			IsActive = CurrentProcess.Responding;
			RandAcc = new PerformanceCounter("Process", "Working Set", CurrentProcess.ProcessName);
			CentralProc = new PerformanceCounter("Process", "% Processor Time", CurrentProcess.ProcessName);
			try
			{
				Ram = RandAcc.NextValue();
				RamPercentage = Ram * 100 / DeviceRAM;
				Ram /= 1024;
			}
			catch { }

			try
			{
				Cpu = CentralProc.NextValue() / DeviceCPU;
			}
			catch { }
			Threads = CurrentProcess.Threads.Count;
			
		try

		{
				User = Environment.UserName;
			}
			catch
			{
				User = "N/A";
			}
			try
			{
				Path = CurrentProcess.MainModule.FileName;
			}
			catch (Exception e)
			{
				Path = "N/A";
				System.Console.WriteLine(e);
				
			}

			try
			{
				StartTime = CurrentProcess.StartTime.ToString();
			}
			catch 
			{
				StartTime="N/A";
			}
		}

		internal void Refresh()
		{
			try
			{
				Ram = RandAcc.NextValue();
				RamPercentage = Ram * 100 / DeviceRAM;
				Ram /= 1024;
			}
			catch
			{
			}

			try
			{

				Cpu = CentralProc.NextValue() / DeviceCPU;
			}
			catch { }

			IsActive = CurrentProcess.Responding;
			Threads = CurrentProcess.Threads.Count;

		}
		public static long DeviceCPU => _deviceCPU;
		public static ulong DeviceRAM => _deviceRAM;
		public string Name
		{
			get => _name;
		private 	set => _name = value;
		}

		public int Id { get; set; }

		public bool IsActive
		{
			get => _isActive;
		private 	set => _isActive = value;
		}

		public double Cpu
		{
			get => _cpu;
		private 	set => _cpu = value;
		}

		public double Ram
		{
			get => _ram;
		private 	set => _ram = value;
		}

		public int Threads
		{
			get => _threads;
		private 	set => _threads = value;
		}

		public string User
		{
			get => _user;
		private 	set => _user = value;
		}

		public string Path
		{
			get => _path;
		private 	set => _path = value;
		}

		public string StartTime
		{
			get => _startTime;
		private	set => _startTime = value;
		}

		public Process CurrentProcess
		{
			get => _currentProcess;
		private 	set => _currentProcess = value;
		}

		public PerformanceCounter RandAcc
		{
			get => _randAcc;
			private set => _randAcc = value;
		}

		public PerformanceCounter CentralProc
		{
			get => _centralProc;
		private 	set => _centralProc = value;
		}

		public double RamPercentage
		{
			get { return _ramPercentage; }
			private set { _ramPercentage = value; }
		}
	}
}