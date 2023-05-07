using doan.DTO;
using doan.DTO.UseImage;
using doan.Wrapper;

namespace doan.Interface
{
    public interface IUseImage
    {
        public Task<(List<DetailImageVM>, PaginationFilter, int)> getAllUser(PaginationFilter filter);
    }
}
