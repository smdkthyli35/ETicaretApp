using MediatR;

namespace ETicaretApp.Application.Features.Queries.Basket.GetBasketItem
{
    public class GetBasketItemQueryRequest : IRequest<List<GetBasketItemQueryResponse>>
    {
    }
}