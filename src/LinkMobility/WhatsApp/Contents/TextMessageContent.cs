namespace AMWD.Net.Api.LinkMobility.WhatsApp
{
	/// <summary>
	/// A WhatsApp text message content.
	/// </summary>
	public class TextMessageContent : IMessageContent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TextMessageContent"/> class with the provided message text.
		/// </summary>
		/// <param name="message">The message text.</param>
		public TextMessageContent(string message)
		{
			Body = new Content { Text = message };
		}

		/// <inheritdoc/>
		[JsonProperty("type")]
		public MessageType Type => MessageType.Text;

		/// <summary>
		/// The content container.
		/// </summary>
		[JsonProperty("text")]
		public Content Body { get; set; }

		/// <inheritdoc/>
		public bool IsValid()
		{
			return !string.IsNullOrWhiteSpace(Body?.Text);
		}

		/// <summary>
		/// Container for the text message content.
		/// </summary>
		public class Content
		{
			/// <summary>
			/// The message text.
			/// </summary>
			[JsonProperty("body")]
			public string? Text { get; set; }

			/// <summary>
			/// Indicates whether urls should try to be previewed.
			/// </summary>
			[JsonProperty("preview_url")]
			public bool PreviewUrl { get; set; } = false;
		}
	}
}
