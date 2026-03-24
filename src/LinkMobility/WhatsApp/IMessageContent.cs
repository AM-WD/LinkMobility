namespace AMWD.Net.Api.LinkMobility.WhatsApp
{
	/// <summary>
	/// The message content of a WhatsApp message.
	/// </summary>
	public interface IMessageContent
	{
		/// <summary>
		/// The type of the message content.
		/// </summary>
		[JsonProperty("type")]
		MessageType Type { get; }

		/// <summary>
		/// Determines whether the content message is valid.
		/// </summary>
		bool IsValid();
	}
}
