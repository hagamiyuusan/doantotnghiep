namespace doan.Entities
{
    public class Product
    {
        public int Id { set; get; }
        public string? Name { set; get; }

        public DateTime Created { set; get; }

        public ICollection<ProductDuration> productDurations { set; get; }


    }
}
