using Microsoft.Playwright;

namespace Playwright_DotNet_E2E.Pages
{
    /// <summary>
    /// Fluent builder for expressive login flow with method chaining.
    /// Enables readable, chainable login operations following fluent interface pattern.
    /// </summary>
    public class LoginFluentChain(LoginPage loginPage)
    {
        private string? _username;
        private string? _password;
        private readonly LoginPage _loginPage = loginPage;

        /// <summary>
        /// Sets the username for the fluent login chain.
        /// </summary>
        /// <param name="username">The username to set.</param>
        /// <returns>This instance for method chaining.</returns>
        public LoginFluentChain WithUsername(string username)
        {
            _username = username;
            return this;
        }

        /// <summary>
        /// Sets the password for the fluent login chain.
        /// </summary>
        /// <param name="password">The password to set.</param>
        /// <returns>This instance for method chaining.</returns>
        public LoginFluentChain WithPassword(string password)
        {
            _password = password;
            return this;
        }

        /// <summary>
        /// Executes the login action with previously set credentials using fluent chain.
        /// </summary>
        /// <returns>A task that completes after the login action finishes.</returns>
        /// <remarks>
        /// The actual navigation (success to secure area or error message display)
        /// is handled by the login form itself. Callers should make appropriate assertions
        /// after awaiting this method.
        /// </remarks>
        public async Task ThenLoginAsync()
        {
            if (string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_password))
            {
                throw new InvalidOperationException("Username and password must be set before calling ThenLoginAsync.");
            }

            await _loginPage.LoginAsAsync(_username, _password);
        }
    }
}
