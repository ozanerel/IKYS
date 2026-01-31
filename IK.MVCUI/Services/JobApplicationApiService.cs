using IK.ENTITIES.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace IK.MVCUI.Services
{
    public class JobApplicationApiService : IJobApplicationApiService
    {
        private readonly HttpClient _httpClient;
        public JobApplicationApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> SendApplicationAsync(JobApplication jobApplication, IFormFile cvFile)
        {
            using var content = new MultipartFormDataContent();

            // Text alanları
            content.Add(new StringContent(jobApplication.ApplicantName), "ApplicantName");
            content.Add(new StringContent(jobApplication.Email), "Email");
            content.Add(new StringContent(jobApplication.PhoneNumber), "PhoneNumber");
            content.Add(new StringContent(jobApplication.Address), "Address");
            content.Add(new StringContent(jobApplication.PositionId.ToString()), "PositionId");
            content.Add(new StringContent(jobApplication.PrivacyAccepted.ToString()), "PrivacyAccepted");

            // CV
            if (cvFile != null && cvFile.Length > 0)
            {
                var stream = cvFile.OpenReadStream();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(cvFile.ContentType);

                content.Add(fileContent, "CvFilePath", cvFile.FileName);
            }

            var response = await _httpClient.PostAsync("jobapplications", content);

            return response.IsSuccessStatusCode;
        }
    }
}
