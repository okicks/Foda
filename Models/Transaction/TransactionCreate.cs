﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Transaction
{
    public class TransactionCreate
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        [ForeignKey(nameof(Store))]
        public int StoreId { get; set; }

        [Required]
        public virtual Data.Store Store { get; set; }

        [Required]
        [Display(Name = "Date")]
        public DateTime TransactionDate { get; set; }

        [Required]
        [Display(Name = "Street")]
        public string DeliveryStreet { get; set; }

        [Required]
        [Display(Name = "City")]
        public string DeliveryCity { get; set; }

        [Required]
        [Display(Name = "State")]
        public string DeliveryState { get; set; }

        [Required]
        [Display(Name = "Zip")]
        public string DeliveryZip { get; set; }

        [Required]
        public Guid OwnerId { get; set; }
    }
}
