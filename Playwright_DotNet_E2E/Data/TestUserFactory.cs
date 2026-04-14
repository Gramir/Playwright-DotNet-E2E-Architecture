using static Playwright_DotNet_E2E.Data.Constants.LoginData;

namespace Playwright_DotNet_E2E.Data
{
    /// <remarks>
    /// Factory for immutable test-user payloads used across authentication scenarios.
    /// Centralizing this data avoids leaking raw credential literals into tests.
    /// </remarks>
    public static class TestUserFactory
    {
        public static TestUser CreateValidUser() => new(ValidUser, ValidPassword);

        public static TestUser CreateInvalidUsernameUser() => new(InvalidUser, ValidPassword);

        public static TestUser CreateInvalidPasswordUser() => new(ValidUser, InvalidPassword);
    }

    public sealed record TestUser(string Username, string Password);
}