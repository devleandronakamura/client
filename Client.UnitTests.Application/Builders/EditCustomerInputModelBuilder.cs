using Client.Application.DTOs.InputModels;
using Client.Domain.Enums;

namespace Client.UnitTests.Application.Builders
{
    public class EditCustomerInputModelBuilder
    {
        private string _firstName = "João";
        private string _lastName = "Silva";
        private string _documentNumber = "661.712.200-35";
        private EDocumentType _documentType = EDocumentType.CPF;
        private DateTime _birthDate = DateTime.Now;
        private readonly string _address = "Av São João, 313 - República - São Paulo";

        public static EditCustomerInputModelBuilder New()
        {
            return new EditCustomerInputModelBuilder();
        }

        public EditCustomerInputModelBuilder WithDocumentType(EDocumentType documentType)
        {
            _documentType = documentType;
            return this;
        }

        public EditCustomerInputModelBuilder WithDocumentNumber(string documentNumber)
        {
            _documentNumber = documentNumber;
            return this;
        }

        public EditCustomerInputModel Build()
        {
            return new EditCustomerInputModel()
            {
                FirstName = _firstName,
                LastName = _lastName,
                BirthDate = _birthDate,
                DocumentNumber = _documentNumber,
                Address = _address,
                DocumentType = _documentType,
            };
        }
    }
}