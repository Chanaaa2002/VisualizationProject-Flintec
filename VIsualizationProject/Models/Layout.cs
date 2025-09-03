using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VIsualizationProject.Models
{
    [Table("Layout")]
    public class Layout
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
                                                
        [Column(TypeName = "date")]
        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Production_Line { get; set; }

        [Required]
        [StringLength(50)]
        public string Location { get; set; }

        public byte[] LayoutImageData { get; set; }
    }
}