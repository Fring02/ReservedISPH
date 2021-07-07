namespace ISPH.Domain.Interfaces.Core
{
   public interface IUser<TId> : IEntity<TId>
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Role { get; }
        public byte[] HashedPassword { get; set; }
        public byte[] SaltPassword { get; set; }
        public string Email { get; set; }
    }
}