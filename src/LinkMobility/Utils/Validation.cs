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
		/// <remarks>
		/// It comes down to a string of digits with a length between 8 and 15, starting with a non-zero digit.
		/// This is a common format for international phone numbers, where the first few digits represent the country code, followed by the national number.
		/// A leading <c>+</c> is has to be removed (not part of the <see href="https://en.wikipedia.org/wiki/E.164">E.164</see>).
		/// <br/>
		/// Regex (inside): <c>^[1-9][0-9]{7,14}$</c>
		/// </remarks>
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
