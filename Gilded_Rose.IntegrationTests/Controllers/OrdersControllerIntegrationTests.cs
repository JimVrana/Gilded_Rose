using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xunit;
using FluentAssertions;

namespace Gilded_Rose.IntegrationTests.Controllers
{
    public class OrdersControllerIntegrationTests : IClassFixture<WebApiTesterFactory<Startup>>
    {
        private readonly WebApiTesterFactory<Startup> _factory;

        public OrdersControllerIntegrationTests(WebApiTesterFactory<Startup> factory)
        {
            _factory = factory;
        }

        public string GetToken(string username, string password)
        {
            string access_token = string.Empty;

            var httpClient = _factory.CreateClient();
            var content = new StringContent($"{{\"username\":\"{username}\",\"password\":\"{password}\"}}", Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = httpClient.PostAsync("/api/Users/authenticate", content).Result;
            var jsonOut = response.Content.ReadAsStringAsync();
            JObject rss = JObject.Parse(jsonOut.Result);
            access_token = rss["token"].ToString();

            return access_token;
        }

        [Theory]
        [InlineData("/api/Order")]
        public void post_order_should_return_success(string url)
        {
            //Arrange
            string username = "admin";
            string password = "admin";
            string access_token = GetToken(username,password);

            int ItemId = 1;
            int Quantity = 1;

            url += $"/{ItemId}/{Quantity}";
            var httpClient = _factory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            var content = new StringContent($"", Encoding.UTF8, "application/json");         

            //Act
            var response = httpClient.PostAsync(url, content).Result;
            var jsonOut = response.Content.ReadAsStringAsync();
            JObject rss = JObject.Parse(jsonOut.Result);
            string message = rss["message"].ToString();

          
            //Assert
            message.Should().Equals("Order successfully placed");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("/api/Order")]
        public void User_Role_should_return_unauthorized(string url)
        {
            //Arrange
            string username = "testuser";
            string password = "password2";
            string access_token = GetToken(username, password);

            int ItemId = 1;
            int Quantity = 1;

            url += $"/{ItemId}/{Quantity}";
            var httpClient = _factory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            var content = new StringContent($"", Encoding.UTF8, "application/json");

            //Act
            var response = httpClient.PostAsync(url, content).Result;
          
            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Forbidden);
        }

        [Theory]
        [InlineData("/api/Order")]
        public void ApiUser_Role_should_successfully_place_order(string url)
        {
            //Arrange
            string username = "jvrana";
            string password = "passwordsAreFun";
            string access_token = GetToken(username, password);

            int ItemId = 1;
            int Quantity = 1;

            url += $"/{ItemId}/{Quantity}";
            var httpClient = _factory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            var content = new StringContent($"", Encoding.UTF8, "application/json");

            //Act
            var response = httpClient.PostAsync(url, content).Result;
            var jsonOut = response.Content.ReadAsStringAsync();
            JObject rss = JObject.Parse(jsonOut.Result);
            string message = rss["message"].ToString();


            //Assert
            message.Should().Equals("Order successfully placed");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("/api/Order")]
        public void Unknown_item_should_fail(string url)
        {
            //Arrange
            string username = "jvrana";
            string password = "passwordsAreFun";
            string access_token = GetToken(username, password);

            int ItemId = 999;
            int Quantity = 1;

            url += $"/{ItemId}/{Quantity}";
            var httpClient = _factory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            var content = new StringContent($"", Encoding.UTF8, "application/json");

            //Act
            var response = httpClient.PostAsync(url, content).Result;
            var jsonOut = response.Content.ReadAsStringAsync();
            JObject rss = JObject.Parse(jsonOut.Result);
            string message = rss["message"].ToString();


            //Assert
            message.Should().Equals("This is an invalid Item.");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("/api/Order")]
        public void Request_Over_Stock_Amount_item_should_fail(string url)
        {
            //Arrange
            string username = "jvrana";
            string password = "passwordsAreFun";
            string access_token = GetToken(username, password);

            int ItemId = 1;
            int Quantity = 999;

            url += $"/{ItemId}/{Quantity}";
            var httpClient = _factory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            var content = new StringContent($"", Encoding.UTF8, "application/json");

            //Act
            var response = httpClient.PostAsync(url, content).Result;
            var jsonOut = response.Content.ReadAsStringAsync();
            JObject rss = JObject.Parse(jsonOut.Result);
            string message = rss["message"].ToString();


            //Assert
            message.Should().Equals("This order exceeds the maximum stock.");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("/api/Order")]
        public void Request_Negative_ItemId_should_fail(string url)
        {
            //Arrange
            string username = "jvrana";
            string password = "passwordsAreFun";
            string access_token = GetToken(username, password);

            int ItemId = -1;
            int Quantity = 1;

            url += $"/{ItemId}/{Quantity}";
            var httpClient = _factory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            var content = new StringContent($"", Encoding.UTF8, "application/json");

            //Act
            var response = httpClient.PostAsync(url, content).Result;
            var jsonOut = response.Content.ReadAsStringAsync();
            JObject rss = JObject.Parse(jsonOut.Result);
            string message = rss["message"].ToString();


            //Assert
            message.Should().Equals("ItemId must be greater or equal to 1");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData("/api/Order")]
        public void Request_Negative_Stock_Amount_item_should_fail(string url)
        {
            //Arrange
            string username = "jvrana";
            string password = "passwordsAreFun";
            string access_token = GetToken(username, password);

            int ItemId = 1;
            int Quantity = -1;

            url += $"/{ItemId}/{Quantity}";
            var httpClient = _factory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            var content = new StringContent($"", Encoding.UTF8, "application/json");

            //Act
            var response = httpClient.PostAsync(url, content).Result;
            var jsonOut = response.Content.ReadAsStringAsync();
            JObject rss = JObject.Parse(jsonOut.Result);
            string message = rss["message"].ToString();


            //Assert
            message.Should().Equals("Quantity must be greater or equal to 1");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
