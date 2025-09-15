using Accounting.API.Filters;
using Accounting.Core.DTOs;
using Accounting.Core.DTOs.UpdateDTOs;
using Accounting.Core.Models;
using Accounting.Core.Services.CustomerService;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : CustomBaseController
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        public CustomersController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           var customers = _customerService.GetAll();
            var dtos = _mapper.Map<List<CustomerDto>>(customers).ToList();
            return CreateActionResult(CustomResponseDto<List<CustomerDto>>.Success(200, dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Customer>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            var dto = _mapper.Map<CustomerDto>(customer);
            return CreateActionResult(CustomResponseDto<CustomerDto>.Success(200, dto));
        }


        [ServiceFilter(typeof(NotFoundFilter<Customer>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            var UserId = 1; // token olmadığı için şuanlık 1 yaptım
            var customer = await _customerService.GetByIdAsync(id);
            customer.UpdatedBy = UserId;
            _customerService.ChangeStatus(customer);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CustomerDto customerDto)
        {
            // token olmadığı için şuanlık 1 yaptım
            var UserId = 1;
            var processedEntity= _mapper.Map<Customer>(customerDto);
            processedEntity.Createdby = UserId;
            processedEntity.UpdatedBy = UserId;
            var customer = await _customerService.AddAsync(processedEntity);

            var customerResponseDto = _mapper.Map<CustomerDto>(customer);

            return CreateActionResult(CustomResponseDto<CustomerDto>.Success(201, customerResponseDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CustomerUpdateDto customerUpdateDto)
        {
            // token olmadığı için şuanlık 1 yaptım
            var UserId = 1;
            var currentCustomer = await _customerService.GetByIdAsync(customerUpdateDto.Id);
            
            currentCustomer.UpdatedBy = UserId;
            currentCustomer.Createdby = UserId;

            _customerService.Update(currentCustomer);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

    }
}
