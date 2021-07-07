namespace ISPH.Domain.Core.Configuration
{
    public static class AuthOptions
    {
        public const string Issuer = "AuthServer"; 
        public const string Audience = "AuthClient";
        public const int Lifetime = 1;
    }
}
