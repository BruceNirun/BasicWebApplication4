using WebApplication4.Data;

namespace WebApplication4.Models
{
    public class Naja
    {
        public Animal? Animal { get; set; }
        public IEnumerable<Animal>? Animals { get; set; }
    }
}
