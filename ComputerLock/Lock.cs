using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace PatronLock.ComputerLock
{
	/// <summary>
	/// Class to lock the system down until (programmatically) opened
	/// </summary>
	public class Lock
	{
		#region Imports
		// http://tamaspiros.co.uk/2007/12/20/c-disable-ctrl-alt-del-alt-tab-alt-f4-start-menu-and-so-on/
		
		/// <summary>
		/// C# struct matching the Win32 KBDLLHOOKSTRUCT
		/// </summary>
		public struct KBDLLHOOKSTRUCT
		{
			/// <summary>
			/// Virtual key code -- http://msdn.microsoft.com/en-us/library/dd375731(v=vs.85).aspx
			/// </summary>
			public int vkCode;

			/// <summary>
			/// Scan code -- http://msdn.microsoft.com/en-us/library/ms644967(v=vs.85).aspx
			/// </summary>
			public int scanCode;

			/// <summary>
			/// Flags -- http://msdn.microsoft.com/en-us/library/ms644967(v=vs.85).aspx
			/// </summary>
			public int flags;

			/// <summary>
			/// Time -- http://msdn.microsoft.com/en-us/library/ms644967(v=vs.85).aspx
			/// </summary>
			public int time;

			/// <summary>
			/// Extra information -- http://msdn.microsoft.com/en-us/library/ms644967(v=vs.85).aspx
			/// </summary>
			public int dwExtraInfo;
		}

		private static int intLLKey;
		private const int SW_HIDE = 0;
		private const int SW_SHOW = 1;

		/// <summary>
		/// Import for the <c>SetWindowsHookEx</c> API call
		/// </summary>
		/// <param name="idHook">hook ID</param>
		/// <param name="lpfn">delegate to handle the processing</param>
		/// <param name="hMod">unknown</param>
		/// <param name="dwThreadId">thread ID</param>
		/// <returns>unknown</returns>
		[DllImport("user32", EntryPoint = "SetWindowsHookExA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int SetWindowsHookEx(int idHook, LowLevelKeyboardProcDelegate lpfn, int hMod, int dwThreadId);
        
		/// <summary>
		/// Import for the <c>UnhookWindowsHookEx</c> API call
		/// </summary>
		/// <param name="hHook">handle to a hook</param>
		/// <returns>unknown</returns>
		[DllImport("user32", EntryPoint = "UnhookWindowsHookEx", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int UnhookWindowsHookEx(int hHook);
        
		/// <summary>
		/// Delegate for <c>SetWindowsHookEx</c>
		/// </summary>
		/// <param name="nCode">node</param>
		/// <param name="wParam">keyboard message identifier (WM_KEYDOWN, WM_KEYUP, WM_SYSKEYDOWN, WM_SYSKEYUP)</param>
		/// <param name="lParam"><see cref="KBDLLHOOKSTRUCT"/> object</param>
		/// <returns><c>1</c> to eat the keypress, else result of <c>CallNextHookEx()</c></returns>
		public delegate int LowLevelKeyboardProcDelegate(int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);
        
		/// <summary>
		/// Import for the <c>CallNextHookEx</c> API call; used with <see cref="KBDLLHOOKSTRUCT"/> here
		/// </summary>
		/// <param name="hHook">handle to a hook</param>
		/// <param name="nCode">node</param>
		/// <param name="wParam">keyboard message identifier (WM_KEYDOWN, WM_KEYUP, WM_SYSKEYDOWN, WM_SYSKEYUP)</param>
		/// <param name="lParam"><see cref="KBDLLHOOKSTRUCT"/> object</param>
		/// <returns>unknown</returns>
		[DllImport("user32", EntryPoint = "CallNextHookEx", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int CallNextHookEx(int hHook, int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);

		/// <summary>
		/// Gets the key state of a specific keycode
		/// </summary>
		/// <param name="keyCode">a virtual keycode</param>
		/// <returns></returns>
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
		public static extern short GetKeyState(int keyCode);

		/// <summary>
		/// ID for the low-level keyboard hook
		/// </summary>
		public const int WH_KEYBOARD_LL = 13;
 
        /* functions to disable taskbar start menu */
        [DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);
		[DllImport("user32.dll")]
		private static extern IntPtr FindWindowEx(IntPtr parentHwnd, IntPtr childAfterHwnd, IntPtr className, string windowText);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int command);
		#endregion Imports

		#region Data Members
		private bool allowSpecialKeystrokes;
		private bool allowTaskManager;
		private bool showStartOrb;
		private LowLevelKeyboardProcDelegate del;
		private Control enableMe;
		#endregion Data Members

		#region Properties
		/// <summary>
		/// Whether or not to allow special keystrokes such as Alt+Tab and the Windows key
		/// </summary>
		public bool AllowSpecialKeystrokes
		{
			get { return this.allowSpecialKeystrokes; }
			set
			{
				this.allowSpecialKeystrokes = value;
				this.ToggleSpecialKeystrokes(value);
			}
		}

		/// <summary>
		/// Whether or not to allow the user to start the Task Manager
		/// </summary>
		public bool AllowTaskManager
		{
			get { return this.allowTaskManager; }
			set
			{
				this.allowTaskManager = value;
				this.ToggleTaskManager(value);
			}
		}

		/// <summary>
		/// Whether or not to show the start orb (start button)
		/// </summary>
		public bool ShowStartOrb
		{
			get { return this.showStartOrb; }
			set
			{
				this.showStartOrb = value;
				this.ToggleStartOrb(value);
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Creates a new <see cref="Lock"/>
		/// </summary>
		public Lock()
		{
			this.enableMe = null;
		}
		
		/// <summary>
		/// Creates a new <see cref="Lock"/>
		/// </summary>
		/// <param name="enableControl">the <see cref="Control"/>, if not <c>null</c> to enable and make visible upon unlocking; <see cref="Open()"/> uses it.</param>
		public Lock(Control enableControl)
		{
			this.enableMe = enableControl;
		}
		#endregion Constructors

		#region Public Methods
		/// <summary>
		/// Unlocks the computer
		/// </summary>
		/// <returns><c>true</c> on success, <c>false</c> on error</returns>
		public bool Open()
		{
			bool success = true;

			success &= this.ToggleSpecialKeystrokes(true);
			success &= this.ToggleTaskManager(true);
			success &= this.ToggleStartOrb(true);

			// If the caller specified a control to enable and make
			// visible, do so
			if (this.enableMe != null)
			{
				this.enableMe.Visible = true;
				this.enableMe.Enabled = true;
			}

			return success;
		}

		/// <summary>
		/// Locks the computer
		/// </summary>
		/// <returns><c>true</c> on success, <c>false</c> on error</returns>
		public bool Close()
		{
			bool success = true;

			success &= this.ToggleSpecialKeystrokes(false);
			success &= this.ToggleTaskManager(false);
			success &= this.ToggleStartOrb(false);

			return success;
		}
		#endregion Public Methods

		#region Private Methods
		/// <summary>
		/// Toggles allowing the Task Manager to be run
		/// </summary>
		/// <param name="enable"><c>true</c> to allow Task Manager, <c>false</c> to disallow</param>
		/// <returns><c>true</c> on success, <c>false</c> on error</returns>
		private bool ToggleTaskManager(bool enable)
		{
			bool success = false;

			RegistryKey key;
			int value = (enable ? 0 : 1);
			string path = @"Software\Microsoft\Windows\CurrentVersion\Policies\System";

			try
			{
				key = Registry.CurrentUser.CreateSubKey(path);
				key.SetValue("DisableTaskMgr", value);
				key.Close();

				success = true;
			}
			catch (Exception e)
			{
				success = false;
				System.Windows.Forms.MessageBox.Show("Error: " + e.Message);
			}

			return success;
		}

		/// <summary>
		/// Toggles allowing Alt+Tab, Alt+Esc, Ctrl+Esc, and the Windows key to be used
		/// </summary>
		/// <param name="enable"><c>true</c> to allow the keystrokes, <c>false</c> to disallow</param>
		/// <returns><c>true</c> on success, <c>false</c> on error</returns>
		private bool ToggleSpecialKeystrokes(bool enable)
		{
			bool success = false;

			// Enable/disable alt+tab/alt+esc/ctrl+esc/win
			if (enable)
			{
				UnhookWindowsHookEx(intLLKey);
			}
			else
			{
				this.del = KeyboardProcessor;

				Module module = Assembly.GetExecutingAssembly().GetModules()[0];
				IntPtr hInstance = Marshal.GetHINSTANCE(module);
				intLLKey = SetWindowsHookEx(WH_KEYBOARD_LL, del, hInstance.ToInt32(), 0);
			}
				
            return success;
		}

		/// <summary>
		/// Toggles showing the start orb
		/// </summary>
		/// <param name="enable"><c>true</c> to make the orb visible, <c>false</c> to make it not visible</param>
		/// <returns><c>true</c> on success, <c>false</c> on error</returns>
		private bool ToggleStartOrb(bool enable)
		{
			try
			{
				// Enable/disable the taskbar
				int hwnd = FindWindow("Shell_TrayWnd", "");
				ShowWindow(hwnd, (enable ? SW_SHOW : SW_HIDE));

				// Enable/disable the orb
				int hOrb = (int)FindWindowEx(IntPtr.Zero, IntPtr.Zero, (IntPtr)0xC017, null);
				ShowWindow(hOrb, (enable ? SW_SHOW : SW_HIDE));

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		/// <summary>
		/// The callback function that is used to block special keystrokes
		/// </summary>
		/// <param name="nCode">the hook code</param>
		/// <param name="wParam">unknown</param>
		/// <param name="lParam">information on what key(s) were pressed</param>
		/// <returns><c>1</c> to ignore the keypress, or the return value of <c>CallNextHookEx()</c></returns>
		/// <remarks><a href="http://msdn.microsoft.com/en-us/library/ms644974(v=vs.85).aspx">Unhelpful, general function documentation</a> is available</remarks>
		private int KeyboardProcessor(int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam)
		{
			bool bEat = false;
			bool bEscapeHatch = false;
			bool bControl = false;

			// KBDLLHOOKSTRUCT Flags
			int FLAG_ALT = 32;

			// Virtual Key Codes -- http://msdn.microsoft.com/en-us/library/dd375731(v=vs.85).aspx
			int VK_TAB = 0x09;
			int VK_RETURN = 0x0D;
			int VK_CONTROL = 0x11;
			int VK_ESCAPE = 0x1B;
			//int VK_KEY_C = 0x43;
			int VK_LWIN = 0x5B;
			int VK_RWIN = 0x5C;

			switch (wParam)
			{
				case 256:
				case 257:
				case 260:
				case 261:
					bEat = ((lParam.vkCode == VK_TAB) && (lParam.flags == FLAG_ALT)) | ((lParam.vkCode == VK_ESCAPE) && (lParam.flags == FLAG_ALT)) /* Alt+Esc */ | ((lParam.vkCode == VK_ESCAPE) && (lParam.flags == 0)) /* Ctrl+Esc */ | ((lParam.vkCode == VK_LWIN) && (lParam.flags == 1)) /* (left) Windows key */ | ((lParam.vkCode == VK_RWIN) && (lParam.flags == 1)) /* (right) Windows key */; // Alt+Tab/Alt+Esc/Ctrl+Esc/Win
					
					bControl = ((GetKeyState(VK_CONTROL) & 0x8000) != 0);
					bEscapeHatch = bControl && (lParam.vkCode == VK_RETURN && lParam.flags == 0); // Ctrl+Enter
					
					break;
			}

			if (bEscapeHatch)
			{
				// Deal with the scape hatch method if wanted
				if (this.enableMe != null)
				{
					this.enableMe.Visible = true;
					this.enableMe.Enabled = true;

					return 1;
				}

				return CallNextHookEx(0, nCode, wParam, ref lParam);
			}
			else if (bEat)
				return 1;
			else
				return CallNextHookEx(0, nCode, wParam, ref lParam);
		}
		#endregion Private Methods
	}
}
