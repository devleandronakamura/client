using Client.Domain.Entities;
using Client.Domain.Enums;
using Client.Domain.Interfaces.Repositories;
using Client.Domain.Interfaces.Services;
using Client.Util.ExtensionMethods;

namespace Client.Domain.Services
{
    public class CustomerServiceDomain : ICustomerServiceDomain
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerServiceDomain(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ResultResponse<Customer>> AddAsync(Customer customer)
        {
            var resultResponse = new ResultResponse<Customer>();

            if (customer.ValidationIsRequired())
            {
                var documentNumberExists = await _customerRepository.DocumentNumberIsValidAsync(customer.DocumentNumber);

                if (documentNumberExists)
                {
                    resultResponse.SetErrorMessage(EStatusCode.BadRequest, $"O CPF ({customer.DocumentNumber}) já está cadastrado.");
                    return resultResponse;
                }
            }

            var customerAdded = await _customerRepository.AddAsync(customer);
            resultResponse.SetData(customerAdded);
            return resultResponse;
        }

        public async Task DeleteByIdAsync(Customer customer)
        {
            customer.Delete();
            await _customerRepository.DeleteByIdAsync(customer);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public async Task<ResultResponse<Customer>> UpdateAsync(Customer customer)
        {
            var resultResponse = new ResultResponse<Customer>();

            if (customer.ValidationIsRequired())
            {
                var customerIdDataBase = await _customerRepository.GetIdByDocumentNumberAsync(customer.DocumentNumber);

                if (!customerIdDataBase.IsEmpty() && customerIdDataBase != customer.Id)
                {
                    resultResponse.SetErrorMessage(EStatusCode.BadRequest, $"O CPF ({customer.DocumentNumber}) já está cadastrado.");
                    return resultResponse;
                }
            }

            await _customerRepository.UpdateAsync(customer);
            resultResponse.SetData(customer);
            return resultResponse;
        }
    }
}
