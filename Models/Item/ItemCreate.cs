﻿using System.ComponentModel.DataAnnotations;

namespace Models.Item
{
    public class ItemCreate
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        public int StoreId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string ItemName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public float Price { get; set; }
    }
}
