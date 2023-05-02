using doan.DTO;
using doan.DTO.AppUser;
using doan.Entities;
using doan.Wrapper;

namespace doan.Interface
{
    public interface IAppUser
    {
        public Task<AppUserGet> getUserByName(string name);

        public Task<(List<AppUserGet>,PaginationFilter, int)> getAllUser(PaginationFilter filter);

        public Task<bool> updateUser(AppUserChangeRequest request);

        public Task<IList<string>> getUserRole(string id);
    }
}
