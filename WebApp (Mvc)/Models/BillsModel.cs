using System.ComponentModel.DataAnnotations;

namespace CofeeShop.Models
{
    public class BillsModel
    {
        public int? BillID { get; set; }

        [Required(ErrorMessage = "The Bill Number field is required.")]
        [StringLength(100, ErrorMessage = "The Bill Number must be less than 100 characters.")]
        public string BillNumber { get; set; }

        [Required(ErrorMessage = "The Bill Date field is required.")]
        [DataType(DataType.Date)]
        public DateTime BillDate { get; set; }

        public int OrderID { get; set; }

        [Required(ErrorMessage = "The Total Amount field is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "The Total Amount must be a positive value.")]
        public double TotalAmount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The Discount must be a positive value.")]
        public double? Discount { get; set; }

        [Required(ErrorMessage = "The Net Amount field is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "The Net Amount must be a positive value.")]
        public double NetAmount { get; set; }

        public int UserID { get; set; }
    }
}
