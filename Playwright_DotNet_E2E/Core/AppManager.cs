using Microsoft.Playwright;
using Playwright_DotNet_E2E.Pages;

namespace Playwright_DotNet_E2E.Core
{
    /// <remarks>
    /// Centralized page manager delegating page creation to PageFactory.
    /// Enables lazy initialization and caching of page instances per IPage session.
    /// </remarks>
    public class AppManager(IPage page)
    {
        private readonly IPageFactory _factory = new PageFactory(page);

        /// <remarks>
        /// Lazy-loaded LoginPage instance. Retrieved from factory cache on subsequent accesses.
        /// </remarks>
        public LoginPage Login => _factory.CreatePage<LoginPage>();

        /// <remarks>
        /// Lazy-loaded SecurePage instance. Retrieved from factory cache on subsequent accesses.
        /// </remarks>
        public SecurePage SecureArea => _factory.CreatePage<SecurePage>();
    }
}