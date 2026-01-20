using IK.ENTITIES.Models;
using Microsoft.AspNetCore.Http;

namespace IK.MVCUI.Services
{
    public interface IJobApplicationApiService
    {
        Task<bool> SendApplicationAsync(JobApplication jobApplication,IFormFile cvFile);
    }
}
