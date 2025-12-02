namespace AMWD.Net.Api.LinkMobility
{
	/// <summary>
	/// Custom status codes as defined by <see href="https://developer.linkmobility.eu/sms-api/rest-api#section/Status-codes">Link Mobility</see>.
	/// </summary>
	public enum StatusCodes
	{
		/// <summary>
		/// Request accepted, Message(s) sent.
		/// </summary>
		Ok = 2000,

		/// <summary>
		/// Request accepted, Message(s) queued.
		/// </summary>
		OkQueued = 2001,

		/// <summary>
		/// Invalid Credentials. Inactive account or customer.
		/// </summary>
		InvalidCredentials = 4001,

		/// <summary>
		/// One or more recipients are not in the correct format or are containing invalid MSISDNs.
		/// </summary>
		InvalidRecipient = 4002,

		/// <summary>
		/// Invalid Sender. Sender address or type is invalid.
		/// </summary>
		InvalidSender = 4003,

		/// <summary>
		/// Invalid messageType.
		/// </summary>
		InvalidMessageType = 4004,

		/// <summary>
		/// Invalid clientMessageId.
		/// </summary>
		InvalidMessageId = 4008,

		/// <summary>
		/// Message text (messageContent) is invalid.
		/// </summary>
		InvalidText = 4009,

		/// <summary>
		/// Message limit is reached.
		/// </summary>
		MessageLimitExceeded = 4013,

		/// <summary>
		/// Sender IP address is not authorized.
		/// </summary>
		UnauthorizedIp = 4014,

		/// <summary>
		/// Invalid Message Priority.
		/// </summary>
		InvalidMessagePriority = 4015,

		/// <summary>
		/// Invalid notification callback url.
		/// </summary>
		InvalidCallbackAddress = 4016,

		/// <summary>
		/// A required parameter was not given. The parameter name is shown in the status message.
		/// </summary>
		ParameterMissing = 4019,

		/// <summary>
		/// Account is invalid.
		/// </summary>
		InvalidAccount = 4021,

		/// <summary>
		/// Access to the API was denied.
		/// </summary>
		AccessDenied = 4022,

		/// <summary>
		/// Request limit exceeded for this IP address.
		/// </summary>
		ThrottlingSpammingIp = 4023,

		/// <summary>
		/// Transfer rate for immediate transmissions exceeded.
		/// Too many recipients in this request (1000).
		/// </summary>
		ThrottlingTooManyRecipients = 4025,

		/// <summary>
		/// The message content results in too many (automatically generated) sms segments.
		/// </summary>
		MaxSmsPerMessageExceeded = 4026,

		/// <summary>
		/// A message content segment is invalid
		/// </summary>
		InvalidMessageSegment = 4027,

		/// <summary>
		/// Recipients not allowed.
		/// </summary>
		RecipientsNotAllowed = 4029,

		/// <summary>
		/// All recipients blacklisted.
		/// </summary>
		RecipientBlacklisted = 4030,

		/// <summary>
		/// Not allowed to send sms messages.
		/// </summary>
		SmsDisabled = 4035,

		/// <summary>
		/// Invalid content category.
		/// </summary>
		InvalidContentCategory = 4040,

		/// <summary>
		/// Invalid validity periode.
		/// </summary>
		InvalidValidityPeriode = 4041,

		/// <summary>
		/// All of the recipients are blocked by quality rating.
		/// </summary>
		RecipientsBlockedByQualityRating = 4042,

		/// <summary>
		/// All of the recipients are blocked by spamcheck.
		/// </summary>
		RecipientsBlockedBySpamcheck = 4043,

		/// <summary>
		/// Internal error.
		/// </summary>
		InternalError = 5000,

		/// <summary>
		/// Service unavailable.
		/// </summary>
		ServiceUnavailable = 5003
	}
}
