using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace AMWD.Net.Api.LinkMobility.Text
{
	/// <summary>
	/// Specifies the type of sender address.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum AddressType
	{
		/// <summary>
		/// National number.
		/// </summary>
		[EnumMember(Value = "national")]
		National = 1,

		/// <summary>
		/// International number.
		/// </summary>
		[EnumMember(Value = "international")]
		International = 2,

		/// <summary>
		/// Alphanumeric sender ID.
		/// </summary>
		[EnumMember(Value = "alphanumeric")]
		Alphanumeric = 3,

		/// <summary>
		/// Shortcode.
		/// </summary>
		[EnumMember(Value = "shortcode")]
		Shortcode = 4,
	}
}
