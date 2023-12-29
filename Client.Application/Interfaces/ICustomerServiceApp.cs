using Client.Application.DTOs.InputModels;
using Client.Application.DTOs.ViewModels;
using Client.Domain.Entities;

namespace Client.Application.Interfaces
{
    public interface ICustomerServiceApp
    {
        Task<IEnumerable<CustomerViewModel>> GetAllAsync();
        Task<ResultResponse<CustomerViewModel>> GetByIdAsync(Guid id);
        Task<ResultResponse<CustomerViewModel>> DeleteByIdAsync(Guid id);
        Task<ResultResponse<CustomerViewModel>> UpdateAsync(Guid id, EditCustomerInputModel updateViewModel);
        Task<ResultResponse<CustomerViewModel>> AddAsync(AddCustomerInputModel customerInputModel);
    }
}