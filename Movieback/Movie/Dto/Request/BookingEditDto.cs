using System.ComponentModel.DataAnnotations;

namespace Movie.Dto.Request
{
    public class BookingEditDto
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Movie ID is required")]

        public long MovieId { get; set; }
        [Required(ErrorMessage = "Screen ID is required")]

        public long ScreenId { get; set; }
        public DateTime ShowDate { get; set; }
        public string ShowTime { get; set; }
        [Required(ErrorMessage = "UserId is required")]

        public long UserId { get; set; }
        public List<long> SeatIds { get; set; }
        public string PaymentId { get; set; }
        public string PaymentType { get; set; }
    }
}

