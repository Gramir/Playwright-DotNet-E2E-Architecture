namespace Playwright_DotNet_E2E.Configuration
{
    /// <remarks>
    /// Strongly-typed configuration container for test automation settings.
    /// Binds to "TestSettings" section in appsettings.json.
    /// Properties expose centralized controls for timeouts and base URL.
    /// </remarks>
    public class TestSettings
    {
        /// <remarks>
        /// Base URL for the application under test. Used by BaseTest for centralized navigation.
        /// </remarks>
        public required string BaseUrl { get; set; }

        /// <remarks>
        /// Navigation timeout in milliseconds. Applied to explicit page navigation operations.
        /// </remarks>
        public required int NavigationTimeoutMs { get; set; }

        /// <remarks>
        /// Default element interaction timeout in milliseconds. Used for locator waits and assertions.
        /// </remarks>
        public required int DefaultTimeoutMs { get; set; }
    }
}
