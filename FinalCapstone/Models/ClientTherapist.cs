using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalCapstone.Models
{
    public class ClientTherapist
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; }
        [ForeignKey("MassageTherapist")]
        public int TherapistId { get; set; }
        public MassageTherapist MassageTherapist { get; set; }
    }
}