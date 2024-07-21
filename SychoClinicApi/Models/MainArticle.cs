using System.ComponentModel.DataAnnotations.Schema;

namespace SychoClinicApi.Models
{
    public class MainArticle
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public byte[] Document { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public byte[]? Image { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
