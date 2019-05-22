using System;
using System.Collections.Generic;
using System.Text;

namespace ITISKeys.Models
{
    public class User : Identity
    {
        public string UId { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Sex { get; set; }

        public string Role { get; set; }
    }
}
