using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace AMWD.Net.Api.LinkMobility.Webhook.WhatsApp
{
	/// <summary>
	/// WhatsApp message status.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum DeliveryStatus
	{
		/// <summary>
		/// Indicates the message was successfully sent from our servers.
		/// <br/>
		/// WhatsApp UI equivalent: One checkmark.
		/// </summary>
		[EnumMember(Value = "sent")]
		Sent = 1,

		/// <summary>
		/// Indicates message was successfully delivered to the WhatsApp user's device.
		/// <br/>
		/// WhatsApp UI equivalent: Two checkmarks.
		/// </summary>
		[EnumMember(Value = "delivered")]
		Delivered = 2,

		/// <summary>
		/// Indicates failure to send or deliver the message to the WhatsApp user's device.
		/// <br/>
		/// WhatsApp UI equivalent: Red error triangle.
		/// </summary>
		[EnumMember(Value = "failed")]
		Failed = 3,

		/// <summary>
		/// Indicates the message was displayed in an open chat thread in the WhatsApp user's device.
		/// <br/>
		/// WhatsApp UI equivalent: Two blue checkmarks.
		/// </summary>
		[EnumMember(Value = "read")]
		Read = 4,

		/// <summary>
		/// Indicates the first time a voice message is played by the WhatsApp user's device.
		/// <br/>
		/// WhatsApp UI equivalent: Blue microphone.
		/// </summary>
		[EnumMember(Value = "played")]
		Played = 5,
	}
}
