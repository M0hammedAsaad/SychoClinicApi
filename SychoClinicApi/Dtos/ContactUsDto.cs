using System.ComponentModel.DataAnnotations;

namespace SychoClinicApi.Dtos
{
    public class ContactUsDto
    {
        [MaxLength(100)]
        [Required]
        public string FullName { get; set; }
        [MaxLength(150)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
