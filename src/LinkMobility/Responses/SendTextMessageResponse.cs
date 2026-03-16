namespace AMWD.Net.Api.LinkMobility
{
	/// <summary>
	/// Response of a text message sent to a list of recipients.
	/// </summary>
	public class SendTextMessageResponse : SendMessageResponse
	{
		/// <summary>
		/// The actual number of generated SMS.
		/// </summary>
		[JsonProperty("smsCount")]
		public int? SmsCount { get; set; }
	}
}
