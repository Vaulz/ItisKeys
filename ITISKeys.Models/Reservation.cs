using System;
using System.Collections.Generic;
using System.Text;

namespace ITISKeys.Models
{
    public class Reservation : Identity
    {
        public Room Room { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public User Reservator { get; set; }
    }
}
