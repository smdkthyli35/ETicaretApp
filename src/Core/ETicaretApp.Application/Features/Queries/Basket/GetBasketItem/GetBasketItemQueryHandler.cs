using ETicaretApp.Application.Abstractions.Services;
using ETicaretApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApp.Application.Features.Queries.Basket.GetBasketItem
{
    public class GetBasketItemQueryHandler : IRequestHandler<GetBasketItemQueryRequest, List<GetBasketItemQueryResponse>>
    {
        private readonly IBasketService _basketService;

        public GetBasketItemQueryHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<List<GetBasketItemQueryResponse>> Handle(GetBasketItemQueryRequest request, CancellationToken cancellationToken)
        {
            List<BasketItem> basketItems = await _basketService.GetBasketItemAsync();

            return basketItems.Select(ba => new GetBasketItemQueryResponse()
            {
                BasketItemId = ba.Id.ToString(),
                Name = ba.Product.Name,
                Price = ba.Product.Price,
                Quantity = ba.Quantity
            }).ToList();
        }
    }
}
