using doan.Wrapper;

namespace doan.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);

    }
}
