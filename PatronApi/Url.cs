using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatronLock.PatronApi
{
	/// <summary>
	/// Class to build a URL to the Patron API
	/// </summary>
	public class Url
	{
		#region Public Methods
		/// <summary>
		/// Builds a URL for the API
		/// </summary>
		/// <param name="ssl"><c>true</c> to use HTTPS, <c>false</c> to use HTTP</param>
		/// <param name="nuid">the NUID you will be looking up</param>
		/// <returns>a <see cref="System.String"/> containing the URL to get information for the specified patron</returns>
		public static string Build(bool ssl, int nuid)
		{
			string server = "library.unl.edu";
			int httpPort = 4500;
			int httpsPort = 54620;

			// Build http(s|'')://<server>:<port>/PATRONAPI/<nuid>/dump
			return String.Format("http{0}://{1}:{2}/PATRONAPI/{3}/dump",
					(ssl ? "s" : ""),
					server,
					(ssl ? httpsPort.ToString() : httpPort.ToString()),
					nuid.ToString()
				);
		}
		#endregion Public Methods
	}
}
