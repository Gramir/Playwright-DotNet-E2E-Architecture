using System.Collections.Concurrent;
using Microsoft.Playwright;
using Playwright_DotNet_E2E.Pages;

namespace Playwright_DotNet_E2E.Core
{
    /// <remarks>
    /// Generic page factory with lazy initialization and caching.
    /// Creates page instances on-demand and caches them to avoid redundant instantiation.
    /// All pages share the same IPage for consistent state across the test.
    /// </remarks>
    public class PageFactory : IPageFactory
    {
        private readonly IPage _page;
        private readonly ConcurrentDictionary<Type, BasePage> _pageCache = new();

        public PageFactory(IPage page)
        {
            _page = page ?? throw new ArgumentNullException(nameof(page));
        }

        /// <remarks>
        /// Creates or retrieves a cached page instance of the specified type.
        /// Uses reflection to dynamically instantiate pages with the shared IPage.
        /// Caching ensures lazy evaluation and idempotency.
        /// </remarks>
        public T CreatePage<T>() where T : BasePage
        {
            var pageType = typeof(T);
            var cachedPage = _pageCache.GetOrAdd(pageType, _ =>
            {
                var instance = (BasePage)Activator.CreateInstance(typeof(T), _page)!;
                return instance;
            });

            return (T)cachedPage;
        }
    }
}
