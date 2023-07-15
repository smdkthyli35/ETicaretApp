using ETicaretApp.Application.Exceptions;
using ETicaretApp.Application.Repositories;
using ETicaretApp.Application.Rules.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApp.Application.Features.Commands.Product.RemoveProduct
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequest, RemoveProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly ProductBusinessRules _productBusinessRules;

        public RemoveProductCommandHandler(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, ProductBusinessRules productBusinessRules)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _productBusinessRules = productBusinessRules;
        }

        public async Task<RemoveProductCommandResponse> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productBusinessRules.IsExistProduct(request.Id);

            await _productWriteRepository.RemoveAsync(request.Id);
            await _productWriteRepository.SaveAsync();

            return new();
        }
    }
}
