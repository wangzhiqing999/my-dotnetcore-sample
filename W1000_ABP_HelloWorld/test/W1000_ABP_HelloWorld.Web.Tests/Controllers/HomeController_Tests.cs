using System.Threading.Tasks;
using W1000_ABP_HelloWorld.Web.Controllers;
using Shouldly;
using Xunit;

namespace W1000_ABP_HelloWorld.Web.Tests.Controllers
{
    public class HomeController_Tests: W1000_ABP_HelloWorldWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}
