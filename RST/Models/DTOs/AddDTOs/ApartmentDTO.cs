using RST.Models.Tables;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RST.Models.DTOs.AddDTOs
{
    public class ApartmentDTO
    {
        public string Location { get; set; }
        public float Area { get; set; }
        public IFormFile Image1 { get; set; }
        public IFormFile Image2 { get; set; }
        public IFormFile Image3 { get; set; }
        public IFormFile Image4 { get; set; }
        public Guid AgentId { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Number of bed rooms must be greater than or equal to 0")]
        public int BedRooms { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Number of rooms must be greater than or equal to 0")]
        public int TotalNumberOfRooms { get; set; }
        public float Price { get; set; }
    }
}
