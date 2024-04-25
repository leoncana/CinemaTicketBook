using Microsoft.Extensions.ObjectPool;
using System.ComponentModel.DataAnnotations;

namespace test1.Models
{
    public class Movies
    {
        [Key]
        public int _id { get; set; }
        public required string title { get; set; }
        public required string description { get; set; }
        public string? imgUrl { get; set; }
        public required int duration { get; set; }
        public string? genre { get; set; }
        public double rating { get; set; }
        public String[] cast { get; set; }
        public String[] crew { get; set; }
    }
}
