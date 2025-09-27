using Accounting.API.Filters;
using Accounting.Core.DTOs.UpdateDTOs;
using Accounting.Core.DTOs;
using Accounting.Core.Models;
using Accounting.Core.Services.ProductService;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = _productService.GetAll();
            var dtos = _mapper.Map<List<ProductDto>>(products).ToList();
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            var dto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, dto));
        }


        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            var UserId = 1; // token olmadığı için şuanlık 1 yaptım
            var product = await _productService.GetByIdAsync(id);
            product.UpdatedBy = UserId;
            _productService.ChangeStatus(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            // token olmadığı için şuanlık 1 yaptım
            var UserId = 1;
            var processedEntity = _mapper.Map<Product>(productDto);
            processedEntity.Createdby = UserId;
            processedEntity.UpdatedBy = UserId;
            var product = await _productService.AddAsync(processedEntity);

            var productResponseDto = _mapper.Map<ProductDto>(product);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productResponseDto));
        }


        [HttpPost("[Action]")]
        public async Task<IActionResult> BuyProduct(ProductDto productDto)
        {
            // token olmadığı için şuanlık 1 yaptım
            var UserId = 1;
            var processedEntity = _mapper.Map<Product>(productDto);
            processedEntity.Createdby = UserId;
            processedEntity.UpdatedBy = UserId;
            await _productService.BuyProduct(processedEntity);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            // token olmadığı için şuanlık 1 yaptım
            var UserId = 1;
            var currentProduct = await _productService.GetByIdAsync(productUpdateDto.Id);

            currentProduct.UpdatedBy = UserId;
            currentProduct.Createdby = UserId;

            _productService.Update(currentProduct);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
