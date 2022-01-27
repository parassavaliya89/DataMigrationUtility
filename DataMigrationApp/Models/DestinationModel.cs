using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMigrationApp.Models
{
    public class DestinationModel
    {
        
        public int Id { get; set; }

        
        public int Sum { get; set; }

        public int  SourceModelId { get; set; }
        public SourceModel Source { get; set; }
    }
}
