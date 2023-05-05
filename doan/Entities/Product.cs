namespace doan.Entities
{
    public class Product
    {
        public int Id { set; get; }
        public string? Name { set; get; }

        public DateTime Created { set; get; }
        public string API_URL { set; get; }

        public ICollection<ProductDuration> productDurations { set; get; }

        public int productTypeId { set; get; }
        public TypeProduct typeProduct { set; get; }
        public ICollection<Subscription> subscriptions { set; get; }


    }
}
