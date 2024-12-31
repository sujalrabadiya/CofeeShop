using System.ComponentModel.DataAnnotations;

namespace CofeeShop.Models
{
    public class CustomerModel
    {
        public int? CustomerID { get; set; }

        [Required(ErrorMessage = "The Customer Name field is required.")]
        [StringLength(100, ErrorMessage = "The Customer Name must be less than 100 characters.")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "The Home Address field is required.")]
        [StringLength(100, ErrorMessage = "The Home Address must be less than 100 characters.")]
        public string HomeAddress { get; set; }

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Mobile No field is required.")]
        [Phone(ErrorMessage = "Invalid Phone Number.")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "The GST No field is required.")]
        [StringLength(15, ErrorMessage = "The GST No must be less than 15 characters.")]
        public string GST_NO { get; set; }

        [Required(ErrorMessage = "The City Name field is required.")]
        [StringLength(100, ErrorMessage = "The City Name must be less than 100 characters.")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "The Pin Code field is required.")]
        [StringLength(15, ErrorMessage = "The Pin Code must be less than 15 characters.")]
        public string PinCode { get; set; }

        [Required(ErrorMessage = "The Net Amount field is required.")]
        public double NetAmount { get; set; }

        public int UserID { get; set; }
    }

    public class CustomerDropDownModel
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
    }
}
