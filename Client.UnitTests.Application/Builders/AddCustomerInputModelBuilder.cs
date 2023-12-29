using Client.Application.DTOs.InputModels;
using Client.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.UnitTests.Application.Builders
{
    public class AddCustomerInputModelBuilder
    {
        private string _firstName = "João";
        private string _lastName = "Silva";
        private string _documentNumber = "661.712.200-35";
        private EDocumentType _documentType = EDocumentType.CPF;
        private DateTime _birthDate = DateTime.Now;
        private readonly string _address = "Av São João, 313 - República - São Paulo";

        public static AddCustomerInputModelBuilder New()
        {
            return new AddCustomerInputModelBuilder();
        }

        public AddCustomerInputModelBuilder WithDocumentType(EDocumentType documentType)
        {
            _documentType = documentType;
            return this;
        }

        public AddCustomerInputModelBuilder WithDocumentNumber(string documentNumber)
        {
            _documentNumber = documentNumber;
            return this;
        }

        public AddCustomerInputModel Build()
        {
            return new AddCustomerInputModel()
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
