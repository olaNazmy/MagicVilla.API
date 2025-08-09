using MagicVilla.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Details = "A luxurious royal villa with ocean views.",
                    Rate = 200.0,
                    Occupancy = 4,
                    Sqft = 550,
                    ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa1.jpg",
                    Amenity = "Pool, Wi-Fi, Breakfast",
                    CreationDate = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedDate = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc)
                },
             new Villa
             {
                 Id = 2,
                 Name = "Premium Pool Villa",
                 Details = "Spacious villa with a private pool and beach access.",
                 Rate = 300.0,
                 Occupancy = 5,
                 Sqft = 600,
                 ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa2.jpg",
                 Amenity = "Pool, Beach, Wi-Fi",
                 CreationDate = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                 UpdatedDate = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc)
             }
             , new Villa
             {
                 Id = 3,
                 Name = "Luxury Beachfront Villa",
                 Details = "Exclusive beachfront villa with modern amenities.",
                 Rate = 400.0,
                 Occupancy = 6,
                 Sqft = 750,
                 ImageUrl = "https://dotnetmastery.com/bluevillaimages/villa3.jpg",
                 Amenity = "Beach, Pool, Wi-Fi, Breakfast",
                 CreationDate = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                 UpdatedDate = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc)
             }


             );
        }
    }
}
