using BookingHotelService.DtoModels;
using BookingHotelService.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BookingHotelService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly BookingContext _context;
        public HotelController(BookingContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody]HotelDto hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _context.Hotel.AnyAsync(h => h.HotelName == hotel.HotelName))
            {
                return BadRequest("Такой отель уже существует");
            }
            var id = Guid.NewGuid();
            var newHotel = new HotelEntity
            {
                Id = id,
                HotelDescription = hotel.HotelDescription,
                HotelLocation = hotel.HotelLocation,
                HotelName = hotel.HotelName,
                RoomList = new List<RoomEntity>()
            };
            
            await _context.Hotel.AddAsync(newHotel);
            await _context.SaveChangesAsync();
            
            return Ok("Отель создан");
        }

        [HttpPost]
        public async Task<IActionResult> AddRoom([FromBody]RoomDto room, string HotelName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Hotel = await _context.Hotel.FirstOrDefaultAsync(x => x.HotelName == HotelName);
            if (Hotel == null)
            {
                return NotFound("Отель не найден");
            }
            var HotelId = Hotel.Id;
            var newRoom = new RoomEntity
            {
                Id = Guid.NewGuid(),
                RoomAmount = room.RoomAmount,
                RoomClass = room.RoomClass,
                RoomDescription = room.RoomDescription,
                RoomPersonNumber = room.RoomPersonNumber,
                RoomPrice = room.RoomPrice,
                HotelId = HotelId
            };
            await _context.Room.AddAsync(newRoom);
            await _context.SaveChangesAsync();
            return Ok("Комната успешно добавлена");
            
        }
    }
}
