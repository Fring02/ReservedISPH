using ISPH.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ISPH.Core.DTO
{
    public class ResumeDto
    {
        public ResumeDto()
        {

        }
        public ResumeDto(Resume r)
        {
            ResumeId = r.ResumeId;
            Name = r.Name;
            StudentId = r.StudentId;
        }
        public Guid ResumeId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Guid StudentId { get; set; }
        public string StudentEmail { get; set; }
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
    }
}
