using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShieldService.Models
{
    public class Character
    {
        public long id { get; set; }
        public string name { get; set; }
        public string thumbnail { get; set; }
        public string description { get; set; }

    }
}