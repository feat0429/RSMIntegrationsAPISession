namespace RSMEnterpriseIntegrationsAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RSMEnterpriseIntegrationsAPI.Application.DTOs.SalesOrderHeader;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces.SalesOrderHeader;

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SalesOrderHeaderController : ControllerBase
    {
        private readonly ISalesOrderHeaderService _service;

        public SalesOrderHeaderController(ISalesOrderHeaderService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get([FromQuery] int page, int pageSize)
        {
            return Ok(await _service.GetPaginatedSalesOrderHeaders(page, pageSize));
        }

        [HttpGet("Get/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.GetSalesOrderHeaderById(id));
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteSalesOrderHeader(id);

            return NoContent();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateSalesOrderHeaderDto dto)
        {
            int id = await _service.CreateSalesOrderHeader(dto);

            return CreatedAtAction(nameof(Get), new { id }, new { salesOrderId = id }); ;
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateSalesOrderHeaderDto dto)
        {
            await _service.UpdateSalesOrderHeader(dto);

            return NoContent();
        }
    }
}
