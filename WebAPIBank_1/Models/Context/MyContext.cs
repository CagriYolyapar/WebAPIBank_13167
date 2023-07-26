using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebAPIBank_1.Models.Entities;
using WebAPIBank_1.Models.Init;

namespace WebAPIBank_1.Models.Context
{
    public class MyContext : DbContext
    {
        public MyContext() : base("MyConnection")
        {
            Database.SetInitializer(new MyInit());
        }

        public DbSet<CardInfo> CardInfoes { get; set; }
    }
}