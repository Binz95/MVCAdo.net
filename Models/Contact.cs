using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCAdoNet.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string Location { get; set; }
    }
}