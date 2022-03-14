using Bunit;
using Bunit.TestDoubles;
using Survey.Client.Pages;
using System.Collections.Generic;
using System.Linq;
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

            var component = ctx.RenderComponent<Index>();
            Assert.Contains("Board", component.Find($".nav-link").TextContent);
        }

    }
}
