using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AMWD.Net.Api.LinkMobility;
using AMWD.Net.Api.LinkMobility.WhatsApp;
using LinkMobility.Tests.Helpers;
using Moq.Protected;

namespace LinkMobility.Tests.WhatsApp
{
	[TestClass]
	public class SendWhatsAppMessageTest
	{
		public TestContext TestContext { get; set; }

		private const string BASE_URL = "https://localhost/rest/";

		private Mock<IAuthentication> _authenticationMock;
		private Mock<ClientOptions> _clientOptionsMock;
		private HttpMessageHandlerMock _httpMessageHandlerMock;

		private Guid _uuid;
		private SendWhatsAppMessageRequest _request;

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

			_uuid = Guid.NewGuid();

			var image = new ImageMessageContent("https://example.com/image.jpg");
			image.Body.Caption = "Hello World :)";
			_request = new SendWhatsAppMessageRequest(image, ["436991234567"]);
		}

		[TestMethod]
		public async Task ShouldSendWhatsAppMessage()
		{
			// Arrange
			_httpMessageHandlerMock.Responses.Enqueue(new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent(@"{ ""clientMessageId"": ""myUniqueId"", ""statusCode"": 2000, ""statusMessage"": ""OK"", ""transferId"": ""0059d0b20100a0a8b803"" }", Encoding.UTF8, "application/json"),
			});

			var client = GetClient();

			// Act
			var response = await client.SendWhatsAppMessage(_uuid, _request, TestContext.CancellationToken);

			// Assert
			Assert.IsNotNull(response);

			Assert.AreEqual("myUniqueId", response.ClientMessageId);
			Assert.AreEqual(StatusCodes.Ok, response.StatusCode);
			Assert.AreEqual("OK", response.StatusMessage);
			Assert.AreEqual("0059d0b20100a0a8b803", response.TransferId);

			Assert.HasCount(1, _httpMessageHandlerMock.RequestCallbacks);

			var callback = _httpMessageHandlerMock.RequestCallbacks.First();
			Assert.AreEqual(HttpMethod.Post, callback.HttpMethod);
			Assert.AreEqual($"https://localhost/rest/channels/{_uuid}/send/whatsapp", callback.Url);
			Assert.AreEqual(@"{""messageContent"":{""type"":""image"",""image"":{""link"":""https://example.com/image.jpg"",""caption"":""Hello World :)""}},""recipientAddressList"":[""436991234567""]}", callback.Content);

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
		public void ShouldThrowOnNullRequest()
		{
			// Arrange
			var client = GetClient();

			// Act & Assert
			var ex = Assert.ThrowsExactly<ArgumentNullException>(() => client.SendWhatsAppMessage(_uuid, null, TestContext.CancellationToken));
			Assert.AreEqual("request", ex.ParamName);

			VerifyNoOtherCalls();
		}

		[TestMethod]
		public void ShouldThrowOnMissingMessage()
		{
			// Arrange
			var req = new SendWhatsAppMessageRequest(null, ["436991234567"]);
			var client = GetClient();

			// Act & Assert
			var ex = Assert.ThrowsExactly<ArgumentException>(() => client.SendWhatsAppMessage(_uuid, req, TestContext.CancellationToken));
			Assert.AreEqual("MessageContent", ex.ParamName);

			VerifyNoOtherCalls();
		}

		[TestMethod]
		public void ShouldThrowOnNoRecipients()
		{
			// Arrange
			var req = new SendWhatsAppMessageRequest(new TextMessageContent("Hello"), []);
			var client = GetClient();

			// Act & Assert
			var ex = Assert.ThrowsExactly<ArgumentException>(() => client.SendWhatsAppMessage(_uuid, req, TestContext.CancellationToken));
			Assert.AreEqual("RecipientAddressList", ex.ParamName);

			VerifyNoOtherCalls();
		}

		[TestMethod]
		public void ShouldThrowOnInvalidRecipient()
		{
			// Arrange
			var client = GetClient();
			var req = new SendWhatsAppMessageRequest(new TextMessageContent("Hello"), ["4791234567", "invalid-recipient"]);

			// Act & Assert
			var ex = Assert.ThrowsExactly<ArgumentException>(() => client.SendWhatsAppMessage(_uuid, req, TestContext.CancellationToken));

			Assert.AreEqual("RecipientAddressList", ex.ParamName);
			Assert.StartsWith($"Recipient address 'invalid-recipient' is not a valid MSISDN format.", ex.Message);

			VerifyNoOtherCalls();
		}

		[TestMethod]
		public void ShouldThrowOnInvalidContentCategory()
		{
			// Arrange
			_request.ContentCategory = 0;
			var client = GetClient();

			// Act & Assert
			var ex = Assert.ThrowsExactly<ArgumentException>(() => client.SendWhatsAppMessage(_uuid, _request, TestContext.CancellationToken));
			Assert.AreEqual("ContentCategory", ex.ParamName);
			Assert.StartsWith("Content category '0' is not valid.", ex.Message);

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

			_authenticationMock.Object.AddHeader(httpClient);

			_authenticationMock.Invocations.Clear();
			_clientOptionsMock.Invocations.Clear();

			ReflectionHelper.GetPrivateField<HttpClient>(client, "_httpClient")?.Dispose();
			ReflectionHelper.SetPrivateField(client, "_httpClient", httpClient);

			return client;
		}
	}
}
