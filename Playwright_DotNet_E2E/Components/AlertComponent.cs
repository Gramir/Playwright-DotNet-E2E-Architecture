using Microsoft.Playwright;
using Playwright_DotNet_E2E.Pages;

namespace Playwright_DotNet_E2E.Components
{
    /// <remarks>
    /// Reusable alert wrapper that reads the shared flash area from BasePage.
    /// Keeps feedback assertions in one place without duplicating the locator.
    /// </remarks>
    public class AlertComponent(IPage page) : BasePage(page)
    {
        /// <remarks>
        /// Exposes the shared flash area through the component boundary so tests read intent, not locators.
        /// </remarks>
        public ILocator FlashBanner => FlashMessage;

        /// <remarks>
        /// Centralized assertion for message feedback so tests express intent rather than locator details.
        /// </remarks>
        public async Task ValidateMessageContainsAsync(string expectedText)
        {
            await Assertions.Expect(FlashBanner).ToContainTextAsync(expectedText);
        }
    }
}