using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VIsualizationProject.Models
{
    [Table("Announcements")]
    public class Announcements
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "date")]
        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Announcement { get; set; }

        [Required]
        [StringLength(50)]
        public string Publisher { get; set; }

        [Required]
        [StringLength(50)]
        public string Publisher_Post { get; set; }
          
        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [Column(TypeName = "date")]
        [Required]
        public DateTime Start_Date { get; set; }

        [Column(TypeName = "date")]
        [Required]
        public DateTime End_Date { get; set; }

        [Required]
        [StringLength(50)]
        public string IsDisplayed { get; set; }


        //[Column(TypeName = "date")]
        //[Required]
        //public object StartDate { get; internal set; }
        //public bool IsDisplayed { get; set; }
    }
}