using Microsoft.Playwright;

namespace Playwright_DotNet_E2E.Pages
{
    /// <remarks>
    /// Page object for the secure area reached after authentication.
    /// It exposes only the post-login affordances that matter to test intent.
    /// </remarks>
    public class SecurePage(IPage page) : BasePage(page)
    {
        /// <remarks>
        /// The logout action is the main proof that authentication succeeded.
        /// </remarks>
        public ILocator LogoutButton => _page.GetByRole(AriaRole.Link, new() { Name = "Logout" });
    }
}