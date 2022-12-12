using BikeMvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BikeMvc.Models
{
    public partial class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
            OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        public int ProductId { get; set; }
        [Display(Name = "Product Name")]
        public string? ProductName { get; set; }
        public string? Image { get; set; }
        public string? Brand { get; set; }
        public int? Price { get; set; }
        public int? Stock { get; set; }
        public int? CategoryId { get; set; }
        public string? Description { get; set; }
        public int? Id { get; set; }
        [Display(Name = "Vendor Id")]
        public virtual Category? IdNavigation { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

