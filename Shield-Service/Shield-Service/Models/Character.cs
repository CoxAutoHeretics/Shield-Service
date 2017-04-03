using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shield_Service.Models
{
    public class Character
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}