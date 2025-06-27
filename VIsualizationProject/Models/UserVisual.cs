using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VIsualizationProject.Models
{
    [Table("UserVisual")]
    public class UserVisual
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(50)]

        public string Password { get; set; }
        [Required]
        [StringLength(20)]
        public string Role { get; set; } 
    
    }
}