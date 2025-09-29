using Accounting.API.Filters;
using Accounting.Core.DTOs.UpdateDTOs;
using Accounting.Core.DTOs;
using Accounting.Core.Models;
using Accounting.Core.Services.SaleService;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : CustomBaseController
    {
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;
        public SalesController(ISaleService saleService, IMapper mapper)
        {
            _saleService = saleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sales = _saleService.GetAll();
            var dtos = _mapper.Map<List<SaleDto>>(sales).ToList();
            return CreateActionResult(CustomResponseDto<List<SaleDto>>.Success(200, dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Sale>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sale = await _saleService.GetByIdAsync(id);
            var dto = _mapper.Map<SaleDto>(sale);
            return CreateActionResult(CustomResponseDto<SaleDto>.Success(200, dto));
        }


        [ServiceFilter(typeof(NotFoundFilter<Sale>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            var UserId = GetUserFromToken();
            var sale = await _saleService.GetByIdAsync(id);
            sale.UpdatedBy = UserId;
            _saleService.ChangeStatus(sale);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(SaleDto saleDto)
        {
            var UserId = GetUserFromToken();
            var processedEntity = _mapper.Map<Sale>(saleDto);
            processedEntity.Createdby = UserId;
            processedEntity.UpdatedBy = UserId;
            var sale = await _saleService.AddAsync(processedEntity);

            var saleResponseDto = _mapper.Map<SaleDto>(sale);

            return CreateActionResult(CustomResponseDto<SaleDto>.Success(201, saleResponseDto));
        }


        [HttpPost("[Action]")]
        public async Task<IActionResult> SaleProduct(SaleDto saleDto)
        {
            var UserId = GetUserFromToken();
            var processedEntity = _mapper.Map<Sale>(saleDto);
            processedEntity.Createdby = UserId;
            processedEntity.UpdatedBy = UserId;
            await _saleService.SaleProduct(processedEntity);


            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPut]
        public async Task<IActionResult> Update(SaleUpdateDto saleUpdateDto)
        {
            // token olmadığı için şuanlık 1 yaptım
            var UserId = 1;
            var currentSale = await _saleService.GetByIdAsync(saleUpdateDto.Id);

            currentSale.UpdatedBy = UserId;
            currentSale.Createdby = UserId;

            _saleService.Update(currentSale);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
