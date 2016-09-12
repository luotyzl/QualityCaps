using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QualityCaps.Models
{
    public class Supplier
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        [Key]
        [DisplayName("Catagorie ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "A Supplier Name is required")]
        [StringLength(160)]
        [DisplayName("Supplier Name")]
        public string Name { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public virtual ICollection<Item> Items { get; set; }

    }
}