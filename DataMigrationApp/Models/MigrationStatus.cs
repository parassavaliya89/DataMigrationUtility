using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMigrationApp.Models
{
    public class MigrationStatus
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public int From { get; set; }

        [Required]
        [MaxLength(50)]
        public int To { get; set; }

        [Required]
        [MaxLength(100)]
        public EnumValuesForStatus Status { get; set; }


    }
}
