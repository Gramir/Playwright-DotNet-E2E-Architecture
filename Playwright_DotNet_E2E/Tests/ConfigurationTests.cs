using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using Playwright_DotNet_E2E.Configuration;
using Playwright_DotNet_E2E.Core;
using Playwright_DotNet_E2E.Startup;

namespace Playwright_DotNet_E2E.Tests
{
    /// <remarks>
    /// Infrastructure tests for validating DI container and configuration bootstrap.
    /// These tests ensure TestSettings are correctly loaded and registered.
    /// </remarks>
    [TestFixture]
    [Category("Infrastructure")]
    public class ConfigurationTests
    {
        private IConfiguration? _configuration;
        private IServiceProvider? _serviceProvider;

        [SetUp]
        public void Setup()
        {
            // Build configuration from appsettings.json
            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = configBuilder.Build();

            // Register DI container using ServiceRegistration
            var services = new ServiceCollection();
            ServiceRegistration.RegisterServices(services, _configuration);
            _serviceProvider = services.BuildServiceProvider();
        }

        [Test]
        public void SettingsLoadedCorrectly_ShouldReadFromConfiguration()
        {
            // Arrange & Act
            var testSettings = _configuration?.GetSection("TestSettings").Get<TestSettings>();

            // Assert
            Assert.That(testSettings, Is.Not.Null, "TestSettings should be loaded from configuration");
            Assert.That(testSettings?.BaseUrl, Is.Not.Null.And.Not.Empty, "BaseUrl should be configured");
            Assert.That(testSettings?.NavigationTimeoutMs, Is.GreaterThan(0), "NavigationTimeoutMs should be positive");
            Assert.That(testSettings?.DefaultTimeoutMs, Is.GreaterThan(0), "DefaultTimeoutMs should be positive");
        }

        [Test]
        public void AppManagerInitializedViaFactory_ShouldResolveServices()
        {
            // Arrange & Act
            var testSettings = _serviceProvider?.GetRequiredService<TestSettings>();
            var appManagerFactory = _serviceProvider?.GetRequiredService<Func<IPage, AppManager>>();

            // Assert
            Assert.That(testSettings, Is.Not.Null, "TestSettings should be resolvable from DI");
            Assert.That(appManagerFactory, Is.Not.Null, "AppManager factory should be resolvable from DI");
        }

        [TearDown]
        public async Task Cleanup()
        {
            if (_serviceProvider is IAsyncDisposable asyncDisposable)
            {
                await asyncDisposable.DisposeAsync();
            }
            else if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
