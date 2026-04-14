using Microsoft.Playwright;
using Playwright_DotNet_E2E.Pages;

namespace Playwright_DotNet_E2E.Core
{
    /// <remarks>
    /// Abstraction for lazy page creation and caching.
    /// Ensures a single IPage instance is reused across all page types.
    /// </remarks>
    public interface IPageFactory
    {
        /// <remarks>
        /// Creates or retrieves a cached page instance of the specified type.
        /// Lazy initialization ensures the page is only created once per type per factory lifetime.
        /// </remarks>
        T CreatePage<T>() where T : BasePage;
    }
}
