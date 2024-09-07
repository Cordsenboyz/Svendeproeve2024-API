using BooksmartAPI.Data;
using BooksmartAPI.DTOs.Order;
using BooksmartAPI.Models;
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

        public OrderController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetPersonal()
        {
            string? email = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            IEnumerable<Order> orders = await _unitOfWork.Orders.GetOrdersByEmail(email);

            IEnumerable<GetOrderDTO> ordersDTO = orders.Adapt<IEnumerable<GetOrderDTO>>();

            return Ok(ordersDTO);
        }
    }
}
