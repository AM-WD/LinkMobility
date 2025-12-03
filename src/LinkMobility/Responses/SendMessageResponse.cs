namespace AMWD.Net.Api.LinkMobility
{
	/// <summary>
	/// Response of a text message sent to a list of recipients.
	/// </summary>
	public class SendMessageResponse
	{
		/// <summary>
		/// Contains the message id defined in the request.
		/// </summary>
		[JsonProperty("clientMessageId")]
		public string? ClientMessageId { get; set; }

		/// <summary>
		/// The actual number of generated SMS.
		/// </summary>
		[JsonProperty("smsCount")]
		public int? SmsCount { get; set; }

		/// <summary>
		/// Status code.
		/// </summary>
		[JsonProperty("statusCode")]
		public StatusCodes? StatusCode { get; set; }

		/// <summary>
		/// Description of the response status code.
		/// </summary>
		[JsonProperty("statusMessage")]
		public string? StatusMessage { get; set; }

		/// <summary>
		/// Unique identifier that is set after successful processing of the request.
		/// </summary>
		[JsonProperty("transferId")]
		public string? TransferId { get; set; }
	}
}
