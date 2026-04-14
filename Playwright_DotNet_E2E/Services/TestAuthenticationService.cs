using Playwright_DotNet_E2E.Data;

namespace Playwright_DotNet_E2E.Services
{
    /// <remarks>
    /// Default authentication service backed by TestUserFactory.
    /// Keeps credential strategies behind a replaceable abstraction.
    /// </remarks>
    public class TestAuthenticationService : IAuthenticationService
    {
        public TestUser GetValidUser() => TestUserFactory.CreateValidUser();

        public TestUser GetInvalidUsernameUser() => TestUserFactory.CreateInvalidUsernameUser();

        public TestUser GetInvalidPasswordUser() => TestUserFactory.CreateInvalidPasswordUser();
    }
}