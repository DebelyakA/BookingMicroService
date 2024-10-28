using BookingHotelService.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookingHotelService
{
    public class BookingContext : DbContext
    {
        public DbSet<RoomEntity> Room { get; set; }

        public DbSet<HotelEntity> Hotel { get; set; }

        public DbSet<BookingEntity> Booking { get; set; }
        public BookingContext(DbContextOptions<BookingContext> options)
             : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=HotelBooking;Username=postgres;Password=29032004");

        }
    }
}
