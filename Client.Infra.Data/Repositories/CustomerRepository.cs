using Client.Domain.Entities;
using Client.Domain.Interfaces.Repositories;
using Client.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Client.Infra.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        protected ClientDbContext _context;

        public CustomerRepository(ClientDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            _context.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task DeleteByIdAsync(Customer customer)
        {
            _context.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DocumentNumberIsValidAsync(string? documentNumber)
        {
            return await _context.Customers.AnyAsync(x => x.DocumentNumber == documentNumber);
        }

        public async Task<Guid> GetIdByDocumentNumberAsync(string? documentNumber)
        {
            return await _context.Customers.Where(x => x.DocumentNumber == documentNumber)
                                           .Select(x => x.Id)
                                           .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.Where(x => !x.IsDeleted)
                                           .AsNoTracking()
                                           .ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Update(customer);
            await _context.SaveChangesAsync();
        }
    }
}
