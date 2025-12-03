using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace AMWD.Net.Api.LinkMobility
{
	/// <summary>
	/// Specifies the message type.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MessageType
	{
		/// <summary>
		/// The message is sent as defined in the account settings.
		/// </summary>
		[EnumMember(Value = "default")]
		Default = 1,

		/// <summary>
		/// The message is sent as voice call.
		/// </summary>
		[EnumMember(Value = "voice")]
		Voice = 2,
	}
}
