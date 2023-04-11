using doan.Entities;

namespace doan.Interface
{
    public interface IAppUser
    {
        public Task<AppUser> getUserbyID(int id);

        public Task<List<AppUser>> getAllUser(int id);

    }
}
