﻿namespace RST.Models.DTOs.UpdateDTOs
{
    public class UpdateAgentDTO
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
    }
}
