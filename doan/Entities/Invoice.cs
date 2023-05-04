namespace doan.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public string paypalId { set; get; }
        public int Total { set; get; }
        public bool isPaid { set; get; }
        public int productDurationId { set; get; }
        public ProductDuration productDuration { set; get; }
        public string username { set; get; }
        public AppUser appUser { set; get; }
    }
}
