using Celta.Modelo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Celta.Persistencia
{
    public class ContextoCelta : DbContext
    {

        public ContextoCelta(DbContextOptions<ContextoCelta> options) : base(options)
        {

        }

        public DbSet<Student> Student { get; set; }

    }
}
