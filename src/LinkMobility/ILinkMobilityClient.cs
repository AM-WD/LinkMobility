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
		/// Performs a POST request to the LINK mobility API.
		/// </summary>
		/// <typeparam name="TResponse">The type of the response.</typeparam>
		/// <typeparam name="TRequest">The type of the request.</typeparam>
		/// <param name="requestPath">The path of the API endpoint.</param>
		/// <param name="request">The request data.</param>
		/// <param name="queryParams">Optional query parameters.</param>
		/// <param name="cancellationToken">A cancellation token to propagate notification that operations should be canceled.</param>
		Task<TResponse> PostAsync<TResponse, TRequest>(string requestPath, TRequest? request, IQueryParameter? queryParams = null, CancellationToken cancellationToken = default);
	}
}
