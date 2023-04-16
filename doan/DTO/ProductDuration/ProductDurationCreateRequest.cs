namespace doan.DTO.ProductDuration
{
    public class ProductDurationCreateRequest
    {
        public int productId { set; get; }
        public int durationId { get; set; }
        public decimal price { set; get; }
    }
}
