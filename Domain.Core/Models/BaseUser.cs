using System.ComponentModel.DataAnnotations;
using ISPH.Domain.Interfaces.Core;

namespace ISPH.Domain.Core.Models
{
    public abstract class BaseUser<TId> : IUser<TId>
    {
        [Key]
        public TId Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public abstract string Role { get; }
        public byte[] HashedPassword { get; set; }
        public byte[] SaltPassword { get; set; }
        public string Email { get; set; }
    }
}