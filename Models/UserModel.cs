using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace OnlineExamSystem.Models
{
    public class UserModel
    {
        [Required(ErrorMessage ="Please Enter First Name")]
        [MaxLength(50,ErrorMessage ="Enter Name Less than 50 Character")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter Last Name")]
        [MaxLength(50, ErrorMessage = "Enter Name Less than 50 Character")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        [EmailAddress(ErrorMessage ="Please Enter a Valid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Tha Password")]
        public string Password { get; set; }
        [ValidateNever]
        public string ReturnUrl { get; set; } 
    }
}
