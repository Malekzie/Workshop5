using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts.DataAccess.Models
{
    public class TripType
    {

        [StringLength(1)]
        public string ID { get; set; }

        [StringLength(20)]
        public string TripTypeName { get; set; }
    }
}
    

