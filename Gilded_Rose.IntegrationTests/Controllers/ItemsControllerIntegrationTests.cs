using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xunit;

namespace Gilded_Rose.IntegrationTests
{
    class ItemsControllerIntegrationTests : IClassFixture<WebApiTesterFactory<Startup>>
    {
        private readonly WebApiTesterFactory<Startup> _factory;

        public ItemsControllerIntegrationTests(WebApiTesterFactory<Startup> factory)
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



    }
}
