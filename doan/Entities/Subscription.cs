namespace doan.Entities
{
    public class Subscription
    {
        public int Id { set; get; }
        public int productId { set; get; }
        public Product product { set; get; }
        public DateTime dueDate { set; get; }
        public string username { set; get; }
        public AppUser AppUser { set; get; }

        public bool isActivate { set; get; }
        public string token { set; get; }

    }
}
