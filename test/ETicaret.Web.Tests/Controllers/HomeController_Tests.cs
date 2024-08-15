using System.Threading.Tasks;
using ETicaret.Models.TokenAuth;
using ETicaret.Web.Controllers;
using Shouldly;
using Xunit;

namespace ETicaret.Web.Tests.Controllers
{
    public class HomeController_Tests: ETicaretWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}