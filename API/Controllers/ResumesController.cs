using System;
using System.IO;
using System.Threading.Tasks;
using ISPH.API.Responses;
using ISPH.Domain.Core.Models;
using ISPH.Domain.Interfaces.Services;
using ISPH.Domain.Models.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISPH.API.Controllers
{
    [Route("/api/v1/students/{studentId}/resume/")]
    [ApiController]
    public class ResumesController : ControllerBase
    {
        private readonly IResumesService _resumesService;
        private readonly string _folderPath;
        public ResumesController(IResumesService resumesService, IWebHostEnvironment environment)
        {
            _resumesService = resumesService;
            _folderPath = environment.WebRootPath + "/resumes/";
        }

        [HttpPost]
        public async Task<IActionResult> UploadResume(IFormFile file, Guid studentId)
        {
            if (file == null) return BadRequest("You didn't upload file");
            
            if (file.ContentType != "application/pdf") return BadRequest("Wrong format of resume");
            string path = _folderPath + file.FileName;
            var resume = new Resume() { Name = file.FileName, Path = path, StudentId = studentId };
            try
            {
                resume = await _resumesService.CreateAsync(resume);
                if (resume.Id == default) return this.ServerError("Failed to upload resume");
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return Ok("Uploaded resume");
            }
            catch (EntityPresentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return this.ServerError("Failed to upload resume");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DownloadResume(Guid studentId)
        {
            var resume = await _resumesService.GetByIdAsync(studentId);
            if (resume == null) return BadRequest("You didn't upload any resume");
            var path = resume.Path + resume.Name;
            using var memoryStream = new MemoryStream();
            using(var stream = new FileStream(path, FileMode.Open))
            {
              await stream.CopyToAsync(memoryStream);
            }
            memoryStream.Position = 0;
            return File(memoryStream, "application/pdf", Path.GetFileName(path));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteResumeAsync(Guid studentId)
        {
            var resume = await _resumesService.GetByIdAsync(studentId);
            if (resume == null) return BadRequest("This file doesn't exist");
            try
            {
                var path = resume.Path + resume.Name;
                await Task.Factory.StartNew(() => { System.IO.File.Delete(path); }).ContinueWith(
                    (task) => _resumesService.DeleteAsync(resume));
                return Ok("Deleted resume");
            }
            catch
            {
                return this.ServerError("Failed to delete resume");
            }
        }
    }
}
