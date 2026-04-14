using Microsoft.Playwright;
using Playwright_DotNet_E2E.Components;

namespace Playwright_DotNet_E2E.Pages
{
    /// <remarks>
    /// Page object for the login screen. It keeps the interaction model small and intent-focused.
    /// </remarks>
    public class LoginPage(IPage page) : BasePage (page)
    {

        /// <remarks>
        /// Uses the shared alert component so feedback assertions stay consistent.
        /// </remarks>
        public AlertComponent Alert => new(_page);

        /// <remarks>
        /// Semantic locators keep the page object resilient to visual refactors.
        /// </remarks>
        public ILocator UsernameInput => _page.GetByLabel("username");

        /// <remarks>
        /// Semantic locators keep the page object resilient to visual refactors.
        /// </remarks>
        public ILocator PasswordInput => _page.GetByLabel("password");

        /// <remarks>
        /// The login action is kept as a single operation so tests can stay at intent level.
        /// </remarks>
        public ILocator LoginButton => _page.GetByRole(AriaRole.Button, new() { Name = "Login" });

        /// <remarks>
        /// Preserves the original imperative login flow for existing tests.
        /// </remarks>
        public async Task LoginAsAsync(string username, string password)
        {
            await UsernameInput.FillAsync(username);
            await PasswordInput.FillAsync(password);
            await LoginButton.ClickAsync();
        }

        /// <summary>
        /// Returns a fluent builder for expressive login flow with method chaining.
        /// </summary>
        /// <returns>A LoginFluentChain instance configured with this page.</returns>
        /// <remarks>
        /// Enables readable, chainable syntax like:
        /// await loginPage.AsFluentChain().WithUsername(user).WithPassword(pass).ThenLoginAsync();
        /// </remarks>
        public LoginFluentChain AsFluentChain() => new(this);
    }
}