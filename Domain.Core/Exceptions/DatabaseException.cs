using System;

namespace ISPH.Domain.Models.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string mes) : base(mes)
        {
            
        }
    }
}