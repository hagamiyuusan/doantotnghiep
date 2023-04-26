using doan.DTO.Subscription;
using doan.Entities;

namespace doan.Interface
{
    public interface ISubscription
    {
        public Task<List<Subscription>> getSubscriptionsByUserId(Guid id);
        public Task<List<Subscription>> getAllSubscription();
        public Task<Subscription> getSubscriptionsById(int id);
        public Task<bool> deleteSubscription(int id);
        public Task<bool> editSubscription(int id);
        public Task<Subscription> createSubscription(SubscriptionCreateRequest request);

    }
}
