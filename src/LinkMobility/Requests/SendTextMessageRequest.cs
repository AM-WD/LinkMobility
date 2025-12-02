namespace AMWD.Net.Api.LinkMobility.Requests
{
	/// <summary>
	/// Request to send a text message to a list of recipients.
	/// </summary>
	public class SendTextMessageRequest
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SendTextMessageRequest"/> class.
		/// </summary>
		/// <param name="messageContent">The message.</param>
		/// <param name="recipientAddressList">The recipient list.</param>
		public SendTextMessageRequest(string messageContent, IReadOnlyCollection<string> recipientAddressList)
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
		/// Specifies the maximum number of SMS to be generated.
		/// </summary>
		/// <remarks>
		/// If the system generates more than this number of SMS, the status code <see cref="StatusCodes.MaxSmsPerMessageExceeded"/> is returned.
		/// The default value of this parameter is <c>0</c>.
		/// If set to <c>0</c>, no limitation is applied.
		/// </remarks>
		[JsonProperty("maxSmsPerMessage")]
		public int? MaxSmsPerMessage { get; set; }

		/// <summary>
		/// <em>UTF-8</em> encoded message content.
		/// </summary>
		[JsonProperty("messageContent")]
		public string MessageContent { get; set; }

		/// <summary>
		/// <em>Optional</em>.
		/// Specifies the message type.
		/// </summary>
		/// <remarks>
		/// Allowed values are <see cref="MessageType.Default"/> and <see cref="MessageType.Voice"/>.
		/// When using the message type <see cref="MessageType.Default"/>, the outgoing message type is determined based on account settings.
		/// Using the message type <see cref="MessageType.Voice"/> triggers a voice call.
		/// </remarks>
		[JsonProperty("messageType")]
		public MessageType? MessageType { get; set; }

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
		public SenderAddressType? SenderAddressType { get; set; }

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
		/// Specifies the validity periode (in seconds) in which the message is tried to be delivered to the recipient.
		/// </summary>
		/// <remarks>
		/// A minimum of 1 minute (<c>60</c> seconds) and a maximum of 3 days (<c>259200</c> seconds) are allowed.
		/// </remarks>
		[JsonProperty("validityPeriode")]
		public int? ValidityPeriode { get; set; }
	}
}
