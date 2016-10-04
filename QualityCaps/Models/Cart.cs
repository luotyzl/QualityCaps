using System.ComponentModel.DataAnnotations;

namespace QualityCaps.Models
{
    public class Cart
    {
        [Key]
        public int ID { get; set; }
        public string CartId { get; set; }
        public int ItemId { get; set; }
        public string Color { get; set; }
        public int Count { get; set; }
        public System.DateTime DateCreated { get; set; }
        public virtual Item Item { get; set; }
    }
}