using RST.Models.Tables;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RST.Models.DTOs.AddDTOs
{
    public class SoldApartmentsDTO
    {
        public Guid CustomerId { get; set; }
        public Guid ApartmentId { get; set; }
        public float SellingPrice { get; set; }
    }
}
