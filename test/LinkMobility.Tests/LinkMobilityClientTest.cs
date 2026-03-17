using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AMWD.Net.Api.LinkMobility;
using LinkMobility.Tests.Helpers;
using Moq.Protected;

namespace LinkMobility.Tests
{
	[TestClass]
	public class LinkMobilityClientTest
	{
		public TestContext TestContext { get; set; }

		private const string BASE_URL = "https://localhost/rest/";

		private Mock<IAuthentication> _authenticationMock;
		private Mock<ClientOptions> _clientOptionsMock;
		private HttpMessageHandlerMock _httpMessageHandlerMock;

		private TestClass _request;

		[TestInitialize]
		public void Initialize()
		{
			_authenticationMock = new Mock<IAuthentication>();
			_clientOptionsMock = new Mock<ClientOptions>();
			_httpMessageHandlerMock = new HttpMessageHandlerMock();

			_authenticationMock
				.Setup(a => a.AddHeader(It.IsAny<HttpClient>()))
				.Callback<HttpClient>(c => c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Scheme", "Parameter"));

			_clientOptionsMock.Setup(c => c.BaseUrl).Returns(BASE_URL);
			_clientOptionsMock.Setup(c => c.Timeout).Returns(TimeSpan.FromSeconds(30));
			_clientOptionsMock.Setup(c => c.DefaultHeaders).Returns(new Dictionary<string, string>());
			_clientOptionsMock.Setup(c => c.DefaultQueryParams).Returns(new Dictionary<string, string>());
			_clientOptionsMock.Setup(c => c.AllowRedirects).Returns(true);
			_clientOptionsMock.Setup(c => c.UseProxy).Returns(false);

			_request = new()
			{
				Str = "Happy Testing",
				Int = 54321
			};
		}

		[TestMethod]
		public void ShouldInitializeWithBasicAuth()
		{
			// Arrange
			string username = "user";
			string password = "pass";
			string expectedParameter = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));

			// Act
			using var client = new LinkMobilityClient(username, password);

			// Assert
			var httpClient = ReflectionHelper.GetPrivateField<HttpClient>(client, "_httpClient");

			Assert.IsNotNull(httpClient);
			Assert.IsNotNull(httpClient.DefaultRequestHeaders.Authorization);
			Assert.AreEqual("Basic", httpClient.DefaultRequestHeaders.Authorization.Scheme);
			Assert.AreEqual(expectedParameter, httpClient.DefaultRequestHeaders.Authorization.Parameter);

			VerifyNoOtherCalls();
		}

		[TestMethod]
		public void ShouldInitializeWithBearerAuth()
		{
			// Arrange
			string token = "test_token";

			// Act
			using var client = new LinkMobilityClient(token);

			// Assert
			var httpClient = ReflectionHelper.GetPrivateField<HttpClient>(client, "_httpClient");

			Assert.IsNotNull(httpClient);
			Assert.IsNotNull(httpClient.DefaultRequestHeaders.Authorization);
			Assert.AreEqual("Bearer", httpClient.DefaultRequestHeaders.Authorization.Scheme);
			Assert.AreEqual(token, httpClient.DefaultRequestHeaders.Authorization.Parameter);

			VerifyNoOtherCalls();
		}

		[TestMethod]
		public void ShouldThrowOnNullAuthentication()
		{
			// Arrange

			// Act & Assert
			var ex = Assert.ThrowsExactly<ArgumentNullException>(() => new LinkMobilityClient((IAuthentication)null));

			Assert.AreEqual("authentication", ex.ParamName);

			VerifyNoOtherCalls();
		}

		[TestMethod]
		public void ShouldAddCustomDefaultHeaders()
		{
			// Arrange
			var clientOptions = new ClientOptions();
			clientOptions.DefaultHeaders.Add("SomeKey", "SomeValue");

			// Act
			using var client = new LinkMobilityClient("token", clientOptions);

			// Assert
			var httpClient = ReflectionHelper.GetPrivateField<HttpClient>(client, "_httpClient");

			Assert.IsNotNull(httpClient);
			Assert.IsTrue(httpClient.DefaultRequestHeaders.Contains("SomeKey"));
			Assert.AreEqual("SomeValue", httpClient.DefaultRequestHeaders.GetValues("SomeKey").First());

			VerifyNoOtherCalls();
		}

		[TestMethod]
		public async Task ShouldAddDefaultQueryParameters()
		{
			// Arrange
			_clientOptionsMock
				.Setup(o => o.DefaultQueryParams)
				.Returns(new Dictionary<string, string>
				{
					{ "SomeKey", "Some Value" },
					{ "key2", "param2" }
				});
			_httpMessageHandlerMock.Responses.Enqueue(new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent(@"{ ""string"": ""some-string"", ""integer"": 123 }", Encoding.UTF8, "application/json"),
			});

			var client = GetClient();

			// Act
			var response = await client.PostAsync<TestClass, TestClass>("test", _request, TestContext.CancellationToken);

			// Assert
			Assert.IsNotNull(response);

			Assert.HasCount(1, _httpMessageHandlerMock.RequestCallbacks);

			var callback = _httpMessageHandlerMock.RequestCallbacks.First();
			Assert.AreEqual(HttpMethod.Post, callback.HttpMethod);
			Assert.AreEqual("https://localhost/rest/test?SomeKey=Some+Value&key2=param2", callback.Url);
			Assert.AreEqual(@"{""string"":""Happy Testing"",""integer"":54321}", callback.Content);

			Assert.HasCount(3, callback.Headers);
			Assert.IsTrue(callback.Headers.ContainsKey("Accept"));
			Assert.IsTrue(callback.Headers.ContainsKey("Authorization"));
			Assert.IsTrue(callback.Headers.ContainsKey("User-Agent"));

			Assert.AreEqual("application/json", callback.Headers["Accept"]);
			Assert.AreEqual("Scheme Parameter", callback.Headers["Authorization"]);
			Assert.AreEqual("LinkMobilityClient/1.0.0", callback.Headers["User-Agent"]);

			_httpMessageHandlerMock.Protected.Verify("SendAsync", Times.Once(), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());

			_clientOptionsMock.VerifyGet(o => o.DefaultQueryParams, Times.Exactly(2));
			VerifyNoOtherCalls();
		}

		[TestMethod]
		public void ShouldDisposeHttpClient()
		{
			// Arrange
			var client = GetClient();

			// Act
			client.Dispose();

			// Assert
			_httpMessageHandlerMock.Protected.Verify("Dispose", Times.Once(), exactParameterMatch: true, true);

			VerifyNoOtherCalls();
		}

		[TestMethod]
		public void ShouldAllowMultipleDispose()
		{
			// Arrange
			var client = GetClient();

			// Act
			client.Dispose();
			client.Dispose();

			// Assert
			_httpMessageHandlerMock.Protected.Verify("Dispose", Times.Once(), exactParameterMatch: true, true);

			VerifyNoOtherCalls();
		}

		[TestMethod]
		public void ShouldAssertClientOptions()
		{
			// Arrange + Act
			_ = GetClient();

			// Assert
			VerifyNoOtherCalls();
		}

		[TestMethod]
		[DataRow(null)]
		[DataRow("")]
		[DataRow("   ")]
		public void ShouldThrowArgumentNullForBaseUrlOnAssertClientOptions(string baseUrl)
		{
			// Arrange
			_clientOptionsMock
				.Setup(o => o.BaseUrl)
				.Returns(baseUrl);

			// Act & Assert
			Assert.ThrowsExactly<ArgumentNullException>(() =>
			{
				var client = GetClient();
			});
		}

		[TestMethod]
		public void ShouldThrowArgumentOutOfRangeForTimeoutOnAssertClientOptions()
		{
			// Arrange
			_clientOptionsMock
				.Setup(o => o.Timeout)
				.Returns(TimeSpan.Zero);

			// Act & Assert
			Assert.ThrowsExactly<ArgumentOutOfRangeException>(() =>
			{
				var client = GetClient();
			});
		}

		[TestMethod]
		public void ShouldThrowArgumentNullForUseProxyOnAssertClientOptions()
		{
			// Arrange
			_clientOptionsMock
				.Setup(o => o.UseProxy)
				.Returns(true);

			// Act & Assert
			Assert.ThrowsExactly<ArgumentNullException>(() =>
			{
				var client = GetClient();
			});
		}

		[TestMethod]
		public async Task ShouldThrowDisposed()
		{
			// Arrange
			var client = GetClient();
			client.Dispose();

			// Act & Assert
			await Assert.ThrowsExactlyAsync<ObjectDisposedException>(async () =>
			{
				await client.PostAsync<object, TestClass>("/request/path", _request, TestContext.CancellationToken);
			});
		}

		[TestMethod]
		[DataRow(null)]
		[DataRow("")]
		[DataRow("   ")]
		public async Task ShouldThrowArgumentNullOnRequestPath(string path)
		{
			// Arrange
			var client = GetClient();

			// Act & Assert
			await Assert.ThrowsExactlyAsync<ArgumentNullException>(async () =>
			{
				await client.PostAsync<object, TestClass>(path, _request, TestContext.CancellationToken);
			});
		}

		[TestMethod]
		public async Task ShouldThrowArgumentOnRequestPath()
		{
			// Arrange
			var client = GetClient();

			// Act & Assert
			await Assert.ThrowsExactlyAsync<ArgumentException>(async () =>
			{
				await client.PostAsync<object, TestClass>("foo?bar=baz", _request, TestContext.CancellationToken);
			});
		}

		[TestMethod]
		public async Task ShouldPost()
		{
			// Arrange
			_httpMessageHandlerMock.Responses.Enqueue(new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent(@"{ ""string"": ""some-string"", ""integer"": 123 }", Encoding.UTF8, "application/json"),
			});

			var client = GetClient();

			// Act
			var response = await client.PostAsync<TestClass, TestClass>("/request/path", _request, TestContext.CancellationToken);

			// Assert
			Assert.IsNotNull(response);
			Assert.AreEqual("some-string", response.Str);
			Assert.AreEqual(123, response.Int);

			Assert.HasCount(1, _httpMessageHandlerMock.RequestCallbacks);

			var callback = _httpMessageHandlerMock.RequestCallbacks.First();
			Assert.AreEqual(HttpMethod.Post, callback.HttpMethod);
			Assert.AreEqual("https://localhost/rest/request/path", callback.Url);
			Assert.AreEqual(@"{""string"":""Happy Testing"",""integer"":54321}", callback.Content);

			Assert.HasCount(3, callback.Headers);
			Assert.IsTrue(callback.Headers.ContainsKey("Accept"));
			Assert.IsTrue(callback.Headers.ContainsKey("Authorization"));
			Assert.IsTrue(callback.Headers.ContainsKey("User-Agent"));

			Assert.AreEqual("application/json", callback.Headers["Accept"]);
			Assert.AreEqual("Scheme Parameter", callback.Headers["Authorization"]);
			Assert.AreEqual("LinkMobilityClient/1.0.0", callback.Headers["User-Agent"]);

			_httpMessageHandlerMock.Protected.Verify("SendAsync", Times.Once(), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());

			_clientOptionsMock.VerifyGet(o => o.DefaultQueryParams, Times.Once);
			VerifyNoOtherCalls();
		}

		[TestMethod]
		public async Task ShouldPostHttpContentDirectly()
		{
			// Arrange
			var stringContent = new StringContent(@"{""test"":""HERE ?""}", Encoding.UTF8, "application/json");
			_httpMessageHandlerMock.Responses.Enqueue(new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent(@"{ ""string"": ""some-string"", ""integer"": 123 }", Encoding.UTF8, "application/json"),
			});

			var client = GetClient();

			// Act
			var response = await client.PostAsync<TestClass, HttpContent>("/request/path", stringContent, TestContext.CancellationToken);

			// Assert
			Assert.IsNotNull(response);
			Assert.AreEqual("some-string", response.Str);
			Assert.AreEqual(123, response.Int);

			Assert.HasCount(1, _httpMessageHandlerMock.RequestCallbacks);

			var callback = _httpMessageHandlerMock.RequestCallbacks.First();
			Assert.AreEqual(HttpMethod.Post, callback.HttpMethod);
			Assert.AreEqual("https://localhost/rest/request/path", callback.Url);
			Assert.AreEqual(@"{""test"":""HERE ?""}", callback.Content);

			Assert.HasCount(3, callback.Headers);
			Assert.IsTrue(callback.Headers.ContainsKey("Accept"));
			Assert.IsTrue(callback.Headers.ContainsKey("Authorization"));
			Assert.IsTrue(callback.Headers.ContainsKey("User-Agent"));

			Assert.AreEqual("application/json", callback.Headers["Accept"]);
			Assert.AreEqual("Scheme Parameter", callback.Headers["Authorization"]);
			Assert.AreEqual("LinkMobilityClient/1.0.0", callback.Headers["User-Agent"]);

			_httpMessageHandlerMock.Protected.Verify("SendAsync", Times.Once(), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());

			_clientOptionsMock.VerifyGet(o => o.DefaultQueryParams, Times.Once);
			VerifyNoOtherCalls();
		}

		[TestMethod]
		public async Task ShouldPostWithoutContent()
		{
			// Arrange
			_httpMessageHandlerMock.Responses.Enqueue(new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent(@"{ ""string"": ""some-string"", ""integer"": 123 }", Encoding.UTF8, "application/json"),
			});

			var client = GetClient();

			// Act
			var response = await client.PostAsync<TestClass, object>("posting", null, TestContext.CancellationToken);

			// Assert
			Assert.IsNotNull(response);
			Assert.AreEqual("some-string", response.Str);
			Assert.AreEqual(123, response.Int);

			Assert.HasCount(1, _httpMessageHandlerMock.RequestCallbacks);

			var callback = _httpMessageHandlerMock.RequestCallbacks.First();
			Assert.AreEqual(HttpMethod.Post, callback.HttpMethod);
			Assert.AreEqual("https://localhost/rest/posting", callback.Url);
			Assert.IsNull(callback.Content);
			Assert.IsNull(callback.ContentRaw);

			Assert.HasCount(3, callback.Headers);
			Assert.IsTrue(callback.Headers.ContainsKey("Accept"));
			Assert.IsTrue(callback.Headers.ContainsKey("Authorization"));
			Assert.IsTrue(callback.Headers.ContainsKey("User-Agent"));

			Assert.AreEqual("application/json", callback.Headers["Accept"]);
			Assert.AreEqual("Scheme Parameter", callback.Headers["Authorization"]);
			Assert.AreEqual("LinkMobilityClient/1.0.0", callback.Headers["User-Agent"]);

			_httpMessageHandlerMock.Protected.Verify("SendAsync", Times.Once(), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());
		}

		[TestMethod]
		[DataRow(HttpStatusCode.Unauthorized)]
		[DataRow(HttpStatusCode.Forbidden)]
		public async Task ShouldThrowAuthenticationExceptionOnStatusCode(HttpStatusCode statusCode)
		{
			// Arrange
			_httpMessageHandlerMock.Responses.Enqueue(new HttpResponseMessage
			{
				StatusCode = statusCode,
				Content = new StringContent(@"", Encoding.UTF8, "application/json"),
			});

			var client = GetClient();

			// Act & Assert
			var ex = await Assert.ThrowsExactlyAsync<AuthenticationException>(async () =>
			{
				await client.PostAsync<object, TestClass>("foo", _request, TestContext.CancellationToken);
			});
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual($"HTTP auth missing: {statusCode}", ex.Message);
		}

		[TestMethod]
		[DataRow(HttpStatusCode.NotFound)]
		[DataRow(HttpStatusCode.InternalServerError)]
		public async Task ShouldThrowApplicationExceptionOnStatusCode(HttpStatusCode statusCode)
		{
			// Arrange
			_httpMessageHandlerMock.Responses.Enqueue(new HttpResponseMessage
			{
				StatusCode = statusCode,
				Content = new StringContent(@"", Encoding.UTF8, "application/json"),
			});

			var client = GetClient();

			// Act & Assert
			var ex = await Assert.ThrowsExactlyAsync<ApplicationException>(async () =>
			{
				await client.PostAsync<object, TestClass>("foo", _request, TestContext.CancellationToken);
			});
			Assert.IsNull(ex.InnerException);
			Assert.AreEqual($"Unknown HTTP response: {statusCode}", ex.Message);
		}

		[TestMethod]
		public async Task ShouldThrowExceptionOnInvalidResponse()
		{
			// Arrange
			_httpMessageHandlerMock.Responses.Enqueue(new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent("This is a bad text :p", Encoding.UTF8, "text/plain"),
			});

			var client = GetClient();

			// Act & Assert
			await Assert.ThrowsExactlyAsync<JsonReaderException>(async () =>
			{
				await client.PostAsync<TestClass, TestClass>("some-path", _request, TestContext.CancellationToken);
			});
		}

		[TestMethod]
		public async Task ShouldOnlySerializeNonNullValues()
		{
			// Arrange
			_request.Str = null;
			_httpMessageHandlerMock.Responses.Enqueue(new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent("\"This is an awesome text ;-)\"", Encoding.UTF8, "text/plain"),
			});

			var client = GetClient();

			// Act
			string response = await client.PostAsync<string, TestClass>("path", _request, TestContext.CancellationToken);

			// Assert
			Assert.IsNotNull(response);

			Assert.AreEqual("This is an awesome text ;-)", response);

			Assert.HasCount(1, _httpMessageHandlerMock.RequestCallbacks);

			var callback = _httpMessageHandlerMock.RequestCallbacks.First();
			Assert.AreEqual(HttpMethod.Post, callback.HttpMethod);
			Assert.AreEqual("https://localhost/rest/path", callback.Url);
			Assert.AreEqual(@"{""integer"":54321}", callback.Content);

			Assert.HasCount(3, callback.Headers);
			Assert.IsTrue(callback.Headers.ContainsKey("Accept"));
			Assert.IsTrue(callback.Headers.ContainsKey("Authorization"));
			Assert.IsTrue(callback.Headers.ContainsKey("User-Agent"));

			Assert.AreEqual("application/json", callback.Headers["Accept"]);
			Assert.AreEqual("Scheme Parameter", callback.Headers["Authorization"]);
			Assert.AreEqual("LinkMobilityClient/1.0.0", callback.Headers["User-Agent"]);

			_httpMessageHandlerMock.Protected.Verify("SendAsync", Times.Once(), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());

			_clientOptionsMock.VerifyGet(o => o.DefaultQueryParams, Times.Once);
			VerifyNoOtherCalls();
		}

		private void VerifyNoOtherCalls()
		{
			_authenticationMock.VerifyNoOtherCalls();
			_clientOptionsMock.VerifyNoOtherCalls();
			_httpMessageHandlerMock.Mock.VerifyNoOtherCalls();
		}

		private LinkMobilityClient GetClient()
		{
			var client = new LinkMobilityClient(_authenticationMock.Object, _clientOptionsMock.Object);

			var httpClient = new HttpClient(_httpMessageHandlerMock.Mock.Object)
			{
				Timeout = _clientOptionsMock.Object.Timeout,
				BaseAddress = new Uri(_clientOptionsMock.Object.BaseUrl)
			};

			httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("LinkMobilityClient", "1.0.0"));
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			if (_clientOptionsMock.Object.DefaultHeaders.Count > 0)
			{
				foreach (var headerKvp in _clientOptionsMock.Object.DefaultHeaders)
					httpClient.DefaultRequestHeaders.Add(headerKvp.Key, headerKvp.Value);
			}
			_authenticationMock.Object.AddHeader(httpClient);

			_authenticationMock.Invocations.Clear();
			_clientOptionsMock.Invocations.Clear();

			ReflectionHelper.GetPrivateField<HttpClient>(client, "_httpClient")?.Dispose();
			ReflectionHelper.SetPrivateField(client, "_httpClient", httpClient);

			return client;
		}

		private class TestClass
		{
			[JsonProperty("string")]
			public string Str { get; set; }

			[JsonProperty("integer")]
			public int Int { get; set; }
		}
	}
}
