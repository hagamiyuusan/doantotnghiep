using doan.Entities;

namespace doan.Interface
{
    public interface IDuration
    {
        public Task<Duration> getAllDuration();
        public Task<List<Duration>> getDurationById(int id);
        public Task<bool> deleteDuration(int id);
        public Task<bool> editDurationt(int id);
        public Task<Duration> createDuration(Product product);
    }
}
