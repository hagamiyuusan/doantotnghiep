using doan.DTO.API;
using doan.Entities;

namespace doan.Interface
{
    public interface IUseProductImageCaptioning
    {
        public Task<string> useProduct(UploadImageRequest input, string API_URL);
        public Task<string> uploadFile(UploadImageRequest input);

    }
}
