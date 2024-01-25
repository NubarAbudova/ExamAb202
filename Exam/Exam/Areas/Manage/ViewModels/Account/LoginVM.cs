
using System.ComponentModel.DataAnnotations;

namespace Exam.Areas.Manage.ViewModels.Account
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Please enter username or email")]
        
        public string UsernameOrEmail { get; set; }
        [Required(ErrorMessage = "Password must be entered")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRemembered { get; set; }
    }
}
