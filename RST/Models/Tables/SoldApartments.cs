using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RST.Models.Tables
{
    public class SoldApartments
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id {get; set;}
        public Guid CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        [JsonIgnore]
        public Customer? Customer { get; set; }
        public Guid ApartmentId { get; set; }
        [ForeignKey("ApartmentId")]
        [JsonIgnore]
        public Agent? Agent { get; set; }
        public float SellingPrice { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
    }
}
