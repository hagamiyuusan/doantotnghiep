using doan.DTO.Duration;

namespace doan.DTO.Product
{
    public class ProductView
    {
        public int id { set; get; }
        public string Name { set; get; }
        public string API_URL { set; get; }
        public List<DurationView> durations { set; get; }
    }
}
