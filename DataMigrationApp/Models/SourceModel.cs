using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMigrationApp.Models
{
    public class SourceModel
    {
        public int Id { get; set; }

        public int FirstNumber { get; set; }

        
        public int SecondNumber { get; set; }
        public DestinationModel Destination { get; set; }
    }
}
