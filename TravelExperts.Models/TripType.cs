﻿using System;
using System.Collections.Generic;

namespace TravelExperts.DataAccess.Models;

public partial class TripType
{
    public string TripTypeId { get; set; }

    public string Ttname { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}