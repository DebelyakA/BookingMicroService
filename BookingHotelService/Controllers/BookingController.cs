using BookingHotelService.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookingHotelService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly BookingContext _context;
        public BookingController(BookingContext context)
        {
            _context = context;
        }
        private decimal CalculatePrice(DateTime start, DateTime end, decimal price)
        {
            var days = (end - start).Days;
            var totalPrice = days * price;
            return totalPrice;
        }

        [HttpPost]
        public async Task<IActionResult> DoBooking([FromBody]BookingEntity booking)
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            var date = DateTime.Now;
            var totalPrice = CalculatePrice(booking.BookingStartDate, booking.BookingEndDate, booking.BookingPrice);
            var newBooking = new BookingEntity
            {
                Id = Guid.NewGuid(),
                BookingDate = date,
                BookingPersonNumber = booking.BookingPersonNumber,
                BookingPrice = totalPrice,
                BookingDescription = booking.BookingDescription,
                BookingEndDate = booking.BookingEndDate,
                BookingStartDate = booking.BookingStartDate,
                BookingStatus = "Зарегистрировано",
                UserId = booking.UserId,
                RoomId = booking.RoomId,
            };
            await _context.Booking.AddAsync(newBooking);
            await _context.SaveChangesAsync();
            return Ok(newBooking);
        }
    }
}
