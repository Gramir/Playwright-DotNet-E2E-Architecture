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

        [SetUp]
        public void SetupBase()
        {
            var provider = _serviceProvider.Value;
            Settings = provider.GetRequiredService<TestSettings>();
            Page.SetDefaultTimeout(Settings.DefaultTimeoutMs);
            Page.SetDefaultNavigationTimeout(Settings.NavigationTimeoutMs);
            var appManagerFactory = provider.GetRequiredService<Func<IPage, AppManager>>();
            App = appManagerFactory(Page);
            AssertionsHelper = new AssertionHelper(Page);
            AuthService = provider.GetRequiredService<IAuthenticationService>();
        }
    
        /// <remarks>
        /// Navigates to a relative path appended to BaseUrl from configuration.
        /// </remarks>
        protected async Task NavigateTo(string relativePath)
        {
            var fullUrl = new Uri(new Uri(Settings.BaseUrl), relativePath).ToString();
            await Page.GotoAsync(fullUrl, new() { WaitUntil = WaitUntilState.DOMContentLoaded });
        }
    }
}