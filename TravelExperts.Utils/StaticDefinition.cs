namespace TravelExperts.Utils
{
    /// <summary>
    /// Defines static values for the application
    /// </summary>
    public static class StaticDefinition
    {
        public const string Customer = "Customer";
        public const string Admin = "Admin";

        public static readonly List<ProvinceVM> ProvinceList = new List<ProvinceVM>
        {
             new ProvinceVM { DisplayName = "Alberta", Value = "AB" },
        new ProvinceVM { DisplayName = "British Columbia", Value = "BC" },
        new ProvinceVM { DisplayName = "Manitoba", Value = "MB" },
        new ProvinceVM { DisplayName = "New Brunswick", Value = "NB" },
        new ProvinceVM { DisplayName = "Newfoundland and Labrador", Value = "NL" },
        new ProvinceVM { DisplayName = "Nova Scotia", Value = "NS" },
        new ProvinceVM { DisplayName = "Northwest Territories", Value = "NT" },
        new ProvinceVM { DisplayName = "Nunavut", Value = "NU" },
        new ProvinceVM { DisplayName = "Ontario", Value = "ON" },
        new ProvinceVM { DisplayName = "Prince Edward Island", Value = "PE" },
        new ProvinceVM { DisplayName = "Quebec", Value = "QC" },
        new ProvinceVM { DisplayName = "Saskatchewan", Value = "SK" },
        new ProvinceVM { DisplayName = "Yukon", Value = "YT" }

        };

    }




}
