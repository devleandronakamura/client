using Client.Domain.Entities;

namespace Client.Domain.Interfaces.Services
{
    public interface ICustomerServiceDomain
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(Guid id);
        Task DeleteByIdAsync(Customer customer);
        Task<ResultResponse<Customer>> UpdateAsync(Customer customer);
        Task<ResultResponse<Customer>> AddAsync(Customer customer);
    }
}