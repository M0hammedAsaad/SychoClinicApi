using System.ComponentModel.DataAnnotations;

namespace SychoClinicApi.Models
{
    public class FeedBack
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Feedback { get; set; }

    }
}
