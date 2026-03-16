namespace AMWD.Net.Api.LinkMobility
{
	/// <summary>
	/// Request to send a text message to a list of recipients.
	/// </summary>
	public class SendBinaryMessageRequest
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SendBinaryMessageRequest"/> class.
		/// </summary>
		/// <param name="messageContent">A binary message as base64 encoded lines.</param>
		/// <param name="recipientAddressList">A list of recipient numbers.</param>
		public SendBinaryMessageRequest(IReadOnlyCollection<string> messageContent, IReadOnlyCollection<string> recipientAddressList)
		{
			MessageContent = messageContent;
			RecipientAddressList = recipientAddressList;
		}

		/// <summary>
		/// <em>Optional</em>.
		/// May contain a freely definable message id.
		/// </summary>
		[JsonProperty("clientMessageId")]
		public string? ClientMessageId { get; set; }

		/// <summary>
		/// <em>Optional</em>.
		/// The content category that is used to categorize the message (used for blacklisting).
		/// </summary>
		/// <remarks>
		///  The following content categories are supported: <see cref="ContentCategory.Informational"/> or <see cref="ContentCategory.Advertisement"/>.
		///  If no content category is provided, the default setting is used (may be changed inside the web interface).
		/// </remarks>
		[JsonProperty("contentCategory")]
		public ContentCategory? ContentCategory { get; set; }

		/// <summary>
		/// <em>Optional</em>.
		/// Array of <c>Base64</c> encoded binary data.
		/// </summary>
		/// <remarks>
		/// Every element of the array corresponds to a message segment.
		/// The binary data is transmitted without being changed (using 8 bit alphabet).
		/// </remarks>
		[JsonProperty("messageContent")]
		public IReadOnlyCollection<string> MessageContent { get; set; }

		/// <summary>
		/// <em>Optional</em>.
		/// When setting a <c>NotificationCallbackUrl</c> all delivery reports are forwarded to this URL.
		/// </summary>
		[JsonProperty("notificationCallbackUrl")]
		public string? NotificationCallbackUrl { get; set; }

		/// <summary>
		/// <em>Optional</em>.
		/// Priority of the message.
		/// </summary>
		/// <remarks>
		/// Must not exceed the value configured for the account used to send the message.
		/// For more information please contact our customer service.
		/// </remarks>
		[JsonProperty("priority")]
		public int? Priority { get; set; }

		/// <summary>
		/// List of recipients (E.164 formatted <see href="https://en.wikipedia.org/wiki/MSISDN">MSISDN</see>s)
		/// to whom the message should be sent.
		/// <br/>
		/// The list of recipients may contain a maximum of <em>1000</em> entries.
		/// </summary>
		[JsonProperty("recipientAddressList")]
		public IReadOnlyCollection<string> RecipientAddressList { get; set; }

		/// <summary>
		/// <em>Optional</em>.
		/// <br/>
		/// <see langword="true"/>: The message is sent as flash SMS (displayed directly on the screen of the mobile phone).
		/// <br/>
		/// <see langword="false"/>: The message is sent as standard text SMS (default).
		/// </summary>
		[JsonProperty("sendAsFlashSms")]
		public bool? SendAsFlashSms { get; set; }

		/// <summary>
		/// <em>Optional</em>.
		/// Address of the sender (assigned to the account) from which the message is sent.
		/// </summary>
		[JsonProperty("senderAddress")]
		public string? SenderAddress { get; set; }

		/// <summary>
		/// <em>Optional</em>.
		/// The sender address type.
		/// </summary>
		[JsonProperty("senderAddressType")]
		public AddressType? SenderAddressType { get; set; }

		/// <summary>
		/// <em>Optional</em>.
		/// <br/>
		/// <see langword="true"/>: The transmission is only simulated, no SMS is sent.
		/// Depending on the number of recipients the status code <see cref="StatusCodes.Ok"/> or <see cref="StatusCodes.OkQueued"/> is returned.
		/// <br/>
		/// <see langword="false"/>: No simulation is done. The SMS is sent via the SMS Gateway. (default)
		/// </summary>
		[JsonProperty("test")]
		public bool? Test { get; set; }

		/// <summary>
		/// <em>Optional</em>.
		/// <br/>
		/// <see langword="true"/>: Indicates the presence of a user data header in the <see cref="MessageContent"/> property.
		/// <br/>
		/// <see langword="false"/>: Indicates the absence of a user data header in the <see cref="MessageContent"/> property. (default)
		/// </summary>
		[JsonProperty("userDataHeaderPresent")]
		public bool? UserDataHeaderPresent { get; set; }

		/// <summary>
		/// <em>Optional</em>.
		/// Specifies the validity periode (in seconds) in which the message is tried to be delivered to the recipient.
		/// </summary>
		/// <remarks>
		/// A minimum of 1 minute (<c>60</c> seconds) and a maximum of 3 days (<c>259200</c> seconds) are allowed.
		/// </remarks>
		[JsonProperty("validityPeriode")]
		public int? ValidityPeriode { get; set; }
	}
}
