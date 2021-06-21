namespace ISPH.Domain.Models.Exceptions
{
    public class UserFoundException : EntityPresentException
    {
        public UserFoundException(string mes) : base(mes)
        {
        }
    }
}