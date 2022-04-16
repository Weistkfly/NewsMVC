using Microsoft.EntityFrameworkCore;
using NewsMVC2019_2820.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsMVC2019_2820.Models.ViewModels;

namespace NewsMVC2019_2820.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<News> News  { get; set;}
        public DbSet<Author> Authors  { get; set;}
        public DbSet<Category> Categories { get; set;}
        public DbSet<Countries> Countries { get; set;}
        public DbSet<User> Users { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(x => x.Roles).WithMany(x => x.Users);
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasData(new Rol { 
                    Id = 1,
                    Name = "Admin"
                }); 

                entity.HasData(new Rol { 
                    Id = 2,
                    Name = "Editor"
                });
            });


            base.OnModelCreating(modelBuilder);
        }


    }
}
