using Accounting.API.Filters;
using Accounting.Core.DTOs.UpdateDTOs;
using Accounting.Core.DTOs;
using Accounting.Core.Models;
using Accounting.Core.Services.PaymentService;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : CustomBaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        public PaymentsController(IPaymentService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var payments = _paymentService.GetAll();
            var dtos = _mapper.Map<List<PaymentDto>>(payments).ToList();
            return CreateActionResult(CustomResponseDto<List<PaymentDto>>.Success(200, dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Payment>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var payment = await _paymentService.GetByIdAsync(id);
            var dto = _mapper.Map<PaymentDto>(payment);
            return CreateActionResult(CustomResponseDto<PaymentDto>.Success(200, dto));
        }


        [ServiceFilter(typeof(NotFoundFilter<Payment>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            var UserId = GetUserFromToken();
            var payment = await _paymentService.GetByIdAsync(id);
            payment.UpdatedBy = UserId;
            _paymentService.ChangeStatus(payment);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(PaymentDto paymentDto)
        {
            var UserId = GetUserFromToken();
            var processedEntity = _mapper.Map<Payment>(paymentDto);
            processedEntity.Createdby = UserId;
            processedEntity.UpdatedBy = UserId;
            var payment = await _paymentService.AddAsync(processedEntity);

            var paymentResponseDto = _mapper.Map<PaymentDto>(payment);

            return CreateActionResult(CustomResponseDto<PaymentDto>.Success(201, paymentResponseDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(PaymentUpdateDto paymentUpdateDto)
        {
            var UserId = GetUserFromToken();
            var currentPayment = await _paymentService.GetByIdAsync(paymentUpdateDto.Id);

            currentPayment.UpdatedBy = UserId;
            currentPayment.Createdby = UserId;

            _paymentService.Update(currentPayment);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
