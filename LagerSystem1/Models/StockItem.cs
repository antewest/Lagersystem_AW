using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LagerSystem1.Models
{
    public class StockItem
    {
        [Key]
        public int ItemId { get; set; }
        [Required]
        [MinLength(5)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Shelf { get; set; }
        [Required]
        [StringLength(1024)]
        public string Description { get; set; }

        public override string ToString()
        {
            return Name + "¤" + Price + "¤" + Shelf + "¤" + Description;
        }
    }
}