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
        public void NotAuthorized()
        {
            using var ctx = new TestContext();
            ctx.AddTestAuthorization();


            var component = ctx.RenderComponent<Index>();
            Assert.Equal("Make your awesome anonymous survey", component.Find($"h1").TextContent);

        }       
        
        [Fact]
        public void Authorized()
        {
            using var ctx = new TestContext();
            var authContext = ctx.AddTestAuthorization();
            //authContext.SetAuthorizing();
            authContext.SetAuthorized("a@a.hu");
            //
            // Summary:
            //     Authenticates the user with specified name and authorization state.
            //
            // Parameters:
            //   userName:
            //     User name for the principal identity.
            //
            //   state:
            //     Authorization state.

            var component = ctx.RenderComponent<Index>();
            Assert.Contains("Board", component.Find($".nav-link").TextContent);

        }
    }
}
