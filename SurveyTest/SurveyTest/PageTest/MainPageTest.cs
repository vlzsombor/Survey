using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Radzen;
using Survey.Client.Pages;
using Survey.Client.Repository;
using Survey.Client.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SurveyTest.PageTest
{
    public class MainPageTest
    {
        [Fact]
        public void HtmlAssertion()
        {
            using var ctx = new TestContext();
            ctx.AddTestAuthorization();

            var component = ctx.RenderComponent<Index>();
            Assert.Equal("Make your awesome anonymous survey", component.Find($"h1").TextContent);
        }

        [Fact]
        public void AuthorizedTest()
        {
            using var ctx = new TestContext();
            var authContext = ctx.AddTestAuthorization();
            authContext.SetAuthorized("a@a.hu");
            HttpClient httpClient = new HttpClient();
            ctx.Services.AddSingleton<IBoardRepository>(new BoardRepository(httpClient));
            ctx.Services.AddSingleton<DialogService>();
            var component = ctx.RenderComponent<Index>();
            Assert.Contains("Board", component.Find($".rz-button-text").TextContent);
        }

    }
}
