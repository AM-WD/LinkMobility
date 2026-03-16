using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AMWD.Net.Api.LinkMobility
{
	/// <summary>
	/// Provides a client for interacting with the Link Mobility API.
	/// </summary>
	public class LinkMobilityClient : ILinkMobilityClient, IDisposable
	{
		private readonly ClientOptions _clientOptions;
		private readonly HttpClient _httpClient;

		private bool _isDisposed;

		/// <summary>
		/// Initializes a new instance of the <see cref="LinkMobilityClient" /> class using basic authentication.
		/// </summary>
		/// <param name="username">The username used for basic authentication.</param>
		/// <param name="password">The password used for basic authentication.</param>
		/// <param name="clientOptions">Optional configuration settings for the client.</param>
		/// <param name="httpClient">Optional <see cref="HttpClient"/> instance if you want a custom <see cref="HttpMessageHandler"/> implemented.</param>
		public LinkMobilityClient(string username, string password, ClientOptions? clientOptions = null, HttpClient? httpClient = null)
			: this(new BasicAuthentication(username, password), clientOptions, httpClient)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LinkMobilityClient"/> class using access token authentication.
		/// </summary>
		/// <param name="token">The bearer token used for authentication.</param>
		/// <param name="clientOptions">Optional configuration settings for the client.</param>
		/// <param name="httpClient">Optional <see cref="HttpClient"/> instance if you want a custom <see cref="HttpMessageHandler"/> implemented.</param>
		public LinkMobilityClient(string token, ClientOptions? clientOptions = null, HttpClient? httpClient = null)
			: this(new AccessTokenAuthentication(token), clientOptions, httpClient)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LinkMobilityClient"/> class using authentication and optional client
		/// configuration.
		/// </summary>
		/// <param name="authentication">The authentication mechanism used to authorize requests.</param>
		/// <param name="clientOptions">Optional client configuration settings.</param>
		/// <param name="httpClient">Optional <see cref="HttpClient"/> instance if you want a custom <see cref="HttpMessageHandler"/> implemented.</param>
		public LinkMobilityClient(IAuthentication authentication, ClientOptions? clientOptions = null, HttpClient? httpClient = null)
		{
			if (authentication == null)
				throw new ArgumentNullException(nameof(authentication));

			_clientOptions = clientOptions ?? new ClientOptions();
			ValidateClientOptions();

			_httpClient = httpClient ?? CreateHttpClient();
			ConfigureHttpClient(_httpClient);

			authentication.AddHeader(_httpClient);
		}

		/// <summary>
		/// Disposes of the resources used by the <see cref="LinkMobilityClient"/> object.
		/// </summary>
		public void Dispose()
		{
			if (_isDisposed)
				return;

			_isDisposed = true;

			_httpClient.Dispose();
			GC.SuppressFinalize(this);
		}

		/// <inheritdoc/>
		public async Task<TResponse> PostAsync<TResponse, TRequest>(string requestPath, TRequest? request, IQueryParameter? queryParams = null, CancellationToken cancellationToken = default)
		{
			ThrowIfDisposed();
			ValidateRequestPath(requestPath);

			string requestUrl = BuildRequestUrl(requestPath, queryParams);
			var httpContent = ConvertRequest(request);

			var httpRequest = new HttpRequestMessage
			{
				Method = HttpMethod.Post,
				RequestUri = new Uri(requestUrl, UriKind.Relative),
				Content = httpContent,
			};

			var httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
			var response = await GetResponse<TResponse>(httpResponse, cancellationToken).ConfigureAwait(false);
			return response;
		}

		private string BuildRequestUrl(string requestPath, IQueryParameter? queryParams = null)
		{
			string path = requestPath.Trim().TrimStart('/');
			var param = new Dictionary<string, string>();

			if (_clientOptions.DefaultQueryParams.Count > 0)
			{
				foreach (var kvp in _clientOptions.DefaultQueryParams)
					param[kvp.Key] = kvp.Value;
			}

			var customQueryParams = queryParams?.GetQueryParameters();
			if (customQueryParams?.Count > 0)
			{
				foreach (var kvp in customQueryParams)
					param[kvp.Key] = kvp.Value;
			}

			if (param.Count == 0)
				return path;

			string queryString = string.Join("&", param.Select(kvp => $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}"));
			return $"{path}?{queryString}";
		}

		private void ValidateClientOptions()
		{
			if (string.IsNullOrWhiteSpace(_clientOptions.BaseUrl))
				throw new ArgumentNullException(nameof(_clientOptions.BaseUrl), "BaseUrl cannot be null or whitespace.");

			if (_clientOptions.Timeout <= TimeSpan.Zero)
				throw new ArgumentOutOfRangeException(nameof(_clientOptions.Timeout), "Timeout must be greater than zero.");

			if (_clientOptions.UseProxy && _clientOptions.Proxy == null)
				throw new ArgumentNullException(nameof(_clientOptions.Proxy), "Proxy cannot be null when UseProxy is true.");
		}

		private static void ValidateRequestPath(string requestPath)
		{
			if (string.IsNullOrWhiteSpace(requestPath))
				throw new ArgumentNullException(nameof(requestPath), "The request path is required");

			if (requestPath.Contains('?'))
				throw new ArgumentException("Query parameters are not allowed in the request path", nameof(requestPath));
		}

		private HttpClient CreateHttpClient()
		{
			string version = GetType().Assembly
				.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
				?.InformationalVersion ?? "unknown";

			HttpMessageHandler handler;
			try
			{
				handler = new HttpClientHandler
				{
					AllowAutoRedirect = _clientOptions.AllowRedirects,
					UseProxy = _clientOptions.UseProxy,
					Proxy = _clientOptions.Proxy
				};
			}
			catch (PlatformNotSupportedException)
			{
				handler = new HttpClientHandler
				{
					AllowAutoRedirect = _clientOptions.AllowRedirects
				};
			}

			var httpClient = new HttpClient(handler, disposeHandler: true);

			httpClient.DefaultRequestHeaders.UserAgent.Clear();
			httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(nameof(LinkMobilityClient), version));

			return httpClient;
		}

		private void ConfigureHttpClient(HttpClient httpClient)
		{
			string baseUrl = _clientOptions.BaseUrl.Trim().TrimEnd('/');

			httpClient.BaseAddress = new Uri($"{baseUrl}/");
			httpClient.Timeout = _clientOptions.Timeout;

			httpClient.DefaultRequestHeaders.Accept.Clear();
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			if (_clientOptions.DefaultHeaders.Count > 0)
			{
				foreach (var headerKvp in _clientOptions.DefaultHeaders)
					httpClient.DefaultRequestHeaders.Add(headerKvp.Key, headerKvp.Value);
			}
		}

		private static HttpContent? ConvertRequest<T>(T request)
		{
			if (request == null)
				return null;

			if (request is HttpContent httpContent)
				return httpContent;

			string json = request.SerializeObject();
			return new StringContent(json, Encoding.UTF8, "application/json");
		}

		private static async Task<TResponse> GetResponse<TResponse>(HttpResponseMessage httpResponse, CancellationToken cancellationToken)
		{
#if NET6_0_OR_GREATER
			string content = await httpResponse.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
#else
			string content = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
#endif

			return httpResponse.StatusCode switch
			{
				HttpStatusCode.Unauthorized => throw new AuthenticationException($"HTTP auth missing: {httpResponse.StatusCode}"),
				HttpStatusCode.Forbidden => throw new AuthenticationException($"HTTP auth missing: {httpResponse.StatusCode}"),
				HttpStatusCode.OK =>
					content.DeserializeObject<TResponse>()
						?? throw new ApplicationException("Response could not be deserialized"),
				_ => throw new ApplicationException($"Unknown HTTP response: {httpResponse.StatusCode}"),
			};
		}

		private void ThrowIfDisposed()
		{
			if (_isDisposed)
				throw new ObjectDisposedException(GetType().FullName);
		}
	}
}
