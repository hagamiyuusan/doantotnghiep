
namespace doan.Entities
{
    public class TypeProduct
    {
        public int Id { get; set; }
        public String name { set; get; }
        public ICollection<Product> products { get; set; }

    }
}
