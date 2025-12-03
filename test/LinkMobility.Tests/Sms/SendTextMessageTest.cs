using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AMWD.Net.Api.LinkMobility;
using LinkMobility.Tests.Helpers;
using Moq.Protected;

namespace LinkMobility.Tests.Sms
{
	[TestClass]
	public class SendTextMessageTest
	{
		public TestContext TestContext { get; set; }

		private const string BASE_URL = "https://localhost/rest/";

		private Mock<IAuthentication> _authenticationMock;
		private Mock<ClientOptions> _clientOptionsMock;
		private HttpMessageHandlerMock _httpMessageHandlerMock;

		private SendTextMessageRequest _request;

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

			_request = new SendTextMessageRequest("example message content", ["436991234567"]);
		}

		[TestMethod]
		public async Task ShouldSendTextMessage()
		{
			// Arrange
			_httpMessageHandlerMock.Responses.Enqueue(new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent(@"{ ""clientMessageId"": ""myUniqueId"", ""smsCount"": 1, ""statusCode"": 2000, ""statusMessage"": ""OK"", ""transferId"": ""0059d0b20100a0a8b803"" }", Encoding.UTF8, "application/json"),
			});

			var client = GetClient();

			// Act
			var response = await client.SendTextMessage(_request, TestContext.CancellationToken);

			// Assert
			Assert.IsNotNull(response);

			Assert.AreEqual("myUniqueId", response.ClientMessageId);
			Assert.AreEqual(1, response.SmsCount);
			Assert.AreEqual(StatusCodes.Ok, response.StatusCode);
			Assert.AreEqual("OK", response.StatusMessage);
			Assert.AreEqual("0059d0b20100a0a8b803", response.TransferId);

			Assert.HasCount(1, _httpMessageHandlerMock.RequestCallbacks);

			var callback = _httpMessageHandlerMock.RequestCallbacks.First();
			Assert.AreEqual(HttpMethod.Post, callback.HttpMethod);
			Assert.AreEqual("https://localhost/rest/smsmessaging/text", callback.Url);
			Assert.AreEqual(@"{""messageContent"":""example message content"",""recipientAddressList"":[""436991234567""]}", callback.Content);

			Assert.HasCount(3, callback.Headers);
			Assert.IsTrue(callback.Headers.ContainsKey("Accept"));
			Assert.IsTrue(callback.Headers.ContainsKey("Authorization"));
			Assert.IsTrue(callback.Headers.ContainsKey("User-Agent"));

			Assert.AreEqual("application/json", callback.Headers["Accept"]);
			Assert.AreEqual("Scheme Parameter", callback.Headers["Authorization"]);
			Assert.AreEqual("LinkMobilityClient/1.0.0", callback.Headers["User-Agent"]);

			_httpMessageHandlerMock.Mock
				.Protected()
				.Verify("SendAsync", Times.Once(), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());

			_clientOptionsMock.VerifyGet(o => o.DefaultQueryParams, Times.Once);
			VerifyNoOtherCalls();
		}

		[TestMethod]
		public async Task ShouldSendTextMessageFullDetails()
		{
			// Arrange
			_request.ClientMessageId = "myCustomId";
			_request.ContentCategory = ContentCategory.Informational;
			_request.MaxSmsPerMessage = 1;
			_request.MessageType = MessageType.Voice;
			_request.NotificationCallbackUrl = "https://user:pass@example.com/callback/";
			_request.Priority = 5;
			_request.SendAsFlashSms = false;
			_request.SenderAddress = "4369912345678";
			_request.SenderAddressType = AddressType.International;
			_request.Test = false;
			_request.ValidityPeriode = 300;

			_httpMessageHandlerMock.Responses.Enqueue(new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent(@"{ ""clientMessageId"": ""myCustomId"", ""smsCount"": 1, ""statusCode"": 4035, ""statusMessage"": ""SMS_DISABLED"", ""transferId"": ""0059d0b20100a0a8b803"" }", Encoding.UTF8, "application/json"),
			});

			var client = GetClient();

			// Act
			var response = await client.SendTextMessage(_request, TestContext.CancellationToken);

			// Assert
			Assert.IsNotNull(response);

			Assert.AreEqual("myCustomId", response.ClientMessageId);
			Assert.AreEqual(1, response.SmsCount);
			Assert.AreEqual(StatusCodes.SmsDisabled, response.StatusCode);
			Assert.AreEqual("SMS_DISABLED", response.StatusMessage);
			Assert.AreEqual("0059d0b20100a0a8b803", response.TransferId);

			Assert.HasCount(1, _httpMessageHandlerMock.RequestCallbacks);

			var callback = _httpMessageHandlerMock.RequestCallbacks.First();
			Assert.AreEqual(HttpMethod.Post, callback.HttpMethod);
			Assert.AreEqual("https://localhost/rest/smsmessaging/text", callback.Url);
			Assert.AreEqual(@"{""clientMessageId"":""myCustomId"",""contentCategory"":""informational"",""maxSmsPerMessage"":1,""messageContent"":""example message content"",""messageType"":""voice"",""notificationCallbackUrl"":""https://user:pass@example.com/callback/"",""priority"":5,""recipientAddressList"":[""436991234567""],""sendAsFlashSms"":false,""senderAddress"":""4369912345678"",""senderAddressType"":""international"",""test"":false,""validityPeriode"":300}", callback.Content);

			Assert.HasCount(3, callback.Headers);
			Assert.IsTrue(callback.Headers.ContainsKey("Accept"));
			Assert.IsTrue(callback.Headers.ContainsKey("Authorization"));
			Assert.IsTrue(callback.Headers.ContainsKey("User-Agent"));

			Assert.AreEqual("application/json", callback.Headers["Accept"]);
			Assert.AreEqual("Scheme Parameter", callback.Headers["Authorization"]);
			Assert.AreEqual("LinkMobilityClient/1.0.0", callback.Headers["User-Agent"]);

			_httpMessageHandlerMock.Mock
				.Protected()
				.Verify("SendAsync", Times.Once(), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());

			_clientOptionsMock.VerifyGet(o => o.DefaultQueryParams, Times.Once);
			VerifyNoOtherCalls();
		}

		[TestMethod]
		public void ShouldThrowOnNullRequest()
		{
			// Arrange
			var client = GetClient();

			// Act & Assert
			var ex = Assert.ThrowsExactly<ArgumentNullException>(() => client.SendTextMessage(null, TestContext.CancellationToken));
			Assert.AreEqual("request", ex.ParamName);

			VerifyNoOtherCalls();
		}

		[TestMethod]
		[DataRow(null)]
		[DataRow("")]
		[DataRow("   ")]
		public void ShouldThrowOnMissingMessage(string message)
		{
			// Arrange
			var req = new SendTextMessageRequest(message, ["4791234567"]);
			var client = GetClient();

			// Act & Assert
			var ex = Assert.ThrowsExactly<ArgumentException>(() => client.SendTextMessage(req, TestContext.CancellationToken));
			Assert.AreEqual("MessageContent", ex.ParamName);

			VerifyNoOtherCalls();
		}

		[TestMethod]
		public void ShouldThrowOnNoRecipients()
		{
			// Arrange
			var req = new SendTextMessageRequest("Hello", []);
			var client = GetClient();

			// Act & Assert
			var ex = Assert.ThrowsExactly<ArgumentException>(() => client.SendTextMessage(req, TestContext.CancellationToken));
			Assert.AreEqual("RecipientAddressList", ex.ParamName);

			VerifyNoOtherCalls();
		}

		[TestMethod]
		[DataRow(null)]
		[DataRow("")]
		[DataRow("   ")]
		[DataRow("invalid-recipient")]
		public void ShouldThrowOnInvalidRecipient(string recipient)
		{
			// Arrange
			var client = GetClient();
			var req = new SendTextMessageRequest("Hello", ["4791234567", recipient]);

			// Act & Assert
			var ex = Assert.ThrowsExactly<ArgumentException>(() => client.SendTextMessage(req, TestContext.CancellationToken));

			Assert.AreEqual("RecipientAddressList", ex.ParamName);
			Assert.StartsWith($"Recipient address '{recipient}' is not a valid MSISDN format.", ex.Message);

			VerifyNoOtherCalls();
		}

		private void VerifyNoOtherCalls()
		{
			_authenticationMock.VerifyNoOtherCalls();
			_clientOptionsMock.VerifyNoOtherCalls();
			_httpMessageHandlerMock.Mock.VerifyNoOtherCalls();
		}

		private ILinkMobilityClient GetClient()
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
