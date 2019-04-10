using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading.Tasks;


namespace CS05.Models
{	
	public class Process
	{
		private static readonly long _deviceCPU = Environment.ProcessorCount;
		private static readonly long _deviceRAM = PerformanceInfo.GetTotalMemory();
		private Process _process;
		private string _name;
		private int _id;
		private bool _isActive;
		private double _cpu;
		private double _ram;
		private int _threads;
		private string _user;
		private string _path;
		private DateTime _startTime;
		private PerformanceCounter _randAcc;
		private PerformanceCounter _centralProc;

		
		public Process(Process process)
		{
			Process = process;
			Name = process.ProcessName;
			Id = process.Id;
			IsActive = process.Responding;
			RandAcc = new PerformanceCounter("Process", "Working Set", Process.ProcessName);
			CentralProc = new PerformanceCounter("Process", "% Processor Time", Process.ProcessName);
			Ram = RandAcc.NextValue()/ DeviceRAM;
			Cpu = CentralProc.NextValue() / DeviceCPU;
			Threads = Process.Threads.Length;
			StartTime = Process.StartTime;
			try
			{
				User = Process.MachineName;
			}
			catch (Exception e)
			{
				System.Console.WriteLine(e);
				User = "N/A";
			}
			try
			{
				Path = Process.MainModule.FileName;
			}
			catch (Exception e)
			{
				Path = "N/A";
				System.Console.WriteLine(e);
				throw;
			}
		}
		public static long DeviceCPU => _deviceCPU;
		public static long DeviceRAM => _deviceRAM;
		public string Name
		{
			get => _name;
			set => _name = value;
		}

		public int Id
		{
			get => _id;
			set => _id = value;
		}

		public bool IsActive
		{
			get => _isActive;
			set => _isActive = value;
		}

		public double Cpu
		{
			get => _cpu;
			set => _cpu = value;
		}

		public double Ram
		{
			get => _ram;
			set => _ram = value;
		}

		public int Threads
		{
			get => _threads;
			set => _threads = value;
		}

		public string User
		{
			get => _user;
			set => _user = value;
		}

		public string Path
		{
			get => _path;
			set => _path = value;
		}

		public DateTime StartTime
		{
			get => _startTime;
			set => _startTime = value;
		}

		public Process Process
		{
			get => _process;
			set => _process = value;
		}

		public PerformanceCounter RandAcc
		{
			get => _randAcc;
			set => _randAcc = value;
		}

		public PerformanceCounter CentralProc
		{
			get => _centralProc;
			set => _centralProc = value;
		}
	}
}