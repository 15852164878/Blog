using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_ef.Models
{
    public class Usermessges
    {
        public string name { get; set; }
        public string pwd { get; set; }
        public virtual ICollection<Usermessges> a { get; set; }
       
    }
}