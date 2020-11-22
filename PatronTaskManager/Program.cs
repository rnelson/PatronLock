using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Xml;
using Microsoft.Win32.TaskScheduler;

namespace PatronTaskManager
{
	/// <summary>
	/// Entry point for the PatronLock Scheduled Task Manager
	/// </summary>
	public class Program
	{
		#region Public Methods
		/// <summary>
		/// Entry
		/// </summary>
		/// <param name="args">command-line arguments</param>
		public static void Main(string[] args)
		{
			if (args.Length != 1)
				Program.Usage();

			string arg = args[0].ToLower().Trim();
			if (arg == "create")
				Program.CreateScheduledTask();
			else if (arg == "remove")
				Program.RemoveScheduledTask();
			else
				Program.Usage();
		}
		#endregion Public Methods

		#region Private Methods
		/// <summary>
		/// Displays program usage information
		/// </summary>
		public static void Usage()
		{
			Console.Error.WriteLine("usage: PatronTaskManager.exe <create|remove>");
			Console.ReadLine();
			return;
		}

		/// <summary>
		/// Creates a scheduled task with the default name
		/// </summary>
		public static void CreateScheduledTask()
		{
			Program.CreateScheduledTask("");
		}

		/// <summary>
		/// Creates a scheduled task with the specified name
		/// </summary>
		/// <param name="taskName">name for the task</param>
		public static void CreateScheduledTask(string taskName)
		{
			if (taskName == null || taskName == String.Empty)
				taskName = @"PatronLock";

			using (TaskService service = new TaskService())
			{
				// Create the basic task
				TaskDefinition task = service.NewTask();
				task.RegistrationInfo.Description = "Start PatronLock on logon";
				task.RegistrationInfo.Author = "CORS, University of Nebraska-Lincoln";

				// Start at logon
				LogonTrigger trigger = new LogonTrigger();
				trigger.Delay = new TimeSpan((long)0);
				task.Triggers.Add(trigger);

				// Runs with the highest possible privileges
				task.Principal.RunLevel = TaskRunLevel.Highest;

				// Set the binary
				string binary = Path.Combine(Directory.GetCurrentDirectory(), "PatronLock.exe");
				task.Actions.Add(new ExecAction(binary, null, Directory.GetCurrentDirectory()));

				// Register the task
				service.RootFolder.RegisterTaskDefinition(taskName, task);
			}
		}

		/// <summary>
		/// Removes a scheduled task with the default name
		/// </summary>
		public static void RemoveScheduledTask()
		{
			Program.RemoveScheduledTask("");
		}

		/// <summary>
		/// Removes a scheduled task with the specified name
		/// </summary>
		/// <param name="taskName">name for the task</param>
		public static void RemoveScheduledTask(string taskName)
		{
			if (taskName == null || taskName == String.Empty)
				taskName = @"PatronLock";

			using (TaskService service = new TaskService())
			{
				// Unegister the task
				service.RootFolder.DeleteTask(taskName);
			}
		}
		#endregion Private Methods
	}
}
