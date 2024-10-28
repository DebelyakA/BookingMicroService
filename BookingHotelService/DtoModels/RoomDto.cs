using System.ComponentModel.DataAnnotations.Schema;

namespace BookingHotelService.DtoModels
{
    public class RoomDto
    {
        public required string RoomClass { get; set; }
        public required int RoomAmount { get; set; }
        public required decimal RoomPrice { get; set; }
        public required string RoomDescription { get; set; }
        public required string RoomPersonNumber { get; set; }
        public required Guid HotelId { get; set; }
    }
}
