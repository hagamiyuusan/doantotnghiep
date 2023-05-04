namespace doan.Entities
{
    public class ProductDuration
    {
        public int Id { set; get; }
        public int durationId { set; get; }
        public Duration duration { set; get; }

        public Product product { set; get; }
        public int productId { set; get; }

        public int price { set; get; }
        public ICollection<Subscription> subscriptions { set; get; }
        public ICollection<Invoice> invoices { set; get; }

    }
}
