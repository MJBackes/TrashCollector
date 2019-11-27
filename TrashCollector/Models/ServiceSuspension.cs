using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class ServiceSuspension
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}", ApplyFormatInEditMode = true)]
        public DateTime StartOfSuspension { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}", ApplyFormatInEditMode = true)]
        public DateTime EndOfSuspension { get; set; }
        public Customer Customer { get; set; }
    }
}