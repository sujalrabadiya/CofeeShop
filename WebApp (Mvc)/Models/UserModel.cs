using System.ComponentModel.DataAnnotations;

namespace CofeeShop.Models
{
    public class UserModel
    {
        public int? UserID { get; set; }

        [Required(ErrorMessage = "The User Name field is required.")]
        [StringLength(50, ErrorMessage = "The User Name must be less than 50 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password field is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "The Password must be at least 6 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The Mobile No field is required.")]
        [Phone(ErrorMessage = "Invalid Phone Number.")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Address can not be Empty.")]
        [StringLength(200, ErrorMessage = "The Address must be less than 200 characters.")]
        public string Address { get; set; }

        public bool IsActive { get; set; }
    }

    public class UserDropDownModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
    }

    public class UserLoginModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }

    public class UserRegisterModel
    {
        public int? UserID { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
    }
}