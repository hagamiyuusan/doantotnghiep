using doan.DTO.Duration;

namespace doan.DTO.Product
{
    public class ProductView
    {
        public int id { set; get; }
        public string Name { set; get; }
        public List<DurationView> durations { set; get; }
    }
}
