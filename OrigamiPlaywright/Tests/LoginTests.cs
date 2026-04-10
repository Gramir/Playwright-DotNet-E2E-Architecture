using OrigamiPlaywright.Core;
using OrigamiPlaywright.Pages;
using static OrigamiPlaywright.Data.Constants.LoginData;
using static OrigamiPlaywright.Data.Constants.Routes;

namespace OrigamiPlaywright.Tests
{
    public class LoginTests : BaseTest
    {
        private LoginPage _loginPage;
        private SecurePage _securePage;

        /// <remarks>
        /// Re-instantiating page objects per test keeps state isolated between runs,
        /// which prevents cross-test leakage as the suite grows.
        /// </remarks>
        [SetUp]
        public void SetupPages()
        {
            _loginPage = new LoginPage(Page);
            _securePage = new SecurePage(Page);
        }

        [Test]
        public async Task SuccessfulLogin_ShouldRedirectToSecureArea()
        {
            
            await NavigateTo(Login);

            await _loginPage.LoginAsAsync(ValidUser, ValidPassword);
            
            // Validate success through navigation, affordance, and feedback text to reduce false positives.
            await Expect(_securePage.LogoutButton).ToBeVisibleAsync();
            await Expect(_securePage.FlashMessage).ToContainTextAsync(SuccessMessage);
            await Expect(Page).ToHaveURLAsync($"{BaseUrl}{SecureArea}");
        }

        [TestCase(InvalidUser, ValidPassword, "Your username is invalid!", TestName = "Login_InvalidUsername_ShowsError")]
        [TestCase(ValidUser, InvalidPassword, "Your password is invalid!", TestName = "Login_InvalidPassword_ShowsError")]
        public async Task InvalidLogin_ShouldShowErrorMessage(string username, string password, string expectedError)
        {
            
            await NavigateTo(Login);
            
            await _loginPage.LoginAsAsync(username, password);
   
            await Expect(_loginPage.FlashMessage).ToContainTextAsync(expectedError);           
            await Expect(_loginPage.LoginButton).ToBeVisibleAsync();
            await Expect(Page).ToHaveURLAsync($"{BaseUrl}{Login}");
        }
    }
}