using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Adress { get; set; }
        [Display(Name ="Zip Code")]
        public int? ZipCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Pick Up Day")]
        public string PickUpDay { get; set; }
        [Display(Name = "Is Service In Suspension")]
        public bool? ServiceSuspended { get; set; }
        [Display(Name = "Beginning of Service")]
        public DateTime? ServiceStartTime { get; set; }
        [Display(Name = "End of Service")]
        public DateTime? ServiceEndTime { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}