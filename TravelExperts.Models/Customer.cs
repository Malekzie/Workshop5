namespace TravelExperts.DataAccess.Models
{
    public partial class Customer : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(25)]
        public string CustFirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(25)]
        public string CustLastName { get; set; }

        [Required]
        [Display(Name = "Address")]
        [StringLength(75)]
        public string CustAddress { get; set; }

        [Required]
        [Display(Name = "City")]
        [StringLength(50)]
        public string CustCity { get; set; }

        [Required]
        [Display(Name = "Province")]
        [StringLength(2)]
        public string CustProv { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        [RegularExpression(@"^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$", ErrorMessage = "Invalid Postal Code format.")]
        [StringLength(7)]
        public string CustPostal { get; set; }

        [Display(Name = "Country")]
        [StringLength(25)]
        public string CustCountry { get; set; }

        [Display(Name = "Home Phone")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Phone number.")]
        [StringLength(20)]
        public string CustHomePhone { get; set; }

        [Display(Name = "Business Phone")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Phone number.")]
        [StringLength(20)]
        public string CustBusPhone { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string? CustEmail { get; set; }

        public int? AgentId { get; set; }

        // Override ASP.NET Identity's Email property to make it optional
        public override string Email { get; set; } = null;
    }
}
