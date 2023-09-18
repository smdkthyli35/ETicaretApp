
using ETicaretApp.Application.ViewModels.Baskets;
using ETicaretApp.Domain.Entities;

namespace ETicaretApp.Application.Abstractions.Services
{
    public interface IBasketService
    {
        Task<List<BasketItem>> GetBasketItemAsync();
        Task AddItemToBasketAsync(VM_Create_BasketItem basketItem);
        Task UpdateQuantityAsync(VM_Update_BasketItem basketItem);
        Task RemoveBasketItemAsync(string basketItemId);
        Basket? GetUserActiveBasket { get; }
    }
}