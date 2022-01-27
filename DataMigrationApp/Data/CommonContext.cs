using DataMigrationApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMigrationApp.Data
{
    public class CommonContext : DbContext
    {
        public DbSet<SourceModel> Source { get; set; }
        public DbSet<DestinationModel> Destination { get; set; }
        public DbSet<MigrationStatus> MigrationStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Paras Savaliya\\Documents\\Test.mdf\";Integrated Security=True;Connect Timeout=30");
        }
    }
}
