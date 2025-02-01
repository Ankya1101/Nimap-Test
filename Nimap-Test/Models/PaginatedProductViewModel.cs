namespace Nimap_Test.Models
{
    public class PaginatedProductViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public int TotalProducts { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
