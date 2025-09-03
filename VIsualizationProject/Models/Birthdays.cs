using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VIsualizationProject.Models
{
    [Table("Birthdays")]
    public class Birthdays
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "date")]
        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Employee_Number { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Position { get; set; }
        [Required]
        [StringLength(50)]
        public string Production_Line { get; set; }
        [Required]
        [StringLength(50)]
        public string Location { get; set; }
        public byte[] Photo { get; set; }
    }
}