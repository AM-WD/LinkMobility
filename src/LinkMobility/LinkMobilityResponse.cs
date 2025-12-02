namespace AMWD.Net.Api.LinkMobility
{
	public class LinkMobilityResponse
	{
		[JsonProperty("clientMessageId")]
		public string ClientMessageId { get; set; }

		[JsonProperty("smsCount")]
		public int SmsCount { get; set; }

		[JsonProperty("statusCode")]
		public StatusCodes StatusCode { get; set; }

		[JsonProperty("statusMessage")]
		public string StatusMessage { get; set; }

		[JsonProperty("transferId")]
		public string TransferId { get; set; }
	}
}
