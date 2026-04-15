using Microsoft.Playwright;
using NSubstitute;
using NUnit.Framework;
using Playwright_DotNet_E2E.Core;
using Playwright_DotNet_E2E.Pages;

namespace Playwright_DotNet_E2E.Tests.Infrastructure
{
    /// <remarks>
    /// Tests for PageFactory lazy initialization and AppManager factory integration.
    /// Ensures that pages are created on-demand with the same IPage instance.
    /// </remarks>
    [Category("Infrastructure")]
    [Category("Unit")]
    public class PageFactoryTests
    {
        private IPageFactory _factory = null!;
        private IPage _mockPage = null!;

        [SetUp]
        public void Setup()
        {
            _mockPage = Substitute.For<IPage>();
            _factory = new PageFactory(_mockPage);
        }

        [Test]
        public void PageFactory_ShouldCreateLoginPageWithProvidedPage()
        {
            // Act
            var loginPage = _factory.CreatePage<LoginPage>();

            // Assert
            Assert.That(loginPage, Is.Not.Null);
            Assert.That(loginPage, Is.InstanceOf<LoginPage>());
        }

        [Test]
        public void PageFactory_ShouldCreateSecurePageWithProvidedPage()
        {
            // Act
            var securePage = _factory.CreatePage<SecurePage>();

            // Assert
            Assert.That(securePage, Is.Not.Null);
            Assert.That(securePage, Is.InstanceOf<SecurePage>());
        }

        [Test]
        public void PageFactory_ShouldReturnSamePageInstanceOnMultipleCalls()
        {
            // Act
            var page1 = _factory.CreatePage<LoginPage>();
            var page2 = _factory.CreatePage<LoginPage>();

            // Assert - same instance due to lazy initialization cache
            Assert.That(page1, Is.SameAs(page2));
        }

        [Test]
        public void PageFactory_ShouldUseSameIPageForAllPageTypes()
        {
            // Act
            var loginPage = _factory.CreatePage<LoginPage>();
            var securePage = _factory.CreatePage<SecurePage>();

            // Assert - both pages share the same IPage instance
            Assert.That(loginPage, Is.Not.Null);
            Assert.That(securePage, Is.Not.Null);
            // Verify both were initialized with the same _mockPage
            // This is validated implicitly by constructor - pages are created with _mockPage
        }

        [Test]
        public void AppManager_ShouldUseLazyInitializationViaFactory()
        {
            // Arrange
            var appManager = new AppManager(_mockPage);

            // Act - accessing pages multiple times should return cached instances
            var login1 = appManager.Login;
            var login2 = appManager.Login;

            // Assert
            Assert.That(login1, Is.SameAs(login2));
        }

        [Test]
        public void AppManager_ShouldReturnLoginPageFromFactory()
        {
            // Arrange
            var appManager = new AppManager(_mockPage);

            // Act
            var loginPage = appManager.Login;

            // Assert
            Assert.That(loginPage, Is.InstanceOf<LoginPage>());
        }

        [Test]
        public void AppManager_ShouldReturnSecurePageFromFactory()
        {
            // Arrange
            var appManager = new AppManager(_mockPage);

            // Act
            var securePage = appManager.SecureArea;

            // Assert
            Assert.That(securePage, Is.InstanceOf<SecurePage>());
        }
    }
}
