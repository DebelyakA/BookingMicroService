using System.ComponentModel.DataAnnotations.Schema;

namespace BookingHotelService.Entities
{
    public class RoomEntity
    {
        [Column("room_id")]
        public Guid Id { get; set; }

        [Column("room_class")]
        public required string RoomClass { get; set; }
        [Column("room_amount")]
        public required int RoomAmount { get; set; }
        [Column("room_price")]
        public required decimal RoomPrice { get; set; }
        [Column("room_description")]
        public required string RoomDescription { get; set; }
        [Column("room_personNumber")]
        public required string RoomPersonNumber { get; set; }

        [Column("hotel_id_fk")]
        public required Guid HotelId { get; set; }
        public HotelEntity HotelEntity { get; set; }

    }
}