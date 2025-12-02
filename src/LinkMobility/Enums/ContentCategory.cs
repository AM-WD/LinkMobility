using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace AMWD.Net.Api.LinkMobility
{
	/// <summary>
	/// Content categories as defined by <see href="https://developer.linkmobility.eu/sms-api/rest-api#operation/sendUsingPOST">Link Mobility</see>.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ContentCategory
	{
		/// <summary>
		/// Represents content that is classified as informational.
		/// </summary>
		[EnumMember(Value = "informational")]
		Informational = 1,

		/// <summary>
		/// Represents content that is classified as an advertisement.
		/// </summary>
		[EnumMember(Value = "advertisement")]
		Advertisement = 2,
	}
}
