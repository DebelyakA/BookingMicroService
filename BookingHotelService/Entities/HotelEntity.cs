using System.ComponentModel.DataAnnotations.Schema;

namespace BookingHotelService.Entities
{
    public class HotelEntity
    {
        [Column("hotel_id")]
        public Guid Id { get; set; }
        [Column("hotel_name")]
        public required string HotelName { get; set; }
        [Column("hotel_description")]
        public required string HotelDescription { get; set; }
        [Column("hotel_location")]
        public required string HotelLocation { get; set; }

        public List<RoomEntity>? RoomList { get; set; }


    }
}