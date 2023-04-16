using doan.DTO.API;
using doan.Entities;

namespace doan.Interface
{
    public interface IUseProductImageCaptioning
    {
        public Task<string> useProduct(UploadImageRequest input);
        public Task<bool> uploadFile(UploadImageRequest input);

    }
}
