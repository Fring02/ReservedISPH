namespace ISPH.Domain.Core.Configuration
{
    public static class AuthOptions
    {
        public const string ISSUER = "AuthServer"; 
        public const string AUDIENCE = "AuthClient";
        public const int LIFETIME = 1;
    }
}
