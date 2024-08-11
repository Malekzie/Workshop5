using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelExperts.DataAccess.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public DateTime? BookingDate { get; set; }

    public string? BookingNo { get; set; }

    //Had a few bugs due to TravelerCount actually being a float in the DB. Pretty comical error in retrospect.
    public double? TravelerCount { get; set; }

    public int? CustomerId { get; set; }

    public string TripTypeId { get; set; }

    public int PackageId { get; set; }

    public virtual Package? Package { get; set; }

    public virtual TripType? TripType { get; set; }
}