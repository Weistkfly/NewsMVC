using Microsoft.EntityFrameworkCore;
using NewsMVC2019_2820.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsMVC2019_2820.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<News> News  { get; set;}
        
    }
}
