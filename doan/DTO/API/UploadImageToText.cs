namespace doan.DTO.API
{
    public class UploadImageToText
    {
        public int idProduct { set; get; }
        public string? token { set; get; }
        public IFormFile image { set; get; }
    }
}
