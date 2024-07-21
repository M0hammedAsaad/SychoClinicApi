using System.ComponentModel.DataAnnotations;

namespace SychoClinicApi.Dtos
{
    public class FeedBackDto
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        [Required]
        public string Feedback { get; set; }
    }
}
