namespace RSMEnterpriseIntegrationsAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RSMEnterpriseIntegrationsAPI.Application.DTOs.Department;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces.Department;

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentsController(IDepartmentService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            return Ok(await _service.GetDepartmentById(id));
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteDepartment(id);

            return NoContent();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateDepartmentDto dto)
        {
            int id = await _service.CreateDepartment(dto);

            return CreatedAtAction(nameof(Get), new { id }, new { departmentId = id });
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateDepartmentDto dto)
        {
            await _service.UpdateDepartment(dto);

            return NoContent();
        }
    }
}
