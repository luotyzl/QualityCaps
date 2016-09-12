using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QualityCaps.Models
{
    public class Color
    {
        [Key]
        [DisplayName("Color ID")]
        public int ID { get; set; }

        [DisplayName("Color")]
        public string Name { get; set; }

    }
}