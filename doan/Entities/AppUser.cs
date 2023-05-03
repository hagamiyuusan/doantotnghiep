using Microsoft.AspNetCore.Identity;

namespace doan.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public ICollection<Subscription> Subscriptions { get; set; }
        public ICollection<ImageToTextResult> imageToTexts { set; get; }
        public ICollection<Invoice> invoices { set; get; }
    }
}
