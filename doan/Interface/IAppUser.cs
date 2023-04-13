using doan.DTO;
using doan.Entities;

namespace doan.Interface
{
    public interface IAppUser
    {
        public Task<AppUserGet> getUserbyID(string id);

        public Task<List<AppUserGet>> getAllUser();

        public Task<bool> updateUser(string id, AppUserChangeRequest request);
    }
}
