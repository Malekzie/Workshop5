﻿using System;
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
                new TripType { TripTypeId = "B", Ttname = "Business" },
                new TripType { TripTypeId = "L", Ttname = "Leisure" },
                new TripType { TripTypeId = "G", Ttname = "Group" }
            };
        }

        public static Dictionary<string, string> GetProvinces()
        {
            return new Dictionary<string, string>
            {
                { "AB", "Alberta" },
                { "BC", "British Columbia" },
                { "MB", "Manitoba" },
                { "NB", "New Brunswick" },
                { "NL", "Newfoundland and Labrador" },
                { "NS", "Nova Scotia" },
                { "ON", "Ontario" },
                { "PE", "Prince Edward Island" },
                { "QC", "Quebec" },
                { "SK", "Saskatchewan" },
                { "NT", "Northwest Territories" },
                { "NU", "Nunavut" },
                { "YT", "Yukon" }
            };
        }
    }




}
