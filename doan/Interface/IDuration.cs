using doan.DTO.Duration;
using doan.Entities;

namespace doan.Interface
{
    public interface IDuration
    {
        public Task<List<Duration>> getAllDuration();
        public Task<Duration> getDurationById(int id);
        public Task<bool> deleteDuration(int id);
        public Task<bool> editDuration(DurationEditRequest request);
        public Task<Duration> createDuration(DurationCreateRequest duration);
    }
}
