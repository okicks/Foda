using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Store
{
    public class StoreCreate
    {
        [Key]
        public int StoreId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string StoreName { get; set; }

        [Required]
        [Display(Name = "Street")]
        public string StoreStreet { get; set; }

        [Required]
        [Display(Name = "City")]
        public string StoreCity { get; set; }

        [Required]
        [Display(Name = "State")]
        public string StoreState { get; set; }

        [Required]
        [Display(Name = "Zip")]
        public string StoreZip { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string StorePhoneNumber { get; set; }

        [Required]
        public Guid OwnerId { get; set; }
    }
}
