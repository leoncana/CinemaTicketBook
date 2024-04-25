using Movie.Models;

namespace Movie.Dto.Request
{
    public class BookingCreateDto
    {
        public long MovieId { get; set; }
        public long ScreenId { get; set; }
        public DateTime ShowDate { get; set; }
        public string ShowTime { get; set; }
        public long UserId { get; set; }
        public List<long> SeatIds { get; set; }
        public string PaymentId { get; set; }
        public string PaymentType { get; set; }
    }
}
