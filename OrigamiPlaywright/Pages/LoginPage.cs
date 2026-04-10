using Microsoft.Playwright;

namespace OrigamiPlaywright.Pages
{
    public class LoginPage(IPage page) : BasePage (page)
    {

    /// <remarks>
    /// Accessibility-first locators are preferred over CSS/XPath because they are
    /// more resilient to layout refactors and keep tests aligned with user-facing semantics.
    /// </remarks>
    public ILocator UsernameInput => _page.GetByLabel("username");
    public ILocator PasswordInput => _page.GetByLabel("password");
    public ILocator LoginButton => _page.GetByRole(AriaRole.Button, new() { Name = "Login" });


    /// <remarks>
    /// Encapsulates login as a single intent-level action to avoid duplicated step orchestration
    /// and reduce inconsistent intermediate states in calling tests.
    /// </remarks>
    public async Task LoginAsAsync(string username, string password)
    {
        await UsernameInput.FillAsync(username);
        await PasswordInput.FillAsync(password);
        await LoginButton.ClickAsync();
    }
}
}