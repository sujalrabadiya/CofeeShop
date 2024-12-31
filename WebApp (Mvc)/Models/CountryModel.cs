using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CofeeShop.Models
{
    public class CountryModel
    {
        public int? CountryID { get; set; }
        [Required]
        [DisplayName("Country Name")]
        public string CountryName { get; set; }
        [Required]
        [DisplayName("Country Code")]
        public string CountryCode { get; set; }
        [Required]
        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Modified Date")]
        public DateTime? ModifiedDate { get; set; }
    }
}
