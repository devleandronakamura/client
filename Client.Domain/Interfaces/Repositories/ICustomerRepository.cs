using Client.Domain.Entities;

namespace Client.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(Guid id);
        Task DeleteByIdAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task<bool> DocumentNumberIsValidAsync(string? documentNumber);
        Task<Guid> GetIdByDocumentNumberAsync(string? documentNumber);
        Task<Customer> AddAsync(Customer customer);
    }
}
