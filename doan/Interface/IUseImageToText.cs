using doan.DTO.API;
using doan.Entities;

namespace doan.Interface
{
    public interface IuseImageToText
    {
        public Task<string> useProduct(UploadImageToText input);
        public Task<string> uploadFile(UploadImageToText input);

    }
}
