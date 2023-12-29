using Client.Application.Services;
using Client.Domain.Entities;
using Client.Domain.Enums;
using Client.Domain.Interfaces.Services;
using Client.UnitTests.Application.Builders;
using Client.UnitTests.Domain.Builders;
using Moq;
using Xunit;

namespace Client.UnitTests.Application.Services
{
    public class CustomerServiceAppTests 
    {
        private readonly Mock<ICustomerServiceDomain> _customerServiceDomainMock = new Mock<ICustomerServiceDomain>();

        private CustomerServiceApp GetCustomerServiceApp()
        {
            return new CustomerServiceApp(_customerServiceDomainMock.Object);
        }

        #region GET

        [Fact(DisplayName = "GET: Com Id existente (Valido)")]
        public async Task GetByIdAsync_ThatExists()
        {
            //Arrange
            var id = Guid.NewGuid();
            var expectedCustomer = CustomerBuilder.New().Build();
            var expectedResponseCode = EStatusCode.Ok;

            _customerServiceDomainMock
                .Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync(expectedCustomer);

            var serviceMock = GetCustomerServiceApp();

            //Act
            var result = await serviceMock.GetByIdAsync(id);
            var responseCode = result.StatusCode;
            var documentNumber = result.Data.DocumentNumber;

            //Assert
            Assert.Equal(expectedResponseCode, responseCode);
            Assert.Equal(expectedCustomer.DocumentNumber, documentNumber);

            _customerServiceDomainMock.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact(DisplayName = "GET: Com Id não encontrado (Não Valido)")]
        public async Task GetByIdAsync_ThatDoesNotExist()
        {
            //Arrange
            var idSearched = Guid.NewGuid();
            var idDataBase = Guid.NewGuid();
            var expectedResponseCode = EStatusCode.BadRequest;
            var expectedErrorMessage = $"Customer [id={idSearched}] was not found.";

            _customerServiceDomainMock
                .Setup(x => x.GetByIdAsync(idDataBase));

            var serviceMock = GetCustomerServiceApp();

            //Act
            var result = await serviceMock.GetByIdAsync(idSearched);
            var responseCode = result.StatusCode;
            var errorMessage = result.ErrorMessage;

            //Assert
            Assert.Equal(expectedResponseCode, responseCode);
            Assert.Equal(expectedErrorMessage, errorMessage);

            _customerServiceDomainMock.Verify(x => x.GetByIdAsync(idSearched), Times.Once);
        }

        [Fact(DisplayName = "GET: Com Id empty (Não Valido)")]
        public async Task GetByIdAsync_ThatIsEmpty()
        {
            //Arrange
            var id = Guid.Empty;
            var expectedResponseCode = EStatusCode.BadRequest;
            var expectedErrorMessage = "Id is empty";

            _customerServiceDomainMock
                .Setup(x => x.GetByIdAsync(id));

            var serviceMock = GetCustomerServiceApp();

            //Act
            var result = await serviceMock.GetByIdAsync(id);
            var responseCode = result.StatusCode;
            var errorMessage = result.ErrorMessage;

            //Assert
            Assert.Equal(expectedResponseCode, responseCode);
            Assert.Equal(expectedErrorMessage, errorMessage);

            _customerServiceDomainMock.Verify(x => x.GetByIdAsync(id), Times.Never);
        }

        #endregion

        #region DELETE

        [Fact(DisplayName = "DELETE: Com Id existente (Valido)")]
        public async Task DeleteByIdAsync_ThatExists()
        {
            //Arrange
            var id = Guid.NewGuid();
            var customer = CustomerBuilder.New().Build();
            var expectedResponseCode = EStatusCode.NoContent;

            _customerServiceDomainMock
                .Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync(customer);

            _customerServiceDomainMock
                .Setup(x => x.DeleteByIdAsync(customer));

            var serviceMock = GetCustomerServiceApp();

            //Act
            var result = await serviceMock.DeleteByIdAsync(id);
            var responseCode = result.StatusCode;

            //Assert
            Assert.Equal(expectedResponseCode, responseCode);

            _customerServiceDomainMock.Verify(x => x.DeleteByIdAsync(customer), Times.Once);
        }

        [Fact(DisplayName = "DELETE: Com Id empty (Não Valido)")]
        public async Task DeleteByIdAsync_WithIdEmptyExist()
        {
            //Arrange
            var idSearched = Guid.Empty;
            var idDataBase = Guid.NewGuid();
            var customer = default(Customer);
            var expectedResponseCode = EStatusCode.BadRequest;
            var expectedErrorMessage = "Id is empty.";

            _customerServiceDomainMock
                .Setup(x => x.GetByIdAsync(idDataBase))
                .ReturnsAsync(customer);

            var serviceMock = GetCustomerServiceApp();

            //Act
            var result = await serviceMock.DeleteByIdAsync(idSearched);
            var responseCode = result.StatusCode;
            var errorMessage = result.ErrorMessage;

            //Assert
            Assert.Equal(expectedResponseCode, responseCode);
            Assert.Equal(expectedErrorMessage, errorMessage);

            _customerServiceDomainMock.Verify(x => x.DeleteByIdAsync(customer), Times.Never);
        }

        [Fact(DisplayName = "DELETE: Com Id não existente (Não Valido)")]
        public async Task DeleteByIdAsync_ThatDoesNotExist()
        {
            //Arrange
            var idSearched = Guid.NewGuid();
            var idDataBase = Guid.NewGuid();
            var customer = default(Customer);
            var expectedResponseCode = EStatusCode.BadRequest;
            var expectedErrorMessage = $"Customer [id={idSearched}] was not found.";

            _customerServiceDomainMock
                .Setup(x => x.GetByIdAsync(idDataBase))
                .ReturnsAsync(customer);

            var serviceMock = GetCustomerServiceApp();

            //Act
            var result = await serviceMock.DeleteByIdAsync(idSearched);
            var responseCode = result.StatusCode;
            var errorMessage = result.ErrorMessage;

            //Assert
            Assert.Equal(expectedResponseCode, responseCode);
            Assert.Equal(expectedErrorMessage, errorMessage);

            _customerServiceDomainMock.Verify(x => x.DeleteByIdAsync(customer), Times.Never);
        }

        #endregion

        #region POST

        [Fact(DisplayName = "POST: Valido")]
        public async Task AddAsync_Valid()
        {
            //Arrange
            string expectedDocumentNumber = "66171220035";
            var expectedResponseCode = EStatusCode.Ok;

            var addCustomer = AddCustomerInputModelBuilder.New()
                                                          .WithDocumentNumber(expectedDocumentNumber)
                                                          .Build();
            var customer = CustomerBuilder.New()
                                          .WithDocumentNumber(expectedDocumentNumber)
                                          .Build();
            
            var resultResponse = new ResultResponse<Customer>();
            resultResponse.SetData(customer);

            _customerServiceDomainMock
                .Setup(x => x.AddAsync(It.IsAny<Customer>()))
                .ReturnsAsync(resultResponse);

            var serviceMock = GetCustomerServiceApp();

            //Act
            var result = await serviceMock.AddAsync(addCustomer);
            var responseCode = result.StatusCode;
            var responseCustomerViewModel = result.Data;

            //Assert
            Assert.Equal(expectedResponseCode, responseCode);
            Assert.Equal(expectedDocumentNumber, responseCustomerViewModel.DocumentNumber);

            _customerServiceDomainMock.Verify(x => x.AddAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Fact(DisplayName = "POST: Com CPF já cadastrado (Não Válido)")]
        public async Task AddAsync_WithNameNotValid()
        {
            //Arrange
            string documentNumber = "47770239079";
            var documentType = EDocumentType.CPF;
            var expectedResponseCode = EStatusCode.BadRequest;
            var expectedErrorMessage = $"O CPF ({documentNumber}) já está cadastrado.";

            var addCustomer = AddCustomerInputModelBuilder.New()
                                                          .WithDocumentType(documentType)
                                                          .WithDocumentNumber(documentNumber)
                                                          .Build();

            var resultResponse = new ResultResponse<Customer>();
            resultResponse.SetErrorMessage(EStatusCode.BadRequest, expectedErrorMessage);

            _customerServiceDomainMock
                .Setup(x => x.AddAsync(It.IsAny<Customer>()))
                .ReturnsAsync(resultResponse);

            var serviceMock = GetCustomerServiceApp();

            //Act
            var result = await serviceMock.AddAsync(addCustomer);
            var responseCode = result.StatusCode;
            var responseErrorMessage = result.ErrorMessage;

            //Assert
            Assert.Equal(expectedResponseCode, responseCode);
            Assert.Equal(responseErrorMessage, expectedErrorMessage);

            _customerServiceDomainMock.Verify(x => x.AddAsync(It.IsAny<Customer>()), Times.Once);
        }

        #endregion

        #region POST

        [Fact(DisplayName = "PUT: Valido")]
        public async Task UpdateAsync_Valid()
        {
            //Arrange
            var expectedResponseCode = EStatusCode.NoContent;

            var id = Guid.NewGuid();
            var editCustomer = EditCustomerInputModelBuilder.New().Build();
            var customer = CustomerBuilder.New().Build();
            var resultResponse = new ResultResponse<Customer>();

            _customerServiceDomainMock
                .Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync(customer);

            _customerServiceDomainMock
                .Setup(x => x.UpdateAsync(customer))
                .ReturnsAsync(resultResponse);

            var serviceMock = GetCustomerServiceApp();

            //Act
            var result = await serviceMock.UpdateAsync(id, editCustomer);
            var responseCode = result.StatusCode;

            //Assert
            Assert.Equal(expectedResponseCode, responseCode);

            _customerServiceDomainMock.Verify(x => x.UpdateAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Fact(DisplayName = "PUT: Com CPF já cadastrado (Não Válido)")]
        public async Task UpdateAsync_CpfAlreadyInDatabase()
        {
            //Arrange
            var documentNumber = "24095933054";
            var documentType = EDocumentType.CPF;
            var expectedResponseCode = EStatusCode.BadRequest;
            var expectedMessageError = $"O CPF ({documentNumber}) já está cadastrado.";

            var id = Guid.NewGuid();
            var editCustomer = EditCustomerInputModelBuilder.New()
                                                            .WithDocumentType(documentType)
                                                            .WithDocumentNumber(documentNumber)
                                                            .Build();
            var customer = CustomerBuilder.New()
                                          .WithDocumentType(documentType)
                                          .WithDocumentNumber(documentNumber)
                                          .Build();

            var resultResponse = new ResultResponse<Customer>();
            resultResponse.SetErrorMessage(expectedResponseCode, expectedMessageError);

            _customerServiceDomainMock
                .Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync(customer);

            _customerServiceDomainMock
                .Setup(x => x.UpdateAsync(customer))
                .ReturnsAsync(resultResponse);

            var serviceMock = GetCustomerServiceApp();

            //Act
            var result = await serviceMock.UpdateAsync(id, editCustomer);
            var responseCode = result.StatusCode;
            var responseErrorMessage = result.ErrorMessage;

            //Assert
            Assert.Equal(expectedResponseCode, responseCode);
            Assert.Equal(expectedMessageError, responseErrorMessage);

            _customerServiceDomainMock.Verify(x => x.UpdateAsync(It.IsAny<Customer>()), Times.Once);
        }

        #endregion
    }
}