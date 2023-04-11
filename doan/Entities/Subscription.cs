namespace doan.Entities
{
    public class Subscription
    {
        public int Id { set; get; }
        public int productDurationId { set; get; }
        public ProductDuration productDuration { set; get; }
        public DateTime createDate { set; get; }
        public Guid UserId { set; get; }
        public AppUser AppUser { set; get; }

    }
}
