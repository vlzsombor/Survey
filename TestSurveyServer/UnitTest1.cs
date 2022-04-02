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

        [Fact]
        public async Task TestCreateMethod()
        {
            var response = await _testServer.CreateRequest("/ifAlive").SendAsync("GET");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
