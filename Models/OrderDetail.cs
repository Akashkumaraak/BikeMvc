using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;

namespace BikeMvc.Models
{
    public partial class OrderDetail
    {
        [Key]
        public int Orderid { get; set; }
        [Display(Name = "Total Amount")]
        public int? TotalAmount { get; set; }
        public int? OrderMasterid { get; set; }
        public int? Productid { get; set; }
        public int? Userid { get; set; }

        public virtual OrderMaster? OrderMaster { get; set; }
        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
    }
}

