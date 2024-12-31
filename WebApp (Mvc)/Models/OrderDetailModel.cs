using System.ComponentModel.DataAnnotations;

namespace CofeeShop.Models
{
    public class OrderDetailModel
    {
        public int? OrderDetailID { get; set; }

        public int OrderID { get; set; }

        public int ProductID { get; set; }

        [Required(ErrorMessage = "The Quantity field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "The Amount field is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "The Amount must be a positive value.")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "The Total Amount field is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "The Total Amount must be a positive value.")]
        public double TotalAmount { get; set; }

        public int UserID { get; set; }
    }
}
