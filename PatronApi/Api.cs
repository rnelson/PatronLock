using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace PatronLock.PatronApi
{
	/// <summary>
	/// Interface for the Patron API
	/// </summary>
	public class Api
	{
		#region Data Members
		private bool ssl;
		#endregion Data Members

		#region Properties
		/// <summary>
		/// Gets/sets whether or not to use SSL
		/// </summary>
		public bool SSL
		{
			get { return this.ssl; }
			set { this.ssl = value; }
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Creates a new <see cref="Api"/> object using SSL
		/// </summary>
		public Api() : this(true)
		{
		}

		/// <summary>
		/// Creates a new <see cref="Api"/> object
		/// </summary>
		/// <param name="ssl"><c>true</c> to use HTTPS, <c>false to use HTTP</c></param>
		public Api(bool ssl)
		{
			this.ssl = ssl;
		}
		#endregion Constructors

		#region Public Methods
		/// <summary>
		/// Obtains a <see cref="Patron"/> object matching the given <paramref name="nuid"/>
		/// </summary>
		/// <param name="nuid">the NUID of the patron</param>
		/// <returns>a <see cref="Patron"/> if given a valid NUID, else <c>null</c></returns>
		public Patron GetPatron(int nuid)
		{
			// Allow the certificate covering *.library.unl.edu
			System.Net.ServicePointManager.ServerCertificateValidationCallback = SslValidator.ValidateSslCertificate;

			// Query the Patron API for the user
			string url = Url.Build(this.SSL, nuid);
			WebClient client = new WebClient();

			try
			{
				string response = client.DownloadString(new Uri(url));

				// Build a new Patron object for this user
				Patron p = new Patron(response);

				// Return the new object
				return p;
			}
			catch (Exception)
			{
				// If there was an error, return null
				return null;
			}
		}
		#endregion Public Methods
	}
}
