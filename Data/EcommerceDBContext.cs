using System.Reflection.Emit;
using EcommerceApp.Models.Domin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace EcommerceApp.Data
{
    public class EcommerceDBContext : IdentityDbContext<ApplicationUser>
    {

        public EcommerceDBContext(DbContextOptions<EcommerceDBContext> options) : base(options)
        {

        }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> products { get; set; }
        public DbSet<Order> orders { get; set; }
          public DbSet<OrderItems> OrderItems { get; set; }
              public DbSet<ApplicationUser> applicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Configure the relationship between Order and ApplicationUser
            builder.Entity<Order>()
                .HasOne(o => o.ApplicationUser)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);





            var role = new List<IdentityRole>()
            {
               new IdentityRole{
                   Id = "6c7212ed-e450-4a5f-8524-f4bce692f507",
                Name="Admin",
                NormalizedName="Admin".ToUpper(),
                ConcurrencyStamp= "6c7212ed-e450-4a5f-8524-f4bce692f507"

             } ,

                new IdentityRole{
                   Id = "a02814aa-7ab6-4726-819c-e4d56a6d8b87",
                Name="User",
                NormalizedName="User".ToUpper(),
                ConcurrencyStamp= "a02814aa-7ab6-4726-819c-e4d56a6d8b87"

             } ,


            };

            builder.Entity<IdentityRole>().HasData(role);


            //    builder.Entity<Products>().HasData(new
            //        {
            //            Id=1,
            //            Name = "Classic Men's Watch",
            //            Brand = "TimeMaster",
            //            Price = 99.9,
            //            Description = "A classic and elegant men's watch with a leather strap and a stainless steel case.",
            //            Size ="50"  

            //    } ,

            //           new
            //           {
            //               Id = 2,
            //               Name = "Sporty Women's Watch",
            //               Brand = "ActiveWear",
            //               Price = 79.8,
            //               Description = "A sporty and stylish women's watch designed for active lifestyles.",
            //            Size = "90"

            //           },

            //            new
            //            {
            //                Id = 3,
            //                Name = "Luxury Chronograph Watch",
            //                Brand = "EleganceX",
            //                Price = 299.4,
            //                Description = "A luxury chronograph watch with a sapphire crystal and a genuine leather band." ,
            //            Size = "90"

            //            }
            //);
        }

    }
}
