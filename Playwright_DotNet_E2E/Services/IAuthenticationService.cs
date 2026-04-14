using Playwright_DotNet_E2E.Data;

namespace Playwright_DotNet_E2E.Services
{
    /// <remarks>
    /// Service contract that provides authentication test data for E2E scenarios.
    /// It separates data sourcing from test orchestration.
    /// </remarks>
    public interface IAuthenticationService
    {
        TestUser GetValidUser();

        TestUser GetInvalidUsernameUser();

        TestUser GetInvalidPasswordUser();
    }
}