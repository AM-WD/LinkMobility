using System.Text.RegularExpressions;

namespace AMWD.Net.Api.LinkMobility.Utils
{
	/// <summary>
	/// Validation helper for LINK Mobility API requirements.
	/// </summary>
	public static class Validation
	{
		/// <summary>
		/// Validates whether the provided string is a valid MSISDN (E.164 formatted).
		/// <br/>
		/// See <see href="https://en.wikipedia.org/wiki/MSISDN">Wikipedia: MSISDN</see> for more information.
		/// </summary>
		/// <param name="msisdn">The string to validate.</param>
		/// <returns><see langword="true"/> for a valid MSISDN number, <see langword="false"/> otherwise.</returns>
		public static bool IsValidMSISDN(string msisdn)
		{
			if (string.IsNullOrWhiteSpace(msisdn))
				return false;

			return Regex.IsMatch(msisdn, @"^[1-9][0-9]{7,14}$", RegexOptions.Compiled);
		}
	}
}
