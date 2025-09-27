using Accounting.Core.Models;
using Accounting.Core.Repositories;
using Accounting.Core.Repositories.ProductInterface;
using Accounting.Core.Services.ProductService;
using Accounting.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Service.Services.ProductService
{
    public class ProductService:Service<Product>, IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWorks _unitOfWorks;
        public ProductService(IUnitOfWorks unitOfWorks, IGenericRepository<Product> repository,IProductRepository productRepository) : base(unitOfWorks, repository)
        {
            _repository = productRepository;
            _unitOfWorks = unitOfWorks;
        }

        public async Task BuyProduct(Product product)
        {
            var currentProduct =await _repository.GetByIdAsync(product.Id);
            if (currentProduct == null)
            {
                throw new Exceptions.NotFoundException($"{product.Id} not found");
            }

            currentProduct.Stock += product.Stock;

            Update(currentProduct);
        }
    }
}
