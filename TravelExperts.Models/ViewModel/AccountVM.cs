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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BusinessPhone { get; set; }
        public string HomePhone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
