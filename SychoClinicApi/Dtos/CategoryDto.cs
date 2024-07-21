namespace SychoClinicApi.Dtos
{
    public class CategoryDto
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string? Symptoms { get; set; }
        public string? Treatment { get; set; }
        public int? CategoriesArticleId { get; set; }
    }
}
