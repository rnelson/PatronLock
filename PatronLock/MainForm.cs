using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using PatronLock.ComputerLock;
using PatronLock.PatronApi;

namespace PatronLock
{
	/// <summary>
	/// The main window class
	/// </summary>
	public partial class MainForm : Form
	{
		// The override password; use Ctrl+Enter to get the prompt
		private const string PASSWORD = "Pbj4Lc";

		#region Imports
		/// <summary>
		/// Logs the user off of Windows
		/// </summary>
		/// <param name="uFlags">flags -- http://msdn.microsoft.com/en-us/library/aa376868(v=vs.85).aspx</param>
		/// <param name="dwReason">shutdown reason code -- http://msdn.microsoft.com/en-us/library/aa376885(v=vs.85).aspx</param>
		/// <returns><c>0</c> on error (call <c>GetLastError()</c> for info), non-zero on success</returns>
		[DllImport("user32.dll")]
		public static extern int ExitWindowsEx(int uFlags, int dwReason);
		#endregion Imports

		#region Data Members
		private Lock _lock;
		#endregion Data Members

		#region Constructors
		/// <summary>
		/// Creates a new <see cref="MainForm"/> and locks down the computer
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
			this.Fullscreen();

			// Wire the KeyPress events on the textboxes
			this.nuidTextBox.KeyPress += this.nuidTextBox_KeyPress;
			this.passwordTextBox.KeyPress += this.passwordTextBox_KeyPress;

			// Lock things down
			this._lock = new Lock(this.passwordPanel);
			this._lock.Close();
			this.exitButton.Enabled = false;
		}
		#endregion Constructors

		#region Event Handlers
		/// <summary>
		/// Handles the user pressing enter in the NUID textbox
		/// </summary>
		/// <param name="sender">the object sending the event</param>
		/// <param name="e">a <see cref="KeyPressEventArgs"/> object for the event</param>
		private void nuidTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e != null && e.KeyChar == (char)13)
				this.Go();
		}

		/// <summary>
		/// Handles the user pressing the Login button
		/// </summary>
		/// <param name="sender">the object sending the event</param>
		/// <param name="e">a <see cref="EventArgs"/> object for the event</param>
		private void loginButton_Click(object sender, EventArgs e)
		{
			this.Go();
		}

		/// <summary>
		/// Disallows the user to exit the application
		/// </summary>
		/// <param name="sender">the object sending the event</param>
		/// <param name="e">a <see cref="FormClosingEventArgs"/> object for the event</param>
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			base.OnClosing(e);
		}

		/// <summary>
		/// Handles the user pressing the Exit button
		/// </summary>
		/// <param name="sender">the object sending the event</param>
		/// <param name="e">a <see cref="EventArgs"/> object for the event</param>
		private void exitButton_Click(object sender, EventArgs e)
		{
			this.Exit();
		}

		/// <summary>
		/// Handles the user clicking the OK button
		/// </summary>
		/// <param name="sender">the object that sent the event</param>
		/// <param name="e"><see cref="EventArgs"/> for the event</param>
		private void okButton_Click(object sender, EventArgs e)
		{
			this.CheckPassword();
		}

		/// <summary>
		/// Handles the user clicking the Cancel button
		/// </summary>
		/// <param name="sender">the object that sent the event</param>
		/// <param name="e"><see cref="EventArgs"/> for the event</param>

		private void cancelButton_Click(object sender, EventArgs e)
		{
			// Pretend this never happened...
			this.passwordPanel.Visible = false;
			this.passwordPanel.Enabled = false;
			this.passwordTextBox.Text = "";

			// Force focus back to the NUID text box
			this.nuidTextBox.Focus();
		}

		/// <summary>
		/// Handles the user pressing enter in the password textbox
		/// </summary>
		/// <param name="sender">the object sending the event</param>
		/// <param name="e">a <see cref="KeyPressEventArgs"/> object for the event</param>
		private void passwordTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e != null && e.KeyChar == (char)13)
				this.CheckPassword();
		}

		/// <summary>
		/// Handles the mouse entering the Logoff button
		/// </summary>
		/// <param name="sender">the object sending the event</param>
		/// <param name="e"><see cref="EventArgs"/> for the event</param>
		private void logoffButton_MouseEnter(object sender, EventArgs e)
		{
			this.logoffButton.ImageIndex = 1;
		}

		/// <summary>
		/// Handles the mouse leaving the Logoff button
		/// </summary>
		/// <param name="sender">the object sending the event</param>
		/// <param name="e"><see cref="EventArgs"/> for the event</param>
		private void logoffButton_MouseLeave(object sender, EventArgs e)
		{
			this.logoffButton.ImageIndex = 0;
		}

		/// <summary>
		/// Handles the user clicking the Logoff button
		/// </summary>
		/// <param name="sender">the object sending the event</param>
		/// <param name="e"><see cref="EventArgs"/> for the event</param>
		private void logoffButton_Click(object sender, EventArgs e)
		{
			ExitWindowsEx(4, 0);
		}
		#endregion Event Handlers

		#region Private Methods
		/// <summary>
		/// Makes the application fullscreen and above all other apps
		/// </summary>
		private void Fullscreen()
		{
			Screen PrimaryScreen = Screen.AllScreens[0];

			this.Location = new Point(0, 0);
			this.FormBorderStyle = FormBorderStyle.None;
			this.Width = PrimaryScreen.Bounds.Width;
			this.Height = PrimaryScreen.Bounds.Height;
			this.TopMost = true;
		}

		/// <summary>
		/// Exits the application, allowing the user to use the computer
		/// </summary>
		private void Exit()
		{
			this.FormClosing -= this.MainForm_FormClosing;
			Application.Exit();
		}

		/// <summary>
		/// Reads the specified ID and attempts to verify the user; on success,
		/// the program is exited
		/// </summary>
		private void Go()
		{
			Patron p = null;
			int nuid = 0;
			bool success = Int32.TryParse(this.nuidTextBox.Text, out nuid);

			if (success)
			{
				this.Cursor = Cursors.WaitCursor;

				Api api = new Api();
				p = api.GetPatron(nuid);

				this.Cursor = Cursors.Default;
			}
			if (p == null)
			{
				statusTextLabel.ForeColor = Color.Red;
				statusTextLabel.Text = "Invalid user.";
			}
			else
			{
				// Get the two values we care about
				string name = p["PATRN NAME"];
				string status = this.GetStatus(p["P TYPE"]);
				string message = name + " (" + status + ")";

				statusTextLabel.ForeColor = Color.Green;
				statusTextLabel.Text = message;

				this._lock.Open();
				this.exitButton.Visible = true;
				this.exitButton.Enabled = true;
				this.Exit();
			}
		}

		/// <summary>
		/// Converts the type (an integer in string form) to a description (e.g., "Grad Student")
		/// </summary>
		/// <param name="type">the type</param>
		/// <returns>a textual description of that type</returns>
		private string GetStatus(string type)
		{
			int status;
			bool success = Int32.TryParse(type, out status);

			if (success)
				return this.GetStatus(status);
			else
				return "Unknown";
		}

		/// <summary>
		/// Converts the type (an integer) to a description (e.g., "Grad Student")
		/// </summary>
		/// <param name="type">the type</param>
		/// <returns>a textual description of that type</returns>
		private string GetStatus(int type)
		{
			string status = "";

			switch (type)
			{
				case 11:
				case 12:
				case 13:
					status = "Staff";
					break;
				case 1:
				case 2:
				case 3:
					status = "Faculty";
					break;
				case 102:
					status = "Library Staff";
					break;
				case 101:
					status = "Library Faculty";
					break;
				case 21:
				case 22:
				case 23:
					status = "Grad Student";
					break;
				case 31:
				case 33:
					status = "Undergrad";
					break;
				default:
					status = "Unknown";
					break;
			}

			return status;
		}

		/// <summary>
		/// Checks to see if the user entered the override password correctly
		/// </summary>
		private void CheckPassword()
		{
			// Select the entire password
			//this.passwordTextBox.SelectAll();

			// Verify the password
			if (this.passwordTextBox.Text == PASSWORD)
			{
				this._lock.Open();
				this.exitButton.Visible = true;
				this.exitButton.Enabled = true;
				this.Exit();
			}
			else
			{
				MessageBox.Show("Sorry, that is not the correct password.", "NUID", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		#endregion Private Methods
	}
}
