using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Item
{
    public class ItemListItem
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        [ForeignKey(nameof(Store))]
        public int StoreId { get; set; }

        [Required]
        public virtual Data.Store Store { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string ItemName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Guid OwnerId { get; set; }
    }
}
