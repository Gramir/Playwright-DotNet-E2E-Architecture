using Microsoft.Playwright;
using Playwright_DotNet_E2E.Pages;

namespace Playwright_DotNet_E2E.Helpers
{
    /// <remarks>
    /// Centralizes common end-to-end assertions so tests stay at the business-intent level.
    /// This keeps repeated Playwright expectations out of the test bodies.
    /// </remarks>
    public class AssertionHelper(IPage page)
    {
        private readonly IPage _page = page;

        /// <remarks>
        /// Confirms the secure-area success signal in one place to reduce duplicate test logic.
        /// </remarks>
        public async Task AssertLoginSuccessfulAsync(SecurePage securePage, string expectedSuccessMessage)
        {
            await Assertions.Expect(securePage.LogoutButton).ToBeVisibleAsync();
            await Assertions.Expect(securePage.FlashMessage).ToContainTextAsync(expectedSuccessMessage);
        }

        /// <remarks>
        /// Confirms the login page remains interactive after an invalid attempt.
        /// </remarks>
        public async Task AssertLoginFailedAsync(LoginPage loginPage, string expectedErrorMessage)
        {
            await Assertions.Expect(loginPage.FlashMessage).ToContainTextAsync(expectedErrorMessage);
            await Assertions.Expect(loginPage.LoginButton).ToBeVisibleAsync();
        }

        /// <remarks>
        /// Keeps navigation checks central so URL assertions do not get scattered across suites.
        /// </remarks>
        public async Task AssertPageNavigatedToAsync(string expectedUrl)
        {
            await Assertions.Expect(_page).ToHaveURLAsync(expectedUrl);
        }
    }
}