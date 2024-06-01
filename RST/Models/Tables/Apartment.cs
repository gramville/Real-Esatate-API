using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RST.Models.Tables
{
    public class Apartment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string Location { get; set; }
        public float Area { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public int BedRooms { get; set; }
        public int TotalNumberOfRooms { get; set; }
        public Guid AgentId { get; set; }
        [ForeignKey("AgentId")]
        [JsonIgnore]
        public Agent? Agent { get; set; }
        public float Price { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
    }
}
