using System;
using System.Collections.Generic;

//Written by Ben Wood
//Model for the Package table

namespace TravelExperts.DataAccess.Models;

public partial class Package
{
    public int PackageId { get; set; }

    public string PkgName { get; set; }

    public DateTime? PkgStartDate { get; set; }

    public DateTime? PkgEndDate { get; set; }

    public string PkgDesc { get; set; }

    public decimal PkgBasePrice { get; set; }

    public decimal? PkgAgencyCommission { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}