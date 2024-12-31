using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CofeeShop.Models
{
    public class CityModel
    {
        public int? CityID { get; set; }
        [Required]
        [DisplayName("State Id")]
        public int StateID { get; set; }
        [Required]
        [DisplayName("Country Id")]
        public int CountryID { get; set; }
        [Required]
        [DisplayName("City Name")]
        public string CityName { get; set; }
        [Required]
        [DisplayName("City Code")]
        public string CityCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public class CountryDropDownModel
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
    }

    public class StateDropDownModel
    {
        public int StateID { get; set; }
        public string StateName { get; set; }
    }
}
