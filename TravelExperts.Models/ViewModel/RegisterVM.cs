using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelExperts.Models.ViewModel
{
    public class RegisterVM
    {
        public RegisterVM()
        {
            ReturnUrl = string.Empty;
            Input = new InputModel();
            ProvinceList = new Dictionary<string, string>();
        }
        public string ReturnUrl { get; set; }
        public InputModel Input { get; set; }
        public Dictionary<string, string> ProvinceList { get; set; }

        public class InputModel
        {
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessage = "Username is required.")]
            public string Username { get; set; }

            [Required(ErrorMessage = "Password is required.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required(ErrorMessage = "Confirm Password is required")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "First name is required.")]
            public string CustFirstName { get; set; }

            [Required(ErrorMessage = "Last name is required.")]
            public string CustLastName { get; set; }

            [Required(ErrorMessage = "Address is required.")]
            public string CustAddress { get; set; }

            [Required(ErrorMessage = "City is required.")]
            public string CustCity { get; set; }

            [Required(ErrorMessage = "Province is required.")]
            public string CustProv { get; set; }

            [RegularExpression(@"^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$", ErrorMessage = "Invalid Postal Code")]
            [Required(ErrorMessage = "Postal code is required.")]
            public string CustPostal { get; set; }

            [Required(ErrorMessage = "Country is required.")]
            public string CustCountry { get; set; }

            [Required(ErrorMessage = "Home phone is required.")]
            public string CustHomePhone { get; set; }

            public string CustBusPhone { get; set; }
        }
    }
}
