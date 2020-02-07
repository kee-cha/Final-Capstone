using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalCapstone.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Subjective { get; set; }
        [Required]
        public string Objective { get; set; }
        [Required]
        public string Assessment { get; set; }
        [Required]
        public string Plan { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}