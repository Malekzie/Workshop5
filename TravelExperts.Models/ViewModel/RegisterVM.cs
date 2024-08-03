using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelExperts.Models.ViewModel
{
    public class RegisterVM
    {
        public string ReturnUrl { get; set; }
        public InputModel Input { get; set; }
        public Dictionary<string, string> ProvinceList { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string CustFirstName { get; set; }

            [Required]
            public string CustLastName { get; set; }

            [Required]
            public string CustAddress { get; set; }

            [Required]
            public string CustCity { get; set; }

            [Required]
            public string CustProv { get; set; }

            [Required]
            public string CustPostal { get; set; }

            [Required]
            public string CustCountry { get; set; }

            [Required]
            public string CustHomePhone { get; set; }

            public string CustBusPhone { get; set; }
        }
    }
}
