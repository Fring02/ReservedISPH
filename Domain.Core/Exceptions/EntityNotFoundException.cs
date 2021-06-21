using System;

namespace ISPH.Domain.Models.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string mes) : base(mes)
        {
            
        }
    }
}