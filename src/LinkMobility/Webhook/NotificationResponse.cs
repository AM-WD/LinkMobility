using AMWD.Net.Api.LinkMobility.Utils;

namespace AMWD.Net.Api.LinkMobility.Webhook
{
	/// <summary>
	/// Representes the response to an incoming message notification.
	/// (See <see href="https://developer.linkmobility.eu/sms-api/receive-incoming-messages">API</see>)
	/// </summary>
	/// <remarks>
	/// This notification acknowlegement is the same for all webhooks of LINK Mobility.
	/// </remarks>
	public class NotificationResponse
	{
		/// <summary>
		/// Gets or sets the status code of the response.
		/// </summary>
		[JsonProperty("statusCode")]
		public StatusCodes StatusCode { get; set; } = StatusCodes.Ok;

		/// <summary>
		/// Gets or sets the status message of the response.
		/// </summary>
		[JsonProperty("statusMessage")]
		public string StatusMessage { get; set; } = "OK";

		/// <summary>
		/// Returns a string representation of the current object in serialized format.
		/// </summary>
		/// <returns>A string containing the serialized form of the object (json).</returns>
		public override string ToString()
			=> this.SerializeObject();
	}
}
