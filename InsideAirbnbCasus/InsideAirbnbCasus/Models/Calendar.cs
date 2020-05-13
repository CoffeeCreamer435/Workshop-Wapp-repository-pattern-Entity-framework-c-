using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InsideAirbnbCasus.Models
{
    public partial class Calendar
    {
        // [Key]
        public int ListingId { get; set; }
        // [Required]
        public DateTime Date { get; set; }
        public string Available { get; set; }
        public string Price { get; set; }
        // [MaxLength(10), MinLength(5)]
        // public string Locaton { get; set; }

        public virtual Listings Listing { get; set; }
    }
}
