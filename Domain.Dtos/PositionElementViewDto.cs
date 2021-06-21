using System;

namespace ISPH.Domain.Dtos
{
    public class PositionElementViewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string ImagePath { get; set; }
    }
}