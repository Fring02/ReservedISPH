using System;

namespace ISPH.Domain.Models.Exceptions
{
    public class EntityPresentException : Exception
    {
        public EntityPresentException(string mes) : base(mes)
        {
            
        }
    }
}