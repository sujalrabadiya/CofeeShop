using System.ComponentModel.DataAnnotations;

namespace CofeeShop.Models
{
    public class ProductModel
    {
        public int? ProductID { get; set; }

        [Required(ErrorMessage = "The Product Name field is required.")]
        [StringLength(100, ErrorMessage = "The Product Name must be less than 100 characters.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "The Product Price field is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "The Product Price must be a positive value.")]
        public double ProductPrice { get; set; }

        [Required(ErrorMessage = "The Product Code field is required.")]
        [StringLength(100, ErrorMessage = "The Product Code must be less than 100 characters.")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        [StringLength(100, ErrorMessage = "The Description must be less than 100 characters.")]
        public string Description { get; set; }

        public int UserID { get; set; }
    }

    public class ProductDropDownModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
    }
}
