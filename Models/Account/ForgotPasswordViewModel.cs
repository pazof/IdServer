using System.ComponentModel.DataAnnotations;

namespace IdServer.Models
{
    public class ForgotPasswordViewModel
    {
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}