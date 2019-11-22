using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class PickUp
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        [Display(Name = "Time of Pick Up Request")]
        public DateTime TimeOfRequest { get; set; }
        [Display(Name = "Time of Pick Up")]
        public DateTime? TimeOfPickUp { get; set; }
        [Display(Name = "Is Special Pick Up?")]
        public bool IsSpecial { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
    }
}