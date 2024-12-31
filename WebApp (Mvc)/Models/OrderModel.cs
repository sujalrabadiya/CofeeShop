using System;
using System.ComponentModel.DataAnnotations;

namespace CofeeShop.Models
{
    public class OrderModel
    {
        public int? OrderID { get; set; }

        [Required(ErrorMessage = "The Order Date field is required.")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        public int CustomerID { get; set; }

        [StringLength(100, ErrorMessage = "The Payment Mode must be less than 100 characters.")]
        public string PaymentMode { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The Total Amount must be a positive value.")]
        public double? TotalAmount { get; set; }

        [Required(ErrorMessage = "The Shipping Address field is required.")]
        [StringLength(100, ErrorMessage = "The Shipping Address must be less than 100 characters.")]
        public string ShippingAddress { get; set; }

        public int UserID { get; set; }
    }

    public class OrderDropDownModel
    {
        public int OrderID { get; set; }
    }
}
