using System.ComponentModel.DataAnnotations.Schema;

namespace BookingHotelService.Entities
{
    public class BookingEntity
    {
        [Column("booking_id")]
        public Guid Id { get; set; }
        [Column("booking_date")]
        public required DateTime BookingDate { get; set; }
        [Column("booking_user_id_fk")]
        public required Guid UserId { get; set; }
        [Column("booking_room_id_fk")]
        public required Guid RoomId { get; set; }
        [Column("booking_personNumber")]
        public required int BookingPersonNumber { get; set; }
        [Column("booking_price")]
        public required decimal BookingPrice { get; set; }
        [Column("booking_description")]
        public string? BookingDescription { get; set; }
        [Column("booking_status")]
        public string? BookingStatus { get; set; }
        [Column("booking_dateStart")]
        public required DateTime BookingStartDate { get; set; }
        [Column("booking_dateEnd")]
        public required DateTime BookingEndDate { get; set; } 

    }
}