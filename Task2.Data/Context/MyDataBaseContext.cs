using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Task2.Core.Model;
using Task2.Data.Configurations;

namespace Task2.Data.Context
{
   public class MyDataBaseContext:DbContext
    {
        public MyDataBaseContext(DbContextOptions<MyDataBaseContext> options) : base(options)
        {

        }
        public DbSet<Text> Texts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TextConfiguration());
        }
    }
}
