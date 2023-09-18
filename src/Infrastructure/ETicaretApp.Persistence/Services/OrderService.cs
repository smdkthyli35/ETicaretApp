using ETicaretApp.Application.Abstractions.Services;
using ETicaretApp.Application.Dtos.Order;
using ETicaretApp.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApp.Persistence.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderWriteRepository _orderWriteRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository)
        {
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            await _orderWriteRepository.AddAsync(new()
            {
                Address = createOrderDto.Address,
                BasketId = Guid.Parse(createOrderDto.BasketId),
                Description = createOrderDto.Description,
            });

            await _orderWriteRepository.SaveAsync();
        }
    }
}
