namespace ISPH.Domain.Interfaces.Core
{
    public interface IEntity<TId>
    {
        public TId Id { get; set; }
    }
}