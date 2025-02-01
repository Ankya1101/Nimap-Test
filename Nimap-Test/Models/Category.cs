

using System.Text.Json.Serialization;

namespace Nimap_Test.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
