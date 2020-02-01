using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalCapstone.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Appointment Date")]
        public string AppointmentDate { get; set; }
        [Display(Name ="Appointment Time")]
        public string AppointmentTime { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; }
        [ForeignKey("MassageTherapist")]
        public int TherapistId { get; set; }
        public MassageTherapist MassageTherapist { get; set; }
    }
}