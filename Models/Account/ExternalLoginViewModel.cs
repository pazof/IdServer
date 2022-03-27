using System.ComponentModel.DataAnnotations;

namespace IdServer.Models
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}