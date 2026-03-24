using System.Threading;
using System.Threading.Tasks;
using AMWD.Net.Api.LinkMobility.Utils;

namespace AMWD.Net.Api.LinkMobility.WhatsApp
{
	/// <summary>
	/// Implementation of WhatsApp messaging. <see href="https://developer.linkmobility.eu/whatsapp-api/rest-api">API</see>
	/// </summary>
	public static class WhatsAppExtensions
	{
		/// <summary>
		/// Sends a WhatsApp message to a list of recipients.
		/// </summary>
		/// <param name="client">The <see cref="ILinkMobilityClient"/> instance.</param>
		/// <param name="uuid">The unique identifier of the WhatsApp channel.</param>
		/// <param name="request">The request data.</param>
		/// <param name="cancellationToken">A cancellation token to propagate notification that operations should be canceled.</param>
		public static Task<SendMessageResponse> SendWhatsAppMessage(this LinkMobilityClient client, Guid uuid, SendWhatsAppMessageRequest request, CancellationToken cancellationToken = default)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (request.MessageContent?.IsValid() != true)
				throw new ArgumentException("A valid message must be provided.", nameof(request.MessageContent));

			if (request.ContentCategory.HasValue && request.ContentCategory.Value != ContentCategory.Informational && request.ContentCategory.Value != ContentCategory.Advertisement && request.ContentCategory.Value != ContentCategory.Personal)
				throw new ArgumentException($"Content category '{request.ContentCategory.Value}' is not valid.", nameof(request.ContentCategory));

			if (request.RecipientAddressList == null || request.RecipientAddressList.Count == 0)
				throw new ArgumentException("At least one recipient must be provided.", nameof(request.RecipientAddressList));

			foreach (string recipient in request.RecipientAddressList)
			{
				if (!Validation.IsValidMSISDN(recipient))
					throw new ArgumentException($"Recipient address '{recipient}' is not a valid MSISDN format.", nameof(request.RecipientAddressList));
			}

			return client.PostAsync<SendMessageResponse, SendWhatsAppMessageRequest>($"/channels/{uuid}/send/whatsapp", request, cancellationToken: cancellationToken);
		}
	}
}
