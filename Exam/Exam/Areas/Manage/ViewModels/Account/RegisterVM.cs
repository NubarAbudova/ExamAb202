

using System.ComponentModel.DataAnnotations;

namespace Exam.Areas.Manage.ViewModels.Account
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Name must be entered")]
        [MaxLength(50,ErrorMessage ="Name must be equal or less than 50 characters")]
        [MinLength(3,ErrorMessage ="Name must be equal or longer than 3 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname must be entered")]
        [MaxLength(50, ErrorMessage = "Surname must be equal or less than 50 characters")]
        [MinLength(3, ErrorMessage = "Surname must be equal or longer than 3 characters")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Username must be entered")]
        [MaxLength(100, ErrorMessage = "Username must be equal or less than 100 characters")]
        [MinLength(4, ErrorMessage = "Username must be equal or longer than 4 characters")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email must be entered")]
        [MaxLength(256)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password must be entered")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword must be entered")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
