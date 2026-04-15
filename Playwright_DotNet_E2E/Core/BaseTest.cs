using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Playwright_DotNet_E2E.Configuration;
using Playwright_DotNet_E2E.Helpers;
using Playwright_DotNet_E2E.Services;
using Playwright_DotNet_E2E.Startup;

namespace Playwright_DotNet_E2E.Core
{
    public abstract class BaseTest : PageTest
    {
        private static readonly Lazy<IServiceProvider> _serviceProvider = new(() => BuildServiceProvider());
        protected AppManager App = null!;
        protected AssertionHelper AssertionsHelper = null!;
        protected IAuthenticationService AuthService = null!;
        protected TestSettings Settings = null!;

        /// <remarks>
        /// Initializes DI container once per test run.
        /// Loads appsettings.json and registers core services.
        /// </remarks>
        private static IServiceProvider BuildServiceProvider()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();
            ServiceRegistration.RegisterServices(services, configuration);
            return services.BuildServiceProvider();
        }

        /// <remarks>
        /// Configures shared test services and starts tracing for failure diagnostics.
        /// Trace is persisted only when a test fails.
        /// </remarks>
        [SetUp]
        public async Task SetupBase()
        {
            var provider = _serviceProvider.Value;
            Settings = provider.GetRequiredService<TestSettings>();
            Page.SetDefaultTimeout(Settings.DefaultTimeoutMs);
            Page.SetDefaultNavigationTimeout(Settings.NavigationTimeoutMs);
            var appManagerFactory = provider.GetRequiredService<Func<IPage, AppManager>>();
            App = appManagerFactory(Page);
            AssertionsHelper = new AssertionHelper(Page);
            AuthService = provider.GetRequiredService<IAuthenticationService>();

            await Context.Tracing.StartAsync(new()
            {
                Title = TestContext.CurrentContext.Test.Name,
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
        }
    
        /// <remarks>
        /// Navigates to a relative path appended to BaseUrl from configuration.
        /// </remarks>
        protected async Task NavigateTo(string relativePath)
        {
            var fullUrl = new Uri(new Uri(Settings.BaseUrl), relativePath).ToString();
            await Page.GotoAsync(fullUrl, new() { WaitUntil = WaitUntilState.DOMContentLoaded });
        }

        /// <remarks>
        /// Records browser videos for UI tests. Videos are finalized when context closes.
        /// </remarks>
        public override BrowserNewContextOptions ContextOptions()
        {
            var options = base.ContextOptions();
            var videosDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "TestResults", "Videos");
            Directory.CreateDirectory(videosDir);

            options.RecordVideoDir = videosDir;
            options.RecordVideoSize = new RecordVideoSize
            {
                Width = 1280,
                Height = 720
            };

            return options;
        }

        [TearDown]
        public async Task CaptureFailureArtifactsAsync()
        {
            var failed = TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed;

            var tracesDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "TestResults", "Traces");
            Directory.CreateDirectory(tracesDir);
            var safeTestName = string.Join("_", TestContext.CurrentContext.Test.Name.Split(Path.GetInvalidFileNameChars()));

            if (failed)
            {
                var tracePath = Path.Combine(tracesDir, $"{safeTestName}-{DateTime.UtcNow:yyyyMMddHHmmss}.zip");
                await Context.Tracing.StopAsync(new() { Path = tracePath });
                TestContext.AddTestAttachment(tracePath, "Failure trace");
            }
            else
            {
                await Context.Tracing.StopAsync();
            }

            if (failed)
            {
                var screenshotsDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "TestResults", "Artifacts");
                Directory.CreateDirectory(screenshotsDir);
                var screenshotPath = Path.Combine(screenshotsDir, $"{safeTestName}-{DateTime.UtcNow:yyyyMMddHHmmss}.png");

                await Page.ScreenshotAsync(new() { Path = screenshotPath, FullPage = true });
                TestContext.AddTestAttachment(screenshotPath, "Failure screenshot");
            }
        }
    }
}