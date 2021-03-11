using System.ComponentModel.DataAnnotations;

namespace EPE.UI.ViewModels.Admin
{
    public class CreateUserViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}