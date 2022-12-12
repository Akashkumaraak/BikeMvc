using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;

namespace BikeMvc.Models
{
    public partial class Cart
    {
        [Key]
        public int Id { get; set; }
        public int? Cartid { get; set; }
        public int? Quantity { get; set; }
        [Display(Name = "Total Amount")]
        public int? TotalAmount { get; set; }
        public int? Userid { get; set; }
        public int? Productid { get; set; }

        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
    }
}
