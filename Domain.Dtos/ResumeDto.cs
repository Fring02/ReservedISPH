﻿using System;
using ISPH.Domain.Core.Models;

namespace ISPH.Domain.Dtos
{
    public class ResumeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
