using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TravelExperts.DataAccess.Models
{
    public partial class Customer : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        public string CustFirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string CustLastName { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string CustAddress { get; set; }

        [Required]
        [Display(Name = "City")]
        public string CustCity { get; set; }

        [Required]
        [Display(Name = "Province")]
        public string CustProv { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        [RegularExpression(@"^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$", ErrorMessage = "Invalid Postal Code format.")]
        public string CustPostal { get; set; }

        [Display(Name = "Country")]
        public string CustCountry { get; set; }

        [Display(Name = "Home Phone")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Phone number.")]
        public string CustHomePhone { get; set; }

        [Display(Name = "Business Phone")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Phone number.")]
        public string CustBusPhone { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string? CustEmail { get; set; }

        public int? AgentId { get; set; }

        public override string Email { get; set; } = null;
    }
}
