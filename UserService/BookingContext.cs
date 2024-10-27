using Microsoft.EntityFrameworkCore;

namespace UserService
{
    public class BookingContext : DbContext
    {
        public DbSet<UserEntity> User { get; set; }
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
