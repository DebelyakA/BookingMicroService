using System.ComponentModel.DataAnnotations.Schema;

namespace BookingHotelService.DtoModels
{
    public class HotelDto
    {
        public required string HotelName { get; set; }
        public required string HotelDescription { get; set; }
        public required string HotelLocation { get; set; }
    }
}
