using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using Playwright_DotNet_E2E.Configuration;
using Playwright_DotNet_E2E.Core;
using Playwright_DotNet_E2E.Services;

namespace Playwright_DotNet_E2E.Startup
{
    /// <remarks>
    /// Centralized dependency injection container configuration.
    /// Registers TestSettings, AppManager factory, and transitive dependencies.
    /// This enables BaseTest to consume settings and components via DI.
    /// </remarks>
    public static class ServiceRegistration
    {
        /// <remarks>
        /// Registers core services and configuration into the dependency injection container.
        /// Call this during test setup or application startup.
        /// </remarks>
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            // Register TestSettings as a singleton bound to the TestSettings section
            var testSettings = configuration.GetSection("TestSettings").Get<TestSettings>()
                ?? throw new InvalidOperationException("TestSettings not configured in appsettings.json");
            services.AddSingleton(testSettings);

            // Register AppManager factory to allow per-test instantiation with IPage
            services.AddTransient<Func<IPage, AppManager>>(sp => page => new AppManager(page));

            // Register authentication service as test-data source for login flows
            services.AddSingleton<IAuthenticationService, TestAuthenticationService>();
        }
    }
}
