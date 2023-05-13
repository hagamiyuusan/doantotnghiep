using doan.DTO.Subscription;
using doan.Entities;

namespace doan.Interface
{
    public interface ISubscription
    {
        public Task<List<SubscriptionView>> getAllSubscription();
        public Task<SubscriptionView> getSubscriptionsById(int id);
        public Task<int> deleteSubscription(int id);
        public Task<int> editSubscription(int id);
        public Task<int> createSubscription(SubscriptionCreateRequest request);
        public Task<List<SubscriptionView>> getSubscriptionByUsername(string username);
    }
}
