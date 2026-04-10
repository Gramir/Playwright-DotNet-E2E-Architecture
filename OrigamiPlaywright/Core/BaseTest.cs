using Microsoft.Playwright.NUnit;

namespace OrigamiPlaywright.Core
{
    public class BaseTest : PageTest
    {
        /// <remarks>
        /// Kept in the test base so environment changes only touch one place while
        /// all suites keep using relative routes and consistent URL composition.
        /// </remarks>
        protected readonly string BaseUrl = "https://the-internet.herokuapp.com";

        /// <remarks>
        /// Accepts relative paths so test methods express intent, while URL construction
        /// remains centralized and consistent in one place.
        /// </remarks>
        protected async Task NavigateTo(string path)
        {
            await Page.GotoAsync($"{BaseUrl}{path}");
        }
    }
}