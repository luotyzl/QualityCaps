using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QualityCaps.Models
{
    public class Catagorie
    {
        [Key]
        [DisplayName("Catagorie ID")]
        public int ID { get; set; }

        [DisplayName("Catagorie")]
        public string Name { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}