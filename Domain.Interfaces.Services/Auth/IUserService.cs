using System.Threading.Tasks;
using ISPH.Domain.Interfaces.Core;

namespace ISPH.Domain.Interfaces.Services.Auth
{
    public interface IUserService<TUser, TId> where TUser : IUser<TId>
    {
         Task<TUser> Register(TUser user, string password);
         Task<TUser> Login(string email, string password);
         Task UpdatePassword(TUser entity, string password);
    }
}
