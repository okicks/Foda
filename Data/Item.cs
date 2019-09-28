using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        [ForeignKey(nameof(Store))]
        public int StoreId { get; set; }

        [Required]
        public virtual Store Store { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string ItemName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Guid OwnerId { get; set; }
    }
}
