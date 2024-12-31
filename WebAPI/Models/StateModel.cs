using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebAPI.Models
{
    public class StateModel
    {
        public int? StateID { get; set; }
        [Required]
        [DisplayName("State Name")]
        public string StateName { get; set; }
        [DisplayName("State Code")]
        public string StateCode { get; set; }
        [Required]
        [DisplayName("Country Name")]
        public int CountryID { get; set; }
        [Required]
        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Modified Date")]
        public DateTime? ModifiedDate { get; set; }
    }
}
