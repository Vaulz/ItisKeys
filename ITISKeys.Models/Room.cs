using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ITISKeys.Models
{
    public class Room : Identity
    {
        public int RoomNumber { get; set; }

        public bool InStock { get; set; }

        public User Keeper { get; set; }
    }
}
