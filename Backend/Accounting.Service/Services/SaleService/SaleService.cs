using Accounting.Core.Models;
using Accounting.Core.Repositories;
using Accounting.Core.Repositories.ProductInterface;
using Accounting.Core.Repositories.SaleInterface;
using Accounting.Core.Services.ProductService;
using Accounting.Core.Services.SaleService;
using Accounting.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Service.Services.SaleService
{
    public class SaleService(IUnitOfWorks unitOfWorks, IGenericRepository<Sale> repository, ISaleRepository saleRepository, IProductService productService) : Service<Sale>(unitOfWorks, repository), ISaleService
    {
        private readonly ISaleRepository _saleRepository= saleRepository;
        private readonly IProductService _productService = productService;
        public async Task SaleProduct(Sale sale)
        {
             var product= await _productService.GetByIdAsync(sale.ProductId);
            product.Stock= product.Stock - sale.Quantity;
             _productService.Update(product);

            sale.TotalPrice=sale.UnitPrice* sale.Quantity;

           await AddAsync(sale);

        }
    }
}
