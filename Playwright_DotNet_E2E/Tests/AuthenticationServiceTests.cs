using Playwright_DotNet_E2E.Services;
using static Playwright_DotNet_E2E.Data.Constants.LoginData;

namespace Playwright_DotNet_E2E.Tests
{
    [Category("Infrastructure")]
    public class AuthenticationServiceTests
    {
        [Test]
        public void AuthService_ShouldCreateValidTestUser()
        {
            IAuthenticationService service = new TestAuthenticationService();

            var user = service.GetValidUser();

            Assert.Multiple(() =>
            {
                Assert.That(user.Username, Is.EqualTo(ValidUser));
                Assert.That(user.Password, Is.EqualTo(ValidPassword));
            });
        }

        [Test]
        public void AuthService_ShouldCreateInvalidUsernameUser()
        {
            IAuthenticationService service = new TestAuthenticationService();

            var user = service.GetInvalidUsernameUser();

            Assert.Multiple(() =>
            {
                Assert.That(user.Username, Is.EqualTo(InvalidUser));
                Assert.That(user.Password, Is.EqualTo(ValidPassword));
            });
        }

        [Test]
        public void AuthService_ShouldCreateInvalidPasswordUser()
        {
            IAuthenticationService service = new TestAuthenticationService();

            var user = service.GetInvalidPasswordUser();

            Assert.Multiple(() =>
            {
                Assert.That(user.Username, Is.EqualTo(ValidUser));
                Assert.That(user.Password, Is.EqualTo(InvalidPassword));
            });
        }
    }
}