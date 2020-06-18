using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PostSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostSystem.Data
{
    public class PostSystemDbContext : DbContext
    {
        public PostSystemDbContext() : base() { }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<MailItem> Mails { get; set; }
        public virtual DbSet<PostOffice> PostOffices { get; set; }
        public virtual DbSet<Delivery> Deliveries { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Server=DESKTOP-H5B78FN\TEW_SQLEXPRESS;" +
                              @"DataBase=PostSystem;" +
                              @"Integrated Security=true;");
                //.ConfigureWarnings(warn => warn.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning));

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.From_Delivery_Office)
                .WithMany(o => o.From_Deliveries)
                .HasForeignKey(d => d.From_Office_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.To_Delivery_Office)
                .WithMany(o => o.To_Deliveries)
                .HasForeignKey(d => d.To_Office_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.Delivery_Mail)
                .WithOne(m => m.Delivery)
                .HasForeignKey<Delivery>(d => d.Mail_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PostOffice>()
                .HasOne(o => o.Office_City)
                .WithMany(c => c.PostOffices)
                .HasForeignKey(o => o.City_Id)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
