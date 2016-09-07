using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using QualityCaps.Models;

namespace QualityCaps.ViewModels
{
    public class ShoppingCartViewModel
    {
        [Key]
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}