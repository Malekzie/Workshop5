﻿using System;
using System.Collections.Generic;

namespace TravelExperts.DataAccess.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public DateTime? BookingDate { get; set; }

    public string? BookingNo { get; set; }

    public double? TravelerCount { get; set; }

    public int? CustomerId { get; set; }

    public string TripTypeId { get; set; }

    public int PackageId { get; set; }

    public virtual Package? Package { get; set; }

    public virtual TripType? TripType { get; set; }
}