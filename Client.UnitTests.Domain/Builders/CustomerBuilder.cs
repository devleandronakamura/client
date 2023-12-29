using Client.Domain.Entities;
using Client.Domain.Enums;

namespace Client.UnitTests.Domain.Builders
{
    public class CustomerBuilder
    {
        private string _firstName = "João";
        private string _lastName = "Silva";
        private string _documentNumber = "661.712.200-35";
        private EDocumentType _documentType = EDocumentType.CPF;
        private DateTime _birthDate = DateTime.Now;
        private readonly string _address = "Av São João, 313 - República - São Paulo";
        private readonly bool _isActive = true;

        public static CustomerBuilder New()
        {
            return new CustomerBuilder();
        }

        public CustomerBuilder WithDocumentType(EDocumentType documentType)
        {
            _documentType = documentType;
            return this;
        }

        public CustomerBuilder WithDocumentNumber(string documentNumber)
        {
            _documentNumber = documentNumber;
            return this;
        }

        public Customer Build()
        {
            return new Customer
                (
                    firstName: _firstName, 
                    lastName: _lastName, 
                    birthDate: _birthDate, 
                    documentNumber: _documentNumber, 
                    documentType: _documentType, 
                    address: _address, 
                    isActive: _isActive
                );
        }
    }
}
