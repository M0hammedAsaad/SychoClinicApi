using System.ComponentModel.DataAnnotations;

namespace SychoClinicApi.Models
{
    public class ContectUs
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string FullName { get; set; }
        [MaxLength(150)]
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
