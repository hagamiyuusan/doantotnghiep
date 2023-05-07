using doan.DTO.UseImage;
using doan.EF;
using doan.Interface;
using doan.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace doan.Repository
{
    public class UseImageRepository : IUseImage
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;

        public UseImageRepository(IConfiguration config, ApplicationDbContext context)
        {
            _config = config;
            _context = context;
        }

        public async Task<(List<DetailImageVM>, PaginationFilter, int)> getAllUser(PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter.key);
            if (!String.IsNullOrEmpty(filter.key))
            {
                var resultWithFilter = await (from imageWithCap in _context.ImageForCaptionings.Where(x => x.caption.Contains(filter.key))
                              .Skip((filter.PageNumber - 1) * validFilter.PageSize)
                              .Take(validFilter.PageSize)
                              select new DetailImageVM
                             {
                                 caption = imageWithCap.caption,
                                 path = imageWithCap.path
                             }).ToListAsync();
                var countWithFilter = await _context.ImageForCaptionings.Where(x => x.caption.Contains(filter.key)).CountAsync();
                return (resultWithFilter, validFilter , countWithFilter);
            }
            var result = await (from imageWithCap in _context.ImageForCaptionings
              .Skip((filter.PageNumber - 1) * validFilter.PageSize)
              .Take(validFilter.PageSize)
                                select new DetailImageVM
                                {
                                    caption = imageWithCap.caption,
                                    path = imageWithCap.path
                                }).ToListAsync();
            var count = await _context.ImageForCaptionings.CountAsync();
            return (result, validFilter, count);

        }
    }
}
