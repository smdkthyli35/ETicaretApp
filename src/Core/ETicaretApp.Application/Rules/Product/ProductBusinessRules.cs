using ETicaretApp.Application.Exceptions;
using ETicaretApp.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApp.Application.Rules.Product
{
    public class ProductBusinessRules
    {
        private readonly IProductReadRepository _productReadRepository;

        public ProductBusinessRules(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task IsExistProduct(string id)
        {
            Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(id, false);

            if (product is null)
                throw new BusinessException($"{id} id product does not found!");
        }
    }
}
