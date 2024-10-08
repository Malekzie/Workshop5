﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts.Models.ViewModel
{
    public class CredentialsVM
    {
        [EmailAddress]
        public string? Email { get; set; }

        public string? Username { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
