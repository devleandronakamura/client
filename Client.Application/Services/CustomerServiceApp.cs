using Client.Application.DTOs.InputModels;
using Client.Application.DTOs.ViewModels;
using Client.Application.Interfaces;
using Client.Application.Mappers;
using Client.Domain.Entities;
using Client.Domain.Enums;
using Client.Domain.Interfaces.Services;
using Client.Util.ExtensionMethods;

namespace Client.Application.Services
{
    public class CustomerServiceApp : ICustomerServiceApp
    {
        private readonly ICustomerServiceDomain _customerServiceDomain;

        public CustomerServiceApp(ICustomerServiceDomain customerServiceDomain)
        {
            _customerServiceDomain = customerServiceDomain;
        }

        public async Task<ResultResponse<CustomerViewModel>> AddAsync(AddCustomerInputModel customerInputModel)
        {
            var resultResponse = new ResultResponse<CustomerViewModel>();

            var customer = CustomerMapper.ToCustomer(customerInputModel);

            var result = await _customerServiceDomain.AddAsync(customer);

            if (!result.IsValid)
            {
                resultResponse.SetErrorMessage(result.StatusCode, result.ErrorMessage);
                return resultResponse;
            }

            var customerAdded = result.Data;
            resultResponse.SetData(CustomerMapper.ToCustomerViewModel(customerAdded));

            return resultResponse;
        }

        public async Task<ResultResponse<CustomerViewModel>> DeleteByIdAsync(Guid id)
        {
            var resultResponse = new ResultResponse<CustomerViewModel>();

            if (id.IsEmpty())
            {
                resultResponse.SetErrorMessage(EStatusCode.BadRequest, "Id is empty.");
                return resultResponse;
            }

            var customer = await _customerServiceDomain.GetByIdAsync(id);

            if (customer == null)
            {
                resultResponse.SetErrorMessage(EStatusCode.BadRequest, $"Customer [id={id}] was not found.");
                return resultResponse;
            }

            await _customerServiceDomain.DeleteByIdAsync(customer);
            resultResponse.SetStatus(EStatusCode.NoContent);

            return resultResponse;
        }

        public async Task<IEnumerable<CustomerViewModel>> GetAllAsync()
        {
            var customers = await _customerServiceDomain.GetAllAsync();

            return CustomerMapper.ToCustomersViewModel(customers);
        }

        public async Task<ResultResponse<CustomerViewModel>> GetByIdAsync(Guid id)
        {
            var resultResponse = new ResultResponse<CustomerViewModel>();

            if (id.IsEmpty())
            {
                resultResponse.SetErrorMessage(EStatusCode.BadRequest, "Id is empty");
                return resultResponse;
            }

            var customer = await _customerServiceDomain.GetByIdAsync(id);

            if (customer == null)
            {
                resultResponse.SetErrorMessage(EStatusCode.BadRequest, $"Customer [id={id}] was not found.");
                return resultResponse;
            }

            var customerViewModel = CustomerMapper.ToCustomerViewModel(customer);

            resultResponse.SetData(customerViewModel);

            return resultResponse;
        }

        public async Task<ResultResponse<CustomerViewModel>> UpdateAsync(Guid id, EditCustomerInputModel updateViewModel)
        {
            var resultResponse = new ResultResponse<CustomerViewModel>();

            if (id.IsEmpty())
            {
                resultResponse.SetErrorMessage(EStatusCode.BadRequest, "Id is empty.");
                return resultResponse;
            }

            var customer = await _customerServiceDomain.GetByIdAsync(id);

            if (customer == null)
            {
                resultResponse.SetErrorMessage(EStatusCode.BadRequest, $"Customer [id={id}] was not found.");
                return resultResponse;
            }

            customer.Update(firstName: updateViewModel.FirstName,
                            lastName: updateViewModel.LastName,
                            birthDate: updateViewModel.BirthDate,
                            documentType: updateViewModel.DocumentType,
                            documentNumber: Util.Functions.MapOnlyNumbers(updateViewModel.DocumentNumber),
                            address: updateViewModel.Address,
                            isActive: updateViewModel.IsActive);

            var result = await _customerServiceDomain.UpdateAsync(customer);

            if (!result.IsValid)
            {
                resultResponse.SetErrorMessage(result.StatusCode, result.ErrorMessage);
                return resultResponse;
            }

            resultResponse.SetStatus(EStatusCode.NoContent);

            return resultResponse;
        }
    }
}