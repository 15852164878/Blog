using mvc_ef.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace mvc_ef.DAL
{
    public class Duse:DbContext
    {
       public DbSet<Usermessges> a { get; set; }
    }
}