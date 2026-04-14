
using Microsoft.Playwright;

namespace Playwright_DotNet_E2E.Pages
{
    /// <remarks>
    /// Base page object for shared UI state that appears across multiple flows.
    /// Centralizing common locators here keeps derived pages focused on behavior.
    /// </remarks>
    public abstract class BasePage(IPage page)
    {
        protected readonly IPage _page = page;

        /// <remarks>
        /// Shared at base level because the same flash area is used by multiple flows
        /// (success and error feedback), giving one locator maintenance point.
        /// </remarks>
        public ILocator FlashMessage => _page.Locator("#flash");
    }
}