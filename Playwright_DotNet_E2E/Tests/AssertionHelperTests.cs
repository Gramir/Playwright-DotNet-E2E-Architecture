using Playwright_DotNet_E2E.Core;
using static Playwright_DotNet_E2E.Data.Constants.LoginData;
using static Playwright_DotNet_E2E.Data.Constants.Routes;

namespace Playwright_DotNet_E2E.Tests
{
    /// <remarks>
    /// Verifies the reusable assertion helper against the real login flow.
    /// The tests intentionally exercise the helper rather than duplicating inline expectations.
    /// </remarks>
    public class AssertionHelperTests : BaseTest
    {
        [Test]
        [Category("Smoke")]
        public async Task CustomAssertion_LoginSuccessful_ShouldValidateLogoutAndFlash()
        {
            await NavigateTo(Login);

            await App.Login.LoginAsAsync(ValidUser, ValidPassword);

            await AssertionsHelper.AssertLoginSuccessfulAsync(App.SecureArea, SuccessMessage);
        }

        [Test]
        [Category("Smoke")]
        public async Task CustomAssertion_VerifyNavigationAndUrlMatch()
        {
            await NavigateTo(Login);

            await AssertionsHelper.AssertPageNavigatedToAsync($"{Settings.BaseUrl}{Login}");
        }

        [Test]
        [Category("Regression")]
        public async Task CustomAssertion_LoginFailed_ShouldValidateErrorAndStayOnLogin()
        {
            await NavigateTo(Login);

            await App.Login.LoginAsAsync(InvalidUser, InvalidPassword);

            await AssertionsHelper.AssertLoginFailedAsync(App.Login, ErrorMessageInvalidUser);
        }
    }
}