using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalCapstone.Models
{
    public class MTAppointViewModel
    {
        public SelectList SetTime { get; set; }
        public SelectList DayPref { get; set; }
        public MassageTherapist MassageTherapist { get; set; }
        public List<MassageTherapist> MtList { get; set; }
        public Client Client { get; set; }
        public ClientPref ClientPref { get; set; }

        public string time;

    }
}