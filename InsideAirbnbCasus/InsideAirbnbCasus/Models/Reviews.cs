using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsideAirbnbCasus.Models
{
    public partial class Reviews
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

        public int Id { get; set; }
        public int ListingId { get; set; }
        public DateTime Date { get; set; }
        public int ReviewerId { get; set; }
        public string ReviewerName { get; set; }
        public string Comments { get; set; }

        public virtual Listings Listing { get; set; }
    }
}
