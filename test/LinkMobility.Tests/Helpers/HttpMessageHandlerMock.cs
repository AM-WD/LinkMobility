using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq.Protected;

namespace LinkMobility.Tests.Helpers
{
	internal class HttpMessageHandlerMock
	{
		public HttpMessageHandlerMock()
		{
			Mock = new();

			Mock.Protected()
				.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
				.Callback<HttpRequestMessage, CancellationToken>(async (request, cancellationToken) =>
				{
					var callback = new HttpRequestMessageCallback
					{
						HttpMethod = request.Method,
						Url = request.RequestUri.ToString(),
						Headers = request.Headers.ToDictionary(h => h.Key, h => string.Join(", ", h.Value))
					};

					if (request.Content != null)
					{
						callback.ContentRaw = await request.Content.ReadAsByteArrayAsync(cancellationToken);
						callback.Content = await request.Content.ReadAsStringAsync(cancellationToken);
					}

					RequestCallbacks.Add(callback);
				})
				.ReturnsAsync(Responses.Dequeue);
		}

		public List<HttpRequestMessageCallback> RequestCallbacks { get; } = [];

		public Queue<HttpResponseMessage> Responses { get; } = new();

		public Mock<HttpClientHandler> Mock { get; }

		public IProtectedMock<HttpClientHandler> Protected => Mock.Protected();
	}

	internal class HttpRequestMessageCallback
	{
		public HttpMethod HttpMethod { get; set; }

		public string Url { get; set; }

		public Dictionary<string, string> Headers { get; set; }

		public byte[] ContentRaw { get; set; }

		public string Content { get; set; }
	}
}
