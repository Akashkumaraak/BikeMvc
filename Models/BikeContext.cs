using System;
using System.Collections.Generic;
using BikeMvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Demo.Models
{
    public partial class BikeContext : DbContext
    {
        public BikeContext()
        {
        }

        public BikeContext(DbContextOptions<BikeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admis { get; set; } = null!;
        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<OrderMaster> OrderMasters { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

    }
}
