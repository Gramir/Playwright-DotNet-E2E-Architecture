
using Microsoft.Playwright;

namespace OrigamiPlaywright.Pages
{
    public class SecurePage(IPage page) : BasePage(page)
{

    public ILocator LogoutButton => _page.GetByRole(AriaRole.Link, new() { Name = "Logout" });
}
}