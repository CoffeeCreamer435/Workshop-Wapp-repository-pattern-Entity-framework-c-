using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsideAirbnbCasus.Models
{
    public partial class Reviews // Principal entity
    {
        public Reviews()
        {
        }

        public Reviews(int id, DateTime date, string reviewerName, string comments)
        {
            Id = id;
            Date = date;
            ReviewerName = reviewerName;
            Comments = comments;
        }

        public int Id { get; set; } // Primary key
        public virtual Listings Listing { get; set; } // Reference navigation
        public int ListingId { get; set; } // Foreign key
        public string Comments { get; set; } // Scalar property

        #region overige properties
        public DateTime Date { get; set; }
        public int ReviewerId { get; set; }
        public string ReviewerName { get; set; }
        #endregion

    }
}
