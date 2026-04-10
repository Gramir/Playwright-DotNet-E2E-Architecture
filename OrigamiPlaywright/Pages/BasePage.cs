
using Microsoft.Playwright;

namespace OrigamiPlaywright.Pages
{
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