using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using Models.Interfaces;
using System;

namespace Infrastructure
{
  public class SondageDbcontext: DbContext
  {
    public SondageDbcontext(DbContextOptions<SondageDbcontext> options) : base(options)
    {
    }

    public virtual DbSet<User> USers { get; set; }
    public virtual DbSet<Vote> Qcms { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
    {
     // var user = new User() { Id = 1, Name = "aa", SurName = "vv" };
     // builder.Entity<User>().HasData(user);  
    }
  
  }
}
