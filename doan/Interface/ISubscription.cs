using doan.DTO.Subscription;
using doan.Entities;

namespace doan.Interface
{
    public interface ISubscription
    {
        public Task<List<Subscription>> getSubscriptionsByUserId(Guid id);
        public Task<List<Subscription>> getAllSubscription();
        public Task<Subscription> getSubscriptionsById(int id);
        public Task<int> deleteSubscription(int id);
        public Task<int> editSubscription(int id);
        public Task<int> createSubscription(SubscriptionCreateRequest request);
    }
}
