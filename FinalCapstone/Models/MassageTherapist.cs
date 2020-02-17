using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalCapstone.Models
{
    public class MassageTherapist
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name ="Zip Code")]
        public string Zip { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Gender { get; set; }
        public string Specialty { get; set; }
        public double Rating { get; set; }
        public double LikeCounter { get; set; }
        public double TotalCounter { get; set; }

        [Display(Name ="Time Frame")]
        public string TimeFramePref { get; set; }
        [Display(Name ="Sessions Per Day")]
        public int SessionPerDay{ get; set; }

        public string AppointmentDate { get; set; }
        [Display(Name ="First Availibility")]
        public string Schedule1 { get; set; }
        [Display(Name = "Second Availibility")]
        public string Schedule2 { get; set; }
        [Display(Name = "Third Availibility")]
        public string Schedule3 { get; set; }
        [Display(Name = "Forth Availibility")]
        public string Schedule4 { get; set; }
        public bool IsOpen1 { get; set; }
        public bool IsOpen2 { get; set; }
        public bool IsOpen3 { get; set; }
        public bool IsOpen4 { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}