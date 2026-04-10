namespace OrigamiPlaywright.Data
{
    public static class Constants
    {
        /// <remarks>
        /// Constants are grouped by domain instead of a flat list so new areas can grow
        /// without turning this file into an unstructured global bag.
        /// </remarks>
        public static class LoginData
        {
            public const string ValidUser = "tomsmith";
            public const string ValidPassword = "SuperSecretPassword!";
            
            public const string InvalidUser = "invalidUser";
            public const string InvalidPassword = "wrongPassword";
            
            public const string ErrorMessageInvalidUser = "Your username is invalid!";
            public const string ErrorMessageInvalidPassword = "Your password is invalid!";
            public const string SuccessMessage = "You logged into a secure area!";
        }

        public static class Routes
        {
            public const string Login = "/login";
            public const string SecureArea = "/secure";
        }
    }
}