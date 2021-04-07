using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Models;

namespace Utilities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
             : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();

        }
        /// <summary>
        /// populate database with table please use DB NAME = ERNI
        /// </summary>
        public DbSet<User> users { get; set; }
        public DbSet<Transaction> transactions { get; set; }
    }
}
