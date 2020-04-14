using System;
using Microsoft.EntityFrameworkCore;
using Ej.Domain;
namespace Ej.DA
{
    public class EjContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public EjContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}