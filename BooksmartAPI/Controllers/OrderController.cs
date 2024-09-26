using BooksmartAPI.Data;
using BooksmartAPI.DTOs.Order;
using BooksmartAPI.Models;
using BooksmartAPI.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BooksmartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly OrderService _orderService;

        public OrderController(UnitOfWork unitOfWork, OrderService orderService)
        {
            _unitOfWork = unitOfWork;
            _orderService = orderService;
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetOwn()
        {
            string? email = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            IEnumerable<Order> orders = await _unitOfWork.Orders.GetOrdersByEmail(email);

            IEnumerable<GetOrderDTO> ordersDTO = orders.Adapt<IEnumerable<GetOrderDTO>>();

            return Ok(ordersDTO);
        }

        [Authorize]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(CreateOrderDTO createOrderDTO)
        {
            string? email = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;

            bool result = await _orderService.Create(createOrderDTO, email);
            if (!result) BadRequest("No Product with that barcode exists!");
            await _unitOfWork.SaveChangesAsync();

            return Created();
        }
    }
}
