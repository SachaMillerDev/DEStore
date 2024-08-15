using InventoryControlService.Controllers;
using InventoryControlService.Data;
using InventoryControlService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Net.Http;
using System.Text;
using Xunit;

namespace InventoryControlService.Tests
{
    public class InventoryItemsControllerTests
    {
        private readonly InventoryItemsController _controller;
        private readonly Mock<InventoryControlContext> _contextMock;
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;

        public InventoryItemsControllerTests()
        {
            _contextMock = new Mock<InventoryControlContext>();
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();

            // Setup a default mock behavior for HttpClientFactory if needed
            var httpClientMock = new HttpClient(new HttpMessageHandlerMock());
            _httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClientMock);

            _controller = new InventoryItemsController(_contextMock.Object, _httpClientFactoryMock.Object);
        }

        [Fact]
        public async Task UpdateInventoryItem_ReturnsNoContentResult()
        {
            // Arrange
            int testId = 1;
            InventoryItem testItem = new InventoryItem { Id = testId, Quantity = 10 };

            // Act
            var result = await _controller.PutInventoryItem(testId, testItem);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        // You can create more tests as needed
    }

    // Mock handler for HttpClient if you want to mock the response from HttpClient
    public class HttpMessageHandlerMock : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent("{\"Message\":\"Mocked response\"}", Encoding.UTF8, "application/json")
            };
            return Task.FromResult(response);
        }
    }
}
