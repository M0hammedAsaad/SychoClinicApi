namespace SychoClinicApi.Models
{
    public class CategoriesArticle
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string? Description { get; set; }
        public byte[]? image { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

    }
}
