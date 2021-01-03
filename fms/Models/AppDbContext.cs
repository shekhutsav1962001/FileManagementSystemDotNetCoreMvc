using System;
using System.Collections.Generic;
using System.Text;
using fms.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace fms.Models
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
         : base(options)
        {

        }
        public DbSet<Uploadfile> files { get; set; }
        public DbSet<User> users { get; set; }

     

    }
}
