using doan.DTO.Subscription;
using doan.Entities;
using doan.Interface;
using Microsoft.AspNetCore.Mvc;

namespace doan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController
    {
        private readonly ISubscription _subscription;

        public SubscriptionController(ISubscription subscription)
        {
            _subscription = subscription;
        }
        [HttpPost]
        public async Task<ActionResult<Subscription>> createSubscription([FromBody] SubscriptionCreateRequest request )
        {
            var result = await _subscription.createSubscription(request);
            return result;
        }
    }
}
