using System.ComponentModel.DataAnnotations;

namespace IdServer.Models
{
    public class LoginWithRecoveryCodeViewModel
    {
        [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Recovery Code")]
            public string RecoveryCode { get; set; }
    }
}