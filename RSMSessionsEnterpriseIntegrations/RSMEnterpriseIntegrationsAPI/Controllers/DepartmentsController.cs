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
            return Ok(await _service.DeleteDepartment(id));
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateDepartmentDto dto)
        {
            return Ok(await _service.CreateDepartment(dto));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateDepartmentDto dto)
        {
            return Ok(await _service.UpdateDepartment(dto));
        }
    }
}
