using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Day
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool HasBeenPopulated { get; set; }
    }
}