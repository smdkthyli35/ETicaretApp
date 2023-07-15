using ETicaretApp.Application.Exceptions;
using ETicaretApp.Application.Repositories;
using ETicaretApp.Application.Rules.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApp.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly ProductBusinessRules _productBusinessRules;

        public GetByIdProductQueryHandler(IProductReadRepository productReadRepository, ProductBusinessRules productBusinessRules)
        {
            _productReadRepository = productReadRepository;
            _productBusinessRules = productBusinessRules;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            await _productBusinessRules.IsExistProduct(request.Id);

            Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id, false);

            return new()
            {
                Id = product.Id.ToString(),
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            };
        }
    }
}
