using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace AMWD.Net.Api.LinkMobility
{
	/// <summary>
	/// Represents a notification for an incoming message or delivery report. (<see href="https://developer.linkmobility.eu/sms-api/receive-incoming-messages">API</see>)
	/// </summary>
	public class IncomingMessageNotification
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IncomingMessageNotification"/> class.
		/// </summary>
		/// <param name="notificationId">The notification id.</param>
		/// <param name="transferId">The transfer id.</param>
		public IncomingMessageNotification(string notificationId, string transferId)
		{
			NotificationId = notificationId;
			TransferId = transferId;
		}

		/// <summary>
		/// Defines the content type of your notification.
		/// </summary>
		[JsonProperty("messageType")]
		public Type MessageType { get; set; }

		/// <summary>
		/// 20 digit long identification of your notification.
		/// </summary>
		[JsonProperty("notificationId")]
		public string NotificationId { get; set; }

		/// <summary>
		/// <see cref="Type.DeliveryReport"/>:
		/// <br/>
		/// Unique transfer-id to connect the deliveryReport to the initial message.
		/// </summary>
		[JsonProperty("transferId")]
		public string TransferId { get; set; }

		/// <summary>
		/// <see cref="Type.Text"/>, <see cref="Type.Binary"/>:
		/// <br/>
		/// Indicates whether you received message is a SMS or a flash-SMS.
		/// </summary>
		[JsonProperty("messageFlashSms")]
		public bool? MessageFlashSms { get; set; }

		/// <summary>
		/// Originator of the sender.
		/// </summary>
		[JsonProperty("senderAddress")]
		public string? SenderAddress { get; set; }

		/// <summary>
		/// <see cref="Type.Text"/>, <see cref="Type.Binary"/>:
		/// <br/>
		/// <see cref="AddressType.International"/> - defines the number format of the mobile originated <see cref="SenderAddress"/>.
		/// International numbers always includes the country prefix.
		/// </summary>
		[JsonProperty("senderAddressType")]
		public AddressType? SenderAddressType { get; set; }

		/// <summary>
		/// Senders address, can either be
		/// <see cref="AddressType.International"/> (4366012345678),
		/// <see cref="AddressType.National"/> (066012345678) or a
		/// <see cref="AddressType.Shortcode"/> (1234).
		/// </summary>
		[JsonProperty("recipientAddress")]
		public string? RecipientAddress { get; set; }

		/// <summary>
		/// <see cref="Type.Text"/>, <see cref="Type.Binary"/>:
		/// <br/>
		/// Defines the number format of the mobile originated message.
		/// </summary>
		[JsonProperty("recipientAddressType")]
		public AddressType? RecipientAddressType { get; set; }

		/// <summary>
		/// <see cref="Type.Text"/>:
		/// <br/>
		/// Text body of the message encoded in <c>UTF-8</c>.
		/// In the case of concatenated SMS it will contain the complete content of all segments.
		/// </summary>
		[JsonProperty("textMessageContent")]
		public string? TextMessageContent { get; set; }

		/// <summary>
		/// <see cref="Type.Binary"/>:
		/// <br/>
		/// Indicates whether a user-data-header is included within a <c>Base64</c> encoded byte segment.
		/// </summary>
		[JsonProperty("userDataHeaderPresent")]
		public bool? UserDataHeaderPresent { get; set; }

		/// <summary>
		/// <see cref="Type.Binary"/>:
		/// <br/>
		/// Content of a binary SMS in an array of <c>Base64</c> strings (URL safe).
		/// </summary>
		[JsonProperty("binaryMessageContent")]
		public IReadOnlyCollection<string>? BinaryMessageContent { get; set; }

		/// <summary>
		/// <see cref="Type.DeliveryReport"/>:
		/// <br/>
		/// Status of the message.
		/// </summary>
		[JsonProperty("deliveryReportMessageStatus")]
		public DeliveryStatus? DeliveryReportMessageStatus { get; set; }

		/// <summary>
		/// <see cref="Type.DeliveryReport"/>:
		/// <br/>
		/// ISO 8601 timestamp. Point of time sending the message to recipients address.
		/// </summary>
		[JsonProperty("sentOn")]
		public DateTime? SentOn { get; set; }

		/// <summary>
		/// <see cref="Type.DeliveryReport"/>:
		/// <br/>
		/// ISO 8601 timestamp. Point of time of submitting the message to the mobile operators network.
		/// </summary>
		[JsonProperty("deliveredOn")]
		public DateTime? DeliveredOn { get; set; }

		/// <summary>
		/// <see cref="Type.DeliveryReport"/>:
		/// <br/>
		/// Type of delivery used to send the message.
		/// </summary>
		[JsonProperty("deliveredAs")]
		public DeliveryType? DeliveredAs { get; set; }

		/// <summary>
		/// <see cref="Type.DeliveryReport"/>:
		/// <br/>
		/// In the case of a delivery report, the <see cref="ClientMessageId"/> contains the optional submitted message id.
		/// </summary>
		[JsonProperty("clientMessageId")]
		public string? ClientMessageId { get; set; }

		/// <summary>
		/// Defines the type of notification.
		/// </summary>
		[JsonConverter(typeof(StringEnumConverter))]
		public enum Type
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

		/// <summary>
		/// Tries to parse the given content as <see cref="IncomingMessageNotification"/>.
		/// </summary>
		/// <param name="json">The given content (should be the notification json).</param>
		/// <param name="notification">The deserialized notification.</param>
		/// <returns>
		/// <see langword="true"/> if the content could be parsed; otherwise, <see langword="false"/>.
		/// </returns>
		public static bool TryParse(string json, out IncomingMessageNotification? notification)
		{
			try
			{
				notification = json.DeserializeObject<IncomingMessageNotification>();
				return notification != null;
			}
			catch
			{
				notification = null;
				return false;
			}
		}
	}
}
