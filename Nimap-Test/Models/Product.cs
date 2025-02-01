using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Nimap_Test.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product Name is required.")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }
        [JsonIgnore]
        public virtual Category Category { get; set; }
    }
}
