using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PatronLock.PatronApi
{
	/// <summary>
	/// A custom SSL validator to allow the library's wildcard certificate to be validated
	/// </summary>
	public class SslValidator
	{
		#region Internal Methods
		/// <summary>
		/// Validates a certificate
		/// </summary>
		/// <param name="sender">the object requesting validation</param>
		/// <param name="certificate">the SSL certificate to validate</param>
		/// <param name="chain">the certificate chain</param>
		/// <param name="errors">policy errors</param>
		/// <returns><c>true</c> if valid, <c>false</c> if invalid</returns>
		/// <remarks>
		/// This function will read app.config (PatronLock.exe.config in the build) to
		/// find the SSL certificate's thumbprint to allow. This allows someone to update
		/// the trusted certificate and re-deploy the .config file without rebuilding the
		/// entire application.
		/// 
		/// The following example sets the thumbprint to B9DCED3E7FB8F857E88790600EF3AFC3A5C7CAEC
		/// 
		/// &lt;configuration&gt;
		///		&lt;appSettings&gt;
		///			&lt;add key="sslthumbprint" value="B9DCED3E7FB8F857E88790600EF3AFC3A5C7CAEC" /&gt;
		///		&lt;/appSettings&gt;
		///	&lt;/configuration&gt;
		/// </remarks>
		internal static bool ValidateSslCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
		{
			try
			{
				// Get the thumbprint for the certificate we were given
				X509Certificate2 cert = new X509Certificate2(certificate);
				string certThumbprint = cert.Thumbprint.Trim().ToUpper();

				// Get the trusted thumbprint from app.config
				AppSettingsReader settings = new AppSettingsReader();
				string trustedThumbprint = (string)settings.GetValue("sslthumbprint", typeof(string));

				if (certThumbprint == trustedThumbprint.Trim().ToUpper())
					return true;
				else
					return false;
			}
			catch (Exception)
			{
				return false;
			}
		}
		#endregion Internal Methods
	}
}
