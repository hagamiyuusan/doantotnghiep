namespace doan.Entities
{
    public class Duration
    {
        public int Id { set; get; }
        public int day { set; get; }
        public string name { set; get; }
        public ICollection<ProductDuration> productDurations { set; get; }

    }
}
