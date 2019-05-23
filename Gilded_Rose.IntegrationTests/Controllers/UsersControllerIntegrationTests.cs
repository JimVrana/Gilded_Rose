using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xunit;
using FluentAssertions;
using Newtonsoft.Json.Linq;

namespace Gilded_Rose.IntegrationTests.Controllers
{
    public class UsersControllerIntegrationTests : IClassFixture<WebApiTesterFactory<Startup>>
    {
        private readonly WebApiTesterFactory<Startup> _factory;

        public UsersControllerIntegrationTests(WebApiTesterFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/Users/authenticate")]
        public  void get_request_should_return_ok(string url)
        {
            string username = "admin";
            string password = "admin";

            //Arrange
            var httpClient = _factory.CreateClient();

            var content = new StringContent($"{{\"username\":\"{username}\",\"password\":\"{password}\"}}", Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //Act
            var response = httpClient.PostAsync(url, content).Result;

            Action act = () => response.EnsureSuccessStatusCode();

            //Assert
            act.Should().NotThrow();

        } 

        [Theory]
        [InlineData("/api/Users/authenticate")]
        public  void get_valid_user_should_return_token(string url)
        {
            string username = "admin";
            string password = "admin";
            string access_token = string.Empty;
            //Arrange
            var httpClient = _factory.CreateClient();

            var content = new StringContent($"{{\"username\":\"{username}\",\"password\":\"{password}\"}}", Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //Act
            var response = httpClient.PostAsync(url, content).Result;

            var jsonOut = response.Content.ReadAsStringAsync();

            JObject rss = JObject.Parse(jsonOut.Result);

            access_token = rss["token"].ToString();


            //Assert
            access_token.Should().NotBeNullOrEmpty();

        }



        [Theory]
        [InlineData("/api/Users/authenticate")]
        public  void get_valid_user_with_wrong_password_should_return_password_incorrect(string url)
        {
            string username = "admin";
            string password = "wrong_password";
            string message = string.Empty;
            //Arrange
            var httpClient = _factory.CreateClient();

            var content = new StringContent($"{{\"username\":\"{username}\",\"password\":\"{password}\"}}", Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //Act
            var response = httpClient.PostAsync(url, content).Result;
            var jsonOut = response.Content.ReadAsStringAsync();
            JObject rss = JObject.Parse(jsonOut.Result);
            message = rss["message"].ToString();
            
            //Assert
            message.Should().Be("UserName or Password is incorrect");
        }

        [Theory]
        [InlineData("/api/Users/authenticate")]
        public void get_null_user_should_return_password_incorrect(string url)
        {
            string username = null;
            string password = null;
            string message = string.Empty;
            //Arrange
            var httpClient = _factory.CreateClient();

            var content = new StringContent($"{{\"username\":\"{username}\",\"password\":\"{password}\"}}", Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //Act
            var response = httpClient.PostAsync(url, content).Result;
            var jsonOut = response.Content.ReadAsStringAsync();
            JObject rss = JObject.Parse(jsonOut.Result);
            message = rss["message"].ToString();

            //Assert
            message.Should().Be("UserName or Password is incorrect");
        }


        [Theory]
        [InlineData("/api/Users/authenticate")]
        public void get_admin_should_be_admin_role(string url)
        {
            string username = "admin";
            string password = "admin";
            string role = string.Empty;
            //Arrange
            var httpClient = _factory.CreateClient();

            var content = new StringContent($"{{\"username\":\"{username}\",\"password\":\"{password}\"}}", Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //Act
            var response = httpClient.PostAsync(url, content).Result;
            var jsonOut = response.Content.ReadAsStringAsync();
            JObject rss = JObject.Parse(jsonOut.Result);
            role = rss["role"].ToString();

            //Assert
            role.Should().Be("Admin");
        }

        [Theory]
        [InlineData("/api/Users/authenticate")]
        public void get_jvrana_should_be_ApiUser(string url)
        {
            string username = "jvrana";
            string password = "passwordsAreFun";
            string role = string.Empty;
            //Arrange
            var httpClient = _factory.CreateClient();

            var content = new StringContent($"{{\"username\":\"{username}\",\"password\":\"{password}\"}}", Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //Act
            var response = httpClient.PostAsync(url, content).Result;
            var jsonOut = response.Content.ReadAsStringAsync();
            JObject rss = JObject.Parse(jsonOut.Result);
            role = rss["role"].ToString();

            //Assert
            role.Should().Be("ApiUser");
        }
    }

}
