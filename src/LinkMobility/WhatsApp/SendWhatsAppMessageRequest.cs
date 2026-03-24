namespace AMWD.Net.Api.LinkMobility.WhatsApp
{
	/// <summary>
	/// Request to send a WhatsApp message to a list of recipients.
	/// </summary>
	public class SendWhatsAppMessageRequest
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SendWhatsAppMessageRequest"/> class.
		/// </summary>
		/// <param name="messageContent">The content of a WhatsApp message.</param>
		/// <param name="recipientAddressList">A list of recipient numbers.</param>
		public SendWhatsAppMessageRequest(IMessageContent messageContent, IReadOnlyCollection<string> recipientAddressList)
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
		///  The following content categories are supported: <see cref="ContentCategory.Informational"/>, <see cref="ContentCategory.Advertisement"/> or <see cref="ContentCategory.Personal"/>.
		///  If no content category is provided, the default setting is used (may be changed inside the web interface).
		/// </remarks>
		[JsonProperty("contentCategory")]
		public ContentCategory? ContentCategory { get; set; }

		/// <summary>
		/// <em>UTF-8</em> encoded message content.
		/// </summary>
		[JsonProperty("messageContent")]
		public IMessageContent MessageContent { get; set; }

		/// <summary>
		/// <em>Optional</em>.
		/// Priority of the message.
		/// </summary>
		/// <remarks>
		/// Must not exceed the value configured for the channel used to send the message.
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
		/// <see langword="true"/>: The transmission is only simulated, no whatsapp message is sent.
		/// Depending on the number of recipients the status code <see cref="StatusCodes.Ok"/> or <see cref="StatusCodes.OkQueued"/> is returned.
		/// <br/>
		/// <see langword="false"/>: No simulation is done. The whatsapp message is sent. (default)
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
