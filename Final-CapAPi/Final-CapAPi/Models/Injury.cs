using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Final_CapAPi.Models
{
    public class Injury
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        [Display(Name ="Injury Location")]
        public string InjuryLocation { get; set; }
    }
}