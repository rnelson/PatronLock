using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PatronLock.PatronApi
{
	/// <summary>
	/// Patron object containing information returned by the Patron API
	/// </summary>
	public class Patron
	{
		#region Data Members
		private Dictionary<string, string> properties;
		#endregion Data Members

		#region Properties
		/// <summary>
		/// Gets/sets a property on the <see cref="Patron"/>
		/// </summary>
		/// <param name="propertyName">the name of the property without <c>[p__]</c></param>
		/// <returns>a <see cref="System.String"/> containing the property value if
		/// defined, else an empty string</returns>
		/// <example>
		/// Api api = new Api();
		/// Patron p = api.GetPatron(11789520);
		/// string name = p["PATRN NAME"]; // name = "Allison, Deann K"
		/// </example>
		public string this[string propertyName]
		{
			get
			{
				if (this.properties.ContainsKey(propertyName))
				{
					return this.properties[propertyName];
				}
				else
				{
					return "";
				}
			}
			set { this.properties[propertyName] = value; }
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Creates a new <see cref="Patron"/> object from the given string
		/// </summary>
		/// <param name="webClientResponse">the result given from the API, likely from
		/// <c>WebClient.DownloadString()</c></param>
		/// <remarks>This method is tailored to the specific format of the API's output
		/// and will need to be updated if the API changes.</remarks>
		public Patron(string webClientResponse)
		{
			// Create the dictionary to hold all of our properties
			this.properties = new Dictionary<string, string>(35);

			// Be prepared to throw an exception on error
			string errorNumber = "";
			string errorMessage = "";

			// Parse the response
			string[] entries= webClientResponse.Split(new string[] { @"<br />" }, StringSplitOptions.RemoveEmptyEntries);
			foreach (string entry in entries)
			{
				if (entry.IndexOf('=') != -1)
				{
					string[] parts = entry.Split(new char[] { '=' } );

					// Trim and strip HTML from both parts
					string stripTagsRegex = @"<(.|\n)*?>";
					string key = Regex.Replace(parts[0], stripTagsRegex, String.Empty).Trim();
					string value = Regex.Replace(parts[1], stripTagsRegex, String.Empty).Trim();

					// Strip out the [p*] from the key name
					int keyPPos = key.IndexOf('[');
					if (keyPPos != -1)
					{
						key = key.Substring(0, keyPPos);
					}

					// Error?
					if (key == "ERRNUM")
						errorNumber = value;
					if (key == "ERRMSG")
						errorMessage = value;
					if (errorNumber.Length > 0 && errorMessage.Length > 0)
						throw new PatronException(Int32.Parse(errorNumber), errorMessage);

					// Save the key/value pair
					this.properties.Add(key, value);
					Console.WriteLine("Added key \"{0}\" for value \"{1}\"", key, value);
				}
			}
		}
		#endregion Constructors
	}
}
