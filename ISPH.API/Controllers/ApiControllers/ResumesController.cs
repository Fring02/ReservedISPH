using System;
using System.IO;
using System.Threading.Tasks;
using ISPH.Core.Models;
using ISPH.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ISPH.Core.DTO;

namespace ISPH.API.Controllers.ApiControllers
{
    [Route("users/students/id={id}/resume/")]
    [ApiController]
    public class ResumesController : ControllerBase
    {
        private readonly IResumesRepository _repos;
        private readonly IWebHostEnvironment _env;
        public ResumesController(IResumesRepository repos, IWebHostEnvironment environment)
        {
            _repos = repos;
            _env = environment;
        }

        

        [HttpPost("add")]
        public async Task<IActionResult> AddResume(IFormFile file, Guid id)
        {
            if(file != null)
            {
                if (file.ContentType != "application/pdf") return BadRequest("Wrong format of resume");
                string path = "/Resumes/" + file.FileName;
                await using(var fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                var resume = new Resume() { Name = file.FileName, Path = path, StudentId = id };
                if (await _repos.HasEntity(resume)) return BadRequest("Your resume already exists");
                if (await _repos.Create(resume)) return LocalRedirect("/home/profile");
                return BadRequest("Failed to upload file");
            }

            return BadRequest("You didn't upload file");
        }

        [HttpGet]
        public async Task<ResumeDto> GetResumeByStudentId(Guid id)
        {
            var res = await _repos.GetById(id);
            if (res != null)
            {
                return new ResumeDto(res)
                {
                    StudentName = res.Student.FirstName,
                    StudentSurname = res.Student.LastName,
                    StudentEmail = res.Student.Email
                };
            }
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> DownloadResume(Guid id)
        {
            var resume = await _repos.GetById(id);
            var path = resume.Path;
            var memoryStream = new MemoryStream();
            await using(var stream = new FileStream(_env.WebRootPath + path, FileMode.Open))
            {
              await stream.CopyToAsync(memoryStream);
            }
            memoryStream.Position = 0;
            return File(memoryStream, "application/pdf", Path.GetFileName(path));
        }



        [HttpPost("delete")]
        public async Task<IActionResult> DeleteResumeAsync(Guid id)
        {
            var file = await _repos.GetById(id);
            if (file == null) return BadRequest("This file doesn't exist");
            string fullPath = Path.GetFullPath("static" + file.Path);
            System.IO.File.Delete(fullPath);
            if (await _repos.Delete(file)) return LocalRedirect("/home/profile");
            return BadRequest("Failed to delete resume");
        }
    }
}
