using doan.DTO;
using doan.DTO.Subscription;
using doan.Entities;
using doan.Wrapper;

namespace doan.Interface
{
    public interface ISubscription
    {
        public Task<(List<SubscriptionView>, PaginationFilter, int)> getAllSubscription(PaginationFilter filter);
        public Task<SubscriptionView> getSubscriptionsById(int id);
        public Task<int> deleteSubscription(int id);
        public Task<int> editSubscription(int id);
        public Task<int> createSubscription(SubscriptionCreateRequest request);
        public Task<(List<SubscriptionView>, PaginationFilter, int)> getSubscriptionByUsername(string username, PaginationFilter filter);
    }
}
