using System.Threading;
using System.Threading.Tasks;

namespace AMWD.Net.Api.LinkMobility
{
	/// <summary>
	/// Defines the interface for a Link Mobility API client.
	/// </summary>
	public interface ILinkMobilityClient
	{
		/// <summary>
		/// Sends a text message to a list of recipients.
		/// </summary>
		/// <param name="request">The request data.</param>
		/// <param name="cancellationToken">A cancellation token to propagate notification that operations should be canceled.</param>
		Task<SendMessageResponse> SendTextMessage(SendTextMessageRequest request, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends a binary message to a list of recipients.
		/// </summary>
		/// <param name="request">The request data.</param>
		/// <param name="cancellationToken">A cancellation token to propagate notification that operations should be canceled.</param>
		Task<SendMessageResponse> SendBinaryMessage(SendBinaryMessageRequest request, CancellationToken cancellationToken = default);
	}
}
