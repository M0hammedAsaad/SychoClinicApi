using System.ComponentModel.DataAnnotations;

namespace SychoClinicApi.Dtos
{
    public class RegisterUserDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public IFormFile? Picture { get; set; }

    }
}
