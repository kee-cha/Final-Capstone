﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Final_CapAPi.Models
{
    public class Treatment
    {
        [Key]
        public string Description { get; set; }
        [ForeignKey("Injury")]
        public int InjuryId { get; set; }
        public Injury Injury { get; set; }
    }
}