namespace ISPH.Domain.Interfaces.Core
{
   public interface IUser<TId> : IEntity<TId>
    {
        public TId Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Role { get; set; }
        public byte[] HashedPassword { get; set; }
        public byte[] SaltPassword { get; set; }
        public string Email { get; set; }
    }
}