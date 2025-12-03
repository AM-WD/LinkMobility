using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace AMWD.Net.Api.LinkMobility
{
	/// <summary>
	/// Defines the types of delivery methods on a report.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum DeliveryType
	{
		/// <summary>
		/// Message sent via SMS.
		/// </summary>
		[EnumMember(Value = "sms")]
		Sms = 1,

		/// <summary>
		/// Message sent as Push message.
		/// </summary>
		[EnumMember(Value = "push")]
		Push = 2,

		/// <summary>
		/// Message sent as failover SMS.
		/// </summary>
		[EnumMember(Value = "failover-sms")]
		FailoverSms = 3,

		/// <summary>
		/// Message sent as voice message.
		/// </summary>
		[EnumMember(Value = "voice")]
		Voice = 4
	}
}
