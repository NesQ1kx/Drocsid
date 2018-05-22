using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Entities;

namespace Dal
{
    public class SampleContext : DbContext
    {
        //public SampleContext() : base (@"Data Source = DEXP\SQLEXPRESS; Initial Catalog = DrocsidDB; Integrated Security = true")
        //{

        //}
        public DbSet<User> Users { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Topic> Topics { get; set; }

       
    }
}
