using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace AMWD.Net.Api.LinkMobility.Webhook.Text
{
	/// <summary>
	/// Defines the type of notification.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TextMessageType
	{
		/// <summary>
		/// Notification of an incoming text message.
		/// </summary>
		[EnumMember(Value = "text")]
		Text = 1,

		/// <summary>
		/// Notification of an incoming binary message.
		/// </summary>
		[EnumMember(Value = "binary")]
		Binary = 2,

		/// <summary>
		/// Notification of a delivery report.
		/// </summary>
		[EnumMember(Value = "deliveryReport")]
		DeliveryReport = 3
	}
}
