using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Survey.Shared.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestSurveyServer
{
    public class UnitTest1 : IDisposable
    {
        protected TestServer _testServer;

        public UnitTest1()
        {
            var webBuilder = new WebHostBuilder();
            webBuilder.UseStartup<Startup>();

            _testServer = new TestServer(webBuilder);

        }

        public void Dispose()
        {
            _testServer.Dispose();
        }
        //[Fact]
        //public async Task TestReadMehtods()
        //{
        //    var response = await _testServer.CreateRequest("/api/card").SendAsync("GET");

        //    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        //}

        [Fact]
        public async Task TestCreateMethod()
        {
            //var request = new HttpRequestMessage(HttpMethod.Post, "/WeatherForecas");
            //var bm = new BoardModel() { Name="bm" };

            //request.Content = new StringContent(JsonConvert.SerializeObject(bm), Encoding.Default, "application/json");


            //var client = _testServer.CreateClient();

            //client.DefaultRequestHeaders.Clear();

            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            //var response = await client.SendAsync(request);
            var response = await _testServer.CreateRequest("/WeatherForecast").SendAsync("GET");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var a = response.Content;

            var b = await a.ReadAsStringAsync();
        }


    }
}