using BooksmartAPI.Data;
using BooksmartAPI.DTOs.Product;
using BooksmartAPI.Models;
using BooksmartAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksmartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ProductService _productService;

        public ProductController(UnitOfWork unitOfWork, ProductService productService)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Get(string barcode)
        {
            GetProductDTO getProductDTO = await _productService.GetDTO(barcode);
            return Ok(getProductDTO);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateExisting(string Barcode)
        {
            await _productService.CreateWithBarcode(Barcode);
            await _unitOfWork.SaveChangesAsync();

            return Created();
        }        
        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateNew(CreateProductDTO createProductDTO)
        {
            await _productService.CreateNew(createProductDTO);
            await _unitOfWork.SaveChangesAsync();

            return Created();
        }

/*        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update()
        {
            return Ok();
        }*/

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await _unitOfWork.Products.DeleteAsync(Id);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
