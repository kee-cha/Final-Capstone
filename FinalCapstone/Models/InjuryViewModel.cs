using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalCapstone.Models
{
    public class InjuryViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Cause { get; set; }
        public string Treatment { get; set; }

        [Display(Name = "Injury Location")]
        public string InjuryLocation { get; set; }
    }
}