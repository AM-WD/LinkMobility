namespace AMWD.Net.Api.LinkMobility.WhatsApp
{
	/// <summary>
	/// A WhatsApp image message content.
	/// </summary>
	public class ImageMessageContent : IMessageContent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ImageMessageContent"/> class with the provided message text.
		/// </summary>
		/// <param name="mediaLink">The link to an image (http/https only).</param>
		public ImageMessageContent(string mediaLink)
		{
			Body = new Content { Link = mediaLink };
		}

		/// <inheritdoc/>
		[JsonProperty("type")]
		public MessageType Type => MessageType.Image;

		/// <summary>
		/// The content container.
		/// </summary>
		[JsonProperty("image")]
		public Content Body { get; set; }

		/// <inheritdoc/>
		public bool IsValid()
		{
			return !string.IsNullOrWhiteSpace(Body?.Link)
				&& (Body!.Link!.StartsWith("http://") || Body!.Link!.StartsWith("https://"));
		}

		/// <summary>
		/// Container for the text message content.
		/// </summary>
		public class Content
		{
			/// <summary>
			/// The message text.
			/// </summary>
			[JsonProperty("link")]
			public string? Link { get; set; }

			/// <summary>
			/// A caption for the image.
			/// </summary>
			[JsonProperty("caption")]
			public string? Caption { get; set; }
		}
	}
}
