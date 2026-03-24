namespace AMWD.Net.Api.LinkMobility.WhatsApp
{
	/// <summary>
	/// A WhatsApp document message content.
	/// </summary>
	public class DocumentMessageContent : IMessageContent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DocumentMessageContent"/> class with the provided document link.
		/// </summary>
		/// <param name="mediaLink">The link to a document (http/https only).</param>
		public DocumentMessageContent(string mediaLink)
		{
			Body = new Content { Link = mediaLink };
		}

		/// <inheritdoc/>
		[JsonProperty("type")]
		public MessageType Type => MessageType.Document;

		/// <summary>
		/// The content container.
		/// </summary>
		[JsonProperty("document")]
		public Content Body { get; set; }

		/// <inheritdoc/>
		public bool IsValid()
		{
			return !string.IsNullOrWhiteSpace(Body?.Link)
				&& (Body!.Link!.StartsWith("http://") || Body!.Link!.StartsWith("https://"));
		}

		/// <summary>
		/// Container for the document message content.
		/// </summary>
		public class Content
		{
			/// <summary>
			/// The media link.
			/// </summary>
			[JsonProperty("link")]
			public string? Link { get; set; }

			/// <summary>
			/// A caption for the document.
			/// </summary>
			[JsonProperty("caption")]
			public string? Caption { get; set; }

			/// <summary>
			/// A filename for the document (e.g. "file.pdf").
			/// </summary>
			[JsonProperty("filename")]
			public string? Filename { get; set; }
		}
	}
}
