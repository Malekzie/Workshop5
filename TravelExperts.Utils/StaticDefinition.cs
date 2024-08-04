using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperts.DataAccess.Models;

namespace TravelExperts.Utils
{
    /// <summary>
    /// Defines static values for the application
    /// </summary>
    public static class StaticDefinition
    {
        public const string Customer = "Customer";
        public const string Admin = "Admin";

        public static IEnumerable<TripType> GetTripTypes()
        {
            return new List<TripType>
        {
            new TripType { ID = "B", TripTypeName = "Business" },
            new TripType { ID = "L", TripTypeName = "Leisure" },
            new TripType { ID = "G", TripTypeName = "Group" }
        };
        }

    }
}
