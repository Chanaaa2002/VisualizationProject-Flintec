using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VIsualizationProject.Models
{
    [Table("OGQC")]
    public class OGQC
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "date")]
        [Required]
        public DateTime Detected_Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Product { get; set; }

        [Required]
        [StringLength(50)]
        public string Lot_Size { get; set; }

        [Required]
        [StringLength(50)]
        public string Defect_Qty { get; set; }

        [Required]
        [StringLength(50)]
        public string Production_Line { get; set; }
    }
}