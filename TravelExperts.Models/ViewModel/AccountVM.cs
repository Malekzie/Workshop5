using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts.Models.ViewModel
{
    public class AccountVM
    {
        [Required(ErrorMessage ="Enter your first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter your last name")]
        public string LastName { get; set; }

        public string BusinessPhone { get; set; }

        [Required(ErrorMessage = "Enter your Phone Number")]
        public string HomePhone { get; set; }

        [Required(ErrorMessage = "Enter a valid address")]
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
