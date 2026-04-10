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

        // Intentionally kept as a ready-to-enable Setup/TearDown pattern in the base test.
        // The target site emits console errors
    /*
        private List <string> _consoleErrors;

        [SetUp]
        public void SetupConsoleListener()
        {
            _consoleErrors = [];

            Page.Console += (_,msg)=>
            {
                if (msg.Type == "error")
                {
                    _consoleErrors.Add($"[Console Error]{msg.Text}");
                }
            };          
        }

        [TearDown]
        public void VerifyConsoleErrors()
        {
            if (_consoleErrors.Count > 0)
            {
                var allErrors = string.Join("\n", _consoleErrors);
                Assert.Fail($"Test finished with console errors:\n{allErrors}");
            }
        }
        */
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