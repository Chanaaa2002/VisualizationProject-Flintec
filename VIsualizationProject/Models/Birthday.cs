using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VIsualizationProject.Models
{
    [Table("Birthday")]
    public class Birthday
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
        public string Employee_Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Position { get; set; }
    }
}