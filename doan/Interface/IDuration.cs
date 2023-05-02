using doan.DTO.Duration;
using doan.Entities;

namespace doan.Interface
{
    public interface IDuration
    {
        public Task<List<Duration>> getAllDuration();
        public Task<Duration> getDurationById(int id);
        public Task<int> deleteDuration(int id);
        public Task<int> editDuration(DurationEditRequest request);
        public Task<int> createDuration(DurationCreateRequest duration);
    }
}
