using doan.DTO.Invoice;

namespace doan.DTO.Subscription
{
    public class SubscriptionView
    {
        public int id { set; get; }
        public string productName { set; get; }
        public DateTime dueDate { set;get; }
        public string token { set; get; }
        public List<InvoiceView> invoiceViews { set; get; }
    }
}
