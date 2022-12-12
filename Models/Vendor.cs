using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BikeMvc.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        [Key]
        public int Id { get; set; }
        public string? Brand { get; set; }
        [Display(Name = "Supplier Name")]
        public string? SupplierName { get; set; }
        public string? Location { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
