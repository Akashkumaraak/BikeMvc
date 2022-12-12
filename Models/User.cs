using BikeMvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeMvc.Models
{
    public partial class User
    {
        public User()
        {
            Carts = new HashSet<Cart>();
            OrderDetails = new HashSet<OrderDetail>();
            OrderMasters = new HashSet<OrderMaster>();
        }
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "*Enter a Username")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "*")]
        public int? MobileNo { get; set; }
        [Required(ErrorMessage = "Please enter the Email Id")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid Email ID")]
        public string? EmailId { get; set; }
        public string? Password { get; set; }
        [Compare("Password", ErrorMessage = "Password do not matched")]
        [NotMapped]
        [Display(Name = "ConfirmPassword")]
        public string? ConfirmPassword { get; set; }
        [Required(ErrorMessage = "*")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "*")]
        public string? Pincode { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<OrderMaster> OrderMasters { get; set; }
    }
}
