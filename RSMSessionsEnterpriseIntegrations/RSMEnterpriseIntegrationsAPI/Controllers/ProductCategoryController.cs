namespace RSMEnterpriseIntegrationsAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RSMEnterpriseIntegrationsAPI.Application.DTOs.ProductCategory;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces.ProductCategory;

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _service;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _service = productCategoryService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAllProductCategories());
        }

        [HttpGet("Get/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.GetProductCategoryById(id));
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteProductCategory(id);

            return NoContent();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateProductCategoryDto dto)
        {
            int id = await _service.CreateProductCategory(dto);

            return CreatedAtAction(nameof(Get), new { id }, new { productCategoryId = id });
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateProductCategoryDto dto)
        {
            await _service.UpdateProductCategory(dto);

            return NoContent();
        }
    }
}
