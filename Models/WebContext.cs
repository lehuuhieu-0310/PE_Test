using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PE_Test.Models
{
    public class WebContext : DbContext
    {
        public WebContext(DbContextOptions<WebContext> options) : base(options)
        {

        }
        public DbSet<Department> Department { get; set; }
        public DbSet<Professor> Professor { get; set; }
    }
}
