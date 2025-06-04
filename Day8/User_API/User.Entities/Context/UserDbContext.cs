using Microsoft.EntityFrameworkCore;
using User.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Entities.Context
{
    public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
    {
        public DbSet<UserDetails> Users { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<UserDetails>().HasData(new UserDetails()
        //    {
        //        Id = 1,
        //        Name = "Pranav",
        //        Email = "pranav@gmail.com",
        //        Role = "admin",
        //        Password = "pranav@123",
        //        PhoneNumber = "9999999999",
        //        CreatedDate = new DateTime(2019, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        //    });

        //    base.OnModelCreating(builder);
        //}
    }
}
