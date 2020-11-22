using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatronLock.PatronApi
{
	/// <summary>
	/// An exception meant to be thrown upon receiving an error from the API
	/// </summary>
	public class PatronException : System.Exception
	{
		#region Data Members
		private int number;
		#endregion Data Members

		#region Properties
		/// <summary>
		/// The error number as specified by the API
		/// </summary>
		public int Number
		{
			get { return this.number; }
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Creates a new <see cref="PatronException"/>
		/// </summary>
		/// <param name="number">the error number</param>
		/// <param name="message">the error message</param>
		public PatronException(int number, string message)
			: base(message)
		{
			this.number = number;
		}
		#endregion Constructors
	}
}
