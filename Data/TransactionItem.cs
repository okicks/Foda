﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
    public class TransactionItem
    {
        [Key]
        public int TransactionItemId { get; set; }

        [Required]
        [ForeignKey(nameof(Transaction))]
        public int TransactionId { get; set; }

        [Required]
        public virtual Transaction Transaction { get; set; }

        [Required]
        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }

        [Required]
        public virtual Item Item { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public Guid OwnerId { get; set; }
    }
}
