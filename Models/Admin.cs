using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace BikeMvc.Models
{
    public partial class Admin
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "*")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "*")]
        public string? Password { get; set; }
        [Compare("Password", ErrorMessage = "Password do not matched")]
        [NotMapped]
        [Display(Name = "ConfirmPassword")]
        public string? ConfirmPassword { get; set; }

    }
}

