namespace WebApi.Dtos
{
    public class ProductDto
    {
        public string? title { get; set; }
        public string? description { get; set; }
        public int price { get; set; }
        public double discountPercentage { get; set; }
        public double rating { get; set; }
        public int stock { get; set; }
        public string? brand { get; set; }
        public string? category { get; set; }
    }
}
