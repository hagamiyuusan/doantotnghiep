﻿using Azure.Core;
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

        public async Task<int> createDuration(DurationCreateRequest duration)
        {
            var durationEntity = new Duration()
            {
                day = duration.day,
                name = duration.name
            };
            await _context.Durations.AddAsync(durationEntity);
            var result =  await _context.SaveChangesAsync();
            return result;
        }

        public async Task<int> deleteDuration(int id)
        {
            var duration = await _context.Durations.FindAsync(id);
            if (duration == null)
            {
                return 0;
            }
            _context.Durations.Remove(duration);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<int> editDuration(DurationEditRequest request)
        {
            var duration = await _context.Durations.FindAsync(request.Id);
            
            if (duration == null)
            {
                return 0;
            }
            duration.name = request.name;
            duration.day = request.day;
            var result = await _context.SaveChangesAsync();
            return result;

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
