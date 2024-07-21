using System.ComponentModel.DataAnnotations.Schema;

namespace SychoClinicApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string? Symptoms { get; set; }
        public string? Treatment { get; set; }

        [ForeignKey("categoriesArticle")]
        public int? CategoriesArticleId { get; set; }
        public virtual CategoriesArticle? categoriesArticle { get; set; }

        public virtual ICollection<MainArticle>? MainArticles { get; set; }
    }
}
