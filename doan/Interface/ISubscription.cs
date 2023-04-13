using doan.Entities;

namespace doan.Interface
{
    public interface ISubscription
    {
        public Task<Subscription> getAllSubscription();
        public Task<List<Subscription>> getSubscriptionsById(int id);
        public Task<bool> deleteSubscription(int id);
        public Task<bool> editSubscription(int id);
        public Task<Subscription> createSubscription(Product product);
    }
}
