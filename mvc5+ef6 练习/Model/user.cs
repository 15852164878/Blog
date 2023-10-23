using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class User
    {
        public int Uid { get; set; }
        public string Upwd { get; set; } 
        public string Uname { get; set; }
    }
}
