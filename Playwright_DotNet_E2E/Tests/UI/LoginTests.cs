using Playwright_DotNet_E2E.Core;
using static Playwright_DotNet_E2E.Data.Constants.LoginData;
using static Playwright_DotNet_E2E.Data.Constants.Routes;

namespace Playwright_DotNet_E2E.Tests.UI
{
    [Category("UI")]
    public class LoginTests : BaseTest
    {

        [Test]
        [Category("Smoke")]
        public async Task SuccessfulLogin_ShouldRedirectToSecureArea()
        {
            await NavigateTo(Login);

            await App.Login.LoginAsAsync(ValidUser, ValidPassword);

            await AssertionsHelper.AssertLoginSuccessfulAsync(App.SecureArea, SuccessMessage);
            await AssertionsHelper.AssertPageNavigatedToAsync($"{Settings.BaseUrl}{SecureArea}");
        }

        [TestCase(InvalidUser, ValidPassword, ErrorMessageInvalidUser, TestName = "Login_InvalidUsername_ShowsError")]
        [TestCase(ValidUser, InvalidPassword, ErrorMessageInvalidPassword, TestName = "Login_InvalidPassword_ShowsError")]
        [Category("Regression")]
        public async Task InvalidLogin_ShouldShowErrorMessage(string username, string password, string expectedError)
        {
            await NavigateTo(Login);

            await App.Login.LoginAsAsync(username, password);

            await AssertionsHelper.AssertLoginFailedAsync(App.Login, expectedError);
        }

        /// <summary>
        /// Phase 4: Tests fluent login API with successful credentials.
        /// Demonstrates expressive method chaining for login flow.
        /// </summary>
        [Test]
        [Category("Smoke")]
        public async Task FluentAPI_ShouldAllowMethodChaining_ShouldRedirectToSecureArea()
        {
            await NavigateTo(Login);

            await App.Login
                .AsFluentChain()
                .WithUsername(ValidUser)
                .WithPassword(ValidPassword)
                .ThenLoginAsync();

            await AssertionsHelper.AssertLoginSuccessfulAsync(App.SecureArea, SuccessMessage);
            await AssertionsHelper.AssertPageNavigatedToAsync($"{Settings.BaseUrl}{SecureArea}");
        }

        /// <summary>
        /// Phase 4: Tests fluent login API with invalid credentials.
        /// Verifies error handling through expressive fluent chain.
        /// </summary>
        [TestCase(InvalidUser, ValidPassword, ErrorMessageInvalidUser, TestName = "FluentAPI_InvalidUsername_ShouldShowError")]
        [TestCase(ValidUser, InvalidPassword, ErrorMessageInvalidPassword, TestName = "FluentAPI_InvalidPassword_ShouldShowError")]
        [Category("Regression")]
        public async Task FluentAPI_WithInvalidCredentials_ShouldDisplayError(string username, string password, string expectedError)
        {
            await NavigateTo(Login);

            await App.Login
                .AsFluentChain()
                .WithUsername(username)
                .WithPassword(password)
                .ThenLoginAsync();

            await AssertionsHelper.AssertLoginFailedAsync(App.Login, expectedError);
        }

        [Test]
        [Category("Smoke")]
        public async Task LoginWithServiceUser_ShouldVerifyAllAssertions()
        {
            var validUser = AuthService.GetValidUser();

            await NavigateTo(Login);

            await App.Login.LoginAsAsync(validUser.Username, validUser.Password);

            await AssertionsHelper.AssertLoginSuccessfulAsync(App.SecureArea, SuccessMessage);
            await AssertionsHelper.AssertPageNavigatedToAsync($"{Settings.BaseUrl}{SecureArea}");
        }
    }
}