namespace RSMEnterpriseIntegrationsAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RSMEnterpriseIntegrationsAPI.Application.DTOs.Product;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces.Product;

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService productService)
        {
            _service = productService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAllProducts());
        }

        [HttpGet("Get/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.GetProductById(id));
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteProduct(id);

            return NoContent();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateProudctDto dto)
        {
            int id = await _service.CreateProduct(dto);

            return CreatedAtAction(nameof(Get), new { id }, new { productId = id }); ;
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateProductDto dto)
        {
            await _service.UpdateProduct(dto);

            return NoContent();
        }
    }
}
