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
        public async Task<IActionResult> AddRoom([FromBody]RoomDto room, string hotelName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //
            //Изменить поиск отеля, желательно будет создать отдельный метод для этого!
            //
            var hotel = await _context.Hotel.FirstOrDefaultAsync(x => x.HotelName == hotelName);
            if (hotel == null)
            {
                return NotFound("Отель не найден");
            }
            var HotelId = hotel.Id;
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

        public async Task<IActionResult>FindHotel(string nameOfHotel, string? location)
        {
            if(nameOfHotel == null)
            {
                return NotFound("Введите название отеля");
            }
            if (location != null)
            {
                var hotels = await _context.Hotel
                .AsNoTracking()
                .Where(n => n.HotelName.StartsWith(nameOfHotel, StringComparison.OrdinalIgnoreCase)
                && n.HotelLocation.StartsWith(location,StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
                return Ok(hotels);
            }
            else
            {
                var hotels = await _context.Hotel
                                .AsNoTracking()
                                .Where(n => n.HotelName.StartsWith(nameOfHotel, StringComparison.OrdinalIgnoreCase))
                                .ToListAsync();
                return Ok(hotels);
            }
            
        }

        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _context.Hotel
                .AsNoTracking()
                .ToListAsync();
            return Ok(hotels);
        }

        public async Task<IActionResult> GetHotelWithRooms(Guid hotelId)
        {
            var hotel = await _context.Hotel
                .AsNoTracking()
                .Include(r => r.RoomList)
                .Where(i => i.Id == hotelId)
                .FirstOrDefaultAsync();
            return Ok(hotel);
                
                
        }
    }
}
