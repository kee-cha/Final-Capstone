using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalCapstone.Models
{
    public class ClientPref
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Head")]
        public bool HeadPain { get; set; }
        [Display(Name ="Neck")]
        public bool NeckPain { get; set; }
        [Display(Name ="Upper Back")]
        public bool UpperBackPain { get; set; }
        [Display(Name ="Lower Back")]
        public bool LowBackPain { get; set; }
        [Display(Name ="Shoulder")]
        public bool ShoulderPain { get; set; }
        [Display(Name ="Arm")]
        public bool ArmPain { get; set; }
        [Display(Name = "Wrist/Hand")]
        public bool WristHandPain { get; set; }
        [Display(Name = "Hip")]
        public bool HipPain { get; set; }
        [Display(Name = "Thigh")]
        public bool ThighPain { get; set; }
        [Display(Name = "Knee/Leg")]
        public bool KneeLegPain { get; set; }
        [Display(Name = "Ankle/Foot")]
        public bool AnkleFootPain { get; set; }
        [Display(Name = "Gender Preference")]
        public string TherapistGender { get; set; }
        [Display(Name = "Specialty Preference")]
        public string TherapistSpecialty { get; set; }
        [Display(Name ="Time Frame")]
        public string TimeFramePref { get; set; }
        [Display(Name ="Appointment Time")]
        public string AppointmentTime { get; set; }
        [Display(Name ="Appointment Date")]
        public string AppointmentDate { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}