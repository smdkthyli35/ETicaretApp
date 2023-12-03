using ETicaretApp.Application.Abstractions.Services;
using ETicaretApp.Application.Dtos.Order;
using ETicaretApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApp.Application.Features.Queries.Order.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQueryRequest, GetOrderByIdQueryResponse>
    {
        private readonly IOrderService _orderService;

        public GetOrderByIdQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
        {
            SingleOrder order = await _orderService.GetOrderByIdAsync(request.Id);
            return new()
            {
                Id = order.Id,
                Address = order.Address,
                BasketItems = order.BasketItems,
                CreatedDate = order.CreatedDate,
                Description = order.Description,
                OrderCode = order.OrderCode,
                Completed = order.Completed
            };
        }
    }
}
