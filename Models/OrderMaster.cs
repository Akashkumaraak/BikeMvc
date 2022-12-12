using BikeMvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;

namespace BikeMvc.Models
{
    public partial class OrderMaster
    {
        public OrderMaster()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        public int OrderMasterid { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Order Date")]
        public DateTime? Orderdate { get; set; }
        [Display(Name = "Total Amount")]
        public int? TotalAmount { get; set; }
        [Display(Name = "Card Number")]
        public int? CardNumber { get; set; }
        [Display(Name = "Amount Paid")]
        public int? AmountPaid { get; set; }
        [Display(Name = "User Id")]
        public int? Userid { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

