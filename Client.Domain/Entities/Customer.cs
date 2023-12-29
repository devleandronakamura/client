using Client.Domain.Enums;

namespace Client.Domain.Entities
{
    public class Customer : EntityBase
    {
        public Customer(string? firstName, string? lastName, DateTime? birthDate, 
            EDocumentType documentType, string? documentNumber, string? address, bool isActive)
        {
            CreatedAt = DateTime.Now;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            DocumentType = documentType;
            DocumentNumber = documentNumber;
            Address = address;
            IsActive = isActive;
        }

        public void Update(string? firstName, string? lastName, DateTime? birthDate,
            EDocumentType documentType, string? documentNumber, string? address, bool isActive)
        {
            LastUpdate = DateTime.Now;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            DocumentType = documentType;
            DocumentNumber = documentNumber;
            Address = address;
            IsActive = isActive;
        }

        public bool ValidationIsRequired()
            => DocumentType == EDocumentType.CPF;

        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public EDocumentType DocumentType { get; private set; }
        public string? DocumentNumber { get; private set; }
        public string? Address { get; private set; }
        public bool IsActive { get; private set; }
    }
}
