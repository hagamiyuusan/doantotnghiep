using doan.DTO.API;
using doan.EF;
using doan.Entities;
using doan.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace doan.Repository
{
    public class UseImageToTextRepository : IuseImageToText
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UseImageToTextRepository(HttpClient httpClient, ApplicationDbContext context, IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            _httpClient = httpClient;
            _context = context;
            _config = config;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> uploadFile(UploadImageToText input)
        {
            string uniqueFileName = UploadedFile(input);
            ImageToTextResult file = new ImageToTextResult
            {
                caption = "",
                path = uniqueFileName,
            };
            await _context.ImageForCaptionings.AddAsync(file);
            await _context.SaveChangesAsync();
            return uniqueFileName;

        }
        private string UploadedFile(UploadImageToText model)
        {
            string uniqueFileName = null;

            if (model.image != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        public async Task<string> useProduct(UploadImageToText input)
        {
            var API_URL = await _context.Products.Where(x => x.Id == input.idProduct).FirstAsync();

            using (var content = new MultipartFormDataContent())
            {
                var fileContent = new StreamContent(input.image.OpenReadStream());
                content.Add(fileContent, "file", input.image.FileName);
                var response = await _httpClient.PostAsync(API_URL.API_URL, content);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }


            //var tokenString = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0eXBlIjoiMSIsIm5iZiI6MTY4MTU0MjM5MywiZXhwIjoxNjg0MTM0MzkzLCJpYXQiOjE2ODE1NDIzOTN9.HtPIhk_eLlllnE_vgGcB4r8s4abLPlDvpOqZW33wnNs";
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
            //var response = await _httpClient.GetAsync("http://127.0.0.1:8000/books");
            //response.EnsureSuccessStatusCode(); 
            //var content = await response.Content.ReadAsStringAsync();
            //return content;
        }
    }
}
