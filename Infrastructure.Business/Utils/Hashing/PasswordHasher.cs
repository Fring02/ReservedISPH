using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ISPH.Domain.Interfaces.Core;

namespace ISPH.Infrastructure.Services.Utils.Hashing
{
    public static class PasswordHasher<TUser, TId> where TUser : IUser<TId>
    {
        public static void CreateHashedPassword(string password, out byte[] hashedPassword, out byte[] saltPassword)
        {
            using var hmac = new HMACSHA512();
            saltPassword = hmac.Key;
            hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
        public static bool CheckHashedPassword(TUser user, string password)
        {
            using var hmac = new HMACSHA512(user.SaltPassword);
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(password)).SequenceEqual(user.HashedPassword);
        }

    }

    
}
