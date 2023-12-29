using Client.Domain.Entities;
using Client.Domain.Enums;
using Client.Domain.Interfaces.Repositories;
using Client.Domain.Services;
using Client.UnitTests.Domain.Builders;
using Moq;
using Xunit;

namespace Client.UnitTests.Domain.Services
{
    public class CustomerServiceDomainTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock = new Mock<ICustomerRepository>();

        private CustomerServiceDomain GetCustomerServiceDomain()
        {
            return new CustomerServiceDomain(_customerRepositoryMock.Object);
        }

        #region GET

        [Fact(DisplayName = "GET: Com Id existente (Valido)")]
        public async Task GetByIdAsync_ThatExists()
        {
            //Arrange
            var id = Guid.NewGuid();
            var expectedCustomer = CustomerBuilder.New().Build();

            _customerRepositoryMock
                .Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync(expectedCustomer);

            var serviceMock = GetCustomerServiceDomain();

            //Act
            var customer = await serviceMock.GetByIdAsync(id);

            //Assert
            Assert.Equal(expectedCustomer.Id, customer.Id);

            _customerRepositoryMock.Verify(x => x.GetByIdAsync(id), Times.Once);
        }

        [Fact(DisplayName = "GET: Com Id não existente (Não Valido)")]
        public async Task GetByIdAsync_ThatDoesNotExist()
        {
            //Arrange
            var idMock = Guid.NewGuid();
            var idSeached = Guid.NewGuid();
            var expectedCustomer = default(Customer);//CustomerBuilder.New().Build();

            _customerRepositoryMock
                .Setup(x => x.GetByIdAsync(idMock))
                .ReturnsAsync(expectedCustomer);

            var serviceMock = GetCustomerServiceDomain();

            //Act
            var customer = await serviceMock.GetByIdAsync(idSeached);

            //Assert
            Assert.Equal(expectedCustomer, customer);

            _customerRepositoryMock.Verify(x => x.GetByIdAsync(idSeached), Times.Once);
        }

        #endregion

        #region DELETE

        [Fact(DisplayName = "DELETE: Com Id existente (Valido)")]
        public async Task DeleteByIdAsync_ThatExists()
        {
            //Arrange
            var customer = CustomerBuilder.New().Build();

            _customerRepositoryMock
                .Setup(x => x.DeleteByIdAsync(customer));

            var serviceMock = GetCustomerServiceDomain();

            //Act
            await serviceMock.DeleteByIdAsync(customer);

            //Assert
            _customerRepositoryMock.Verify(x => x.DeleteByIdAsync(customer), Times.Once);
        }

        #endregion

        #region PUT

        [Fact(DisplayName = "PUT: Valido")]
        public async Task EditAsync_ThatExists()
        {
            //Arrange
            var documentType = EDocumentType.RG;
            var customer = CustomerBuilder.New()
                                          .WithDocumentType(documentType)
                                          .Build();
            var resultResponse = new ResultResponse<Customer>();

            _customerRepositoryMock
                .Setup(x => x.UpdateAsync(customer));

            var serviceMock = GetCustomerServiceDomain();

            //Act
            await serviceMock.UpdateAsync(customer);

            //Assert
            _customerRepositoryMock.Verify(x => x.UpdateAsync(customer), Times.Once);
        }

        #endregion

        #region POST

        [Fact(DisplayName = "POST: Valido")]
        public async Task AddAsync_Valid()
        {
            //Arrange
            var documentType = EDocumentType.RG;
            var customer = CustomerBuilder.New()
                                          .WithDocumentType(documentType)
                                          .Build();
            var resultResponse = new ResultResponse<Customer>();
            resultResponse.SetData(customer);
            var expectedStatusCode = EStatusCode.Ok;

            _customerRepositoryMock
                .Setup(x => x.AddAsync(customer))
                .ReturnsAsync(customer);

            var serviceMock = GetCustomerServiceDomain();

            //Act
            var result = await serviceMock.AddAsync(customer);
            var statusCode = result.StatusCode;

            //Assert
            Assert.Equal(expectedStatusCode, statusCode);

            _customerRepositoryMock.Verify(x => x.AddAsync(customer), Times.Once);
        }

        #endregion
    }
}
