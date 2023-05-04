namespace doan.DTO.Product
{
    public class ProductCreateRequest
    {
        public int typeProductId {set;get;}
        public string API_URL {set;get;}
        public string Name { get; set; }
        public DateTime Created { get; set; }
    }
}
