using Client.Domain.Enums;

namespace Client.Application.DTOs.InputModels
{
    public class AddCustomerInputModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public EDocumentType DocumentType { get; set; }
        public string? DocumentNumber { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
    }
}