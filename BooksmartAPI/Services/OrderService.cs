using BooksmartAPI.Data;
using BooksmartAPI.DTOs.Order;
using BooksmartAPI.Hubs;
using BooksmartAPI.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace BooksmartAPI.Services
{
    public class OrderService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly StoreHub _storeHub;
        public OrderService(UnitOfWork unitOfWork, UserManager<User> userManager, StoreHub storeHub)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _storeHub = storeHub;
        }

        public async Task<bool> Create(CreateOrderDTO createOrderDTO, string email)
        {
            Order order = new();

            foreach (var item in createOrderDTO.productBarCodes) {
                Product product = await _unitOfWork.Products.GetWithAllRelations(item);
                if (product is null) return false;

                OrderProduct orderProduct = new() { BarCode = product.BarCode };

                order.Products.Add(orderProduct);

                await _unitOfWork.Products.DeleteAsync(product);
                await _storeHub.SendUpdate(product);
            }

            order.User = await _userManager.FindByEmailAsync(email);

            await _unitOfWork.Orders.AddAsync(order);

            return true;
        } 
    }
}
