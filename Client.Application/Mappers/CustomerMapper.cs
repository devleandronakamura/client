using Client.Application.DTOs.InputModels;
using Client.Application.DTOs.ViewModels;
using Client.Domain.Entities;

namespace Client.Application.Mappers
{
    public static class CustomerMapper
    {
        public static List<CustomerViewModel> ToCustomersViewModel(IEnumerable<Customer> customers)
        {
            var customersViewModel = new List<CustomerViewModel>();

            foreach (var customer in customers)
            {
                customersViewModel.Add(new CustomerViewModel()
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    BirthDate = customer.BirthDate,
                    DocumentType = customer.DocumentType.ToString(),
                    DocumentNumber = customer.DocumentNumber,
                    Address = customer.Address,
                    IsActive = customer.IsActive
                });
            }

            return customersViewModel;
        }

        public static Customer ToCustomer(AddCustomerInputModel addCustomer)
        {
            return new Customer(firstName: addCustomer.FirstName,
                                lastName: addCustomer.LastName,
                                birthDate: addCustomer.BirthDate,
                                documentType: addCustomer.DocumentType,
                                documentNumber: Util.Functions.MapOnlyNumbers(addCustomer.DocumentNumber),
                                address: addCustomer.Address,
                                isActive: addCustomer.IsActive);
        }

        public static CustomerViewModel ToCustomerViewModel(Customer customer)
        {
            return new CustomerViewModel()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                BirthDate = customer.BirthDate,
                DocumentType = customer.DocumentType.ToString(),
                DocumentNumber = customer.DocumentNumber,
                Address = customer.Address,
                IsActive = customer.IsActive
            };
        }
    }
}
