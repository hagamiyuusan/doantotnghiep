using Azure.Core;
using doan.DTO.Duration;
using doan.EF;
using doan.Entities;
using doan.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace doan.Repository
{
    public class DurationRepository : IDuration
    {

        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;

        public DurationRepository(IConfiguration config, ApplicationDbContext context)
        {
            _config = config;
            _context = context;
        }

        public async Task<Duration> createDuration(DurationCreateRequest duration)
        {
            var durationEntity = new Duration()
            {
                day = duration.day,
                name = duration.name
            };
            var result = await _context.Durations.AddAsync(durationEntity);
            return durationEntity;
        }

        public async Task<bool> deleteDuration(int id)
        {
            var duration = await _context.Durations.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.Durations.Remove(duration);
            var result = await _context.SaveChangesAsync();
            return (result == 1 ? true : false);
        }

        public async Task<bool> editDuration(int id, DurationEditRequest request)
        {
            var duration = await _context.Durations.FindAsync(id);
            
            if (duration == null)
            {
                return false;
            }
            duration.name = request.name;
            duration.day = request.day;
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<List<Duration>> getAllDuration()
        {
            return await _context.Durations.ToListAsync();
        }

        public async Task<Duration> getDurationById(int id)
        {
            return await _context.Durations.Where(x => x.Id == id).FirstOrDefaultAsync();
        }


    }
}
