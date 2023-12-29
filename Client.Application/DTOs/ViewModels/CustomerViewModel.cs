using Client.Domain.Enums;

namespace Client.Application.DTOs.ViewModels
{
    public class CustomerViewModel
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? DocumentType { get; set; }
        public string? DocumentNumber { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
    }
}
