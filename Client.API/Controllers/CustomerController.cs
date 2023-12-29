using Client.Application.DTOs.InputModels;
using Client.Application.DTOs.ViewModels;
using Client.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Client.API.Controllers
{
    [Route(WebConstants.CustomerRouteName)]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServiceApp _customerServiceApp;

        public CustomerController(ICustomerServiceApp customerServiceApp)
        {
            _customerServiceApp = customerServiceApp;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> GetAllAsync()
        {
            try
            {
                //_logger.LogInformation("Try to get all users");
                var customers = await _customerServiceApp.GetAllAsync();

                return Ok(customers);
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error to get all users. Details {0}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CustomerViewModel>> AddAsync(AddCustomerInputModel customer)
        {
            try
            {
                //_logger.LogInformation("Try to get all users");
                var resultResponse = await _customerServiceApp.AddAsync(customer);

                if (!resultResponse.IsValid)
                    return BadRequest(resultResponse.ErrorMessage);

                return Ok(resultResponse.Data);
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error to get all users. Details {0}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CustomerViewModel>> GetByIdAsync(Guid id)
        {
            try
            {
                //_logger.LogInformation("Try to get all users");
                var resultResponse = await _customerServiceApp.GetByIdAsync(id);

                if (resultResponse.IsValid)
                    return Ok(resultResponse.Data);
                else
                    return BadRequest(resultResponse.ErrorMessage);
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error to get all users. Details {0}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                //_logger.LogInformation("Try to get all users");
                var resultResponse = await _customerServiceApp.DeleteByIdAsync(id);

                if (resultResponse.IsValid)
                    return NoContent();
                else
                    return BadRequest(resultResponse.ErrorMessage);
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error to get all users. Details {0}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> UpdateAsync(Guid id, EditCustomerInputModel updateViewModel)
        {
            try
            {
                //_logger.LogInformation("Try to get all users");
                var resultResponse = await _customerServiceApp.UpdateAsync(id, updateViewModel);

                if (resultResponse.IsValid)
                    return NoContent();
                else
                    return BadRequest(resultResponse.ErrorMessage);
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error to get all users. Details {0}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
