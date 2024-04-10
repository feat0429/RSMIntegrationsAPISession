namespace RSMEnterpriseIntegrationsAPI.Application.Services
{
    using RSMEnterpriseIntegrationsAPI.Application.DTOs.Department;
    using RSMEnterpriseIntegrationsAPI.Application.Exceptions;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces.Department;
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository repository)
        {
            _departmentRepository = repository;
        }

        public async Task<int> CreateDepartment(CreateDepartmentDto departmentDto)
        {
            if (departmentDto is null 
                || string.IsNullOrWhiteSpace(departmentDto.Name) 
                || string.IsNullOrWhiteSpace(departmentDto.GroupName))
            {
                throw new BadRequestException("Department info is not valid.");
            }

            Department department = new()
            {
                GroupName = departmentDto.GroupName,
                Name = departmentDto.Name,
            };

            return await _departmentRepository.CreateDepartment(department);
        }

        public async Task<int> DeleteDepartment(int id)
        {
            if(id <= 0)
            {
                throw new BadRequestException("Id is not valid.");
            }
            var department = await ValidateDepartmentExistence(id);
            return await _departmentRepository.DeleteDepartment(department);
        }

        public async Task<IEnumerable<GetDepartmentDto>> GetAll()
        {
            var departments = await _departmentRepository.GetAllDepartments();
            List<GetDepartmentDto> departmentsDto = [];

            foreach (var department in departments)
            {
                GetDepartmentDto dto = new()
                {
                    Name = department.Name,
                    GroupName = department.GroupName,
                    DepartmentId = department.DepartmentId
                };

                departmentsDto.Add(dto);
            }
            
            /*departments.ToList().ForEach(department =>
            {
                GetDepartmentDto dto = new()
                {
                    DepartmentId = department.DepartmentId,
                    Name = department.Name,
                    GroupName = department.GroupName
                };
                departmentsDto.Add(dto);
            });*/

            return departmentsDto; 
        }

        public async Task<GetDepartmentDto?> GetDepartmentById(int id)
        {
            if(id <= 0)
            {
                throw new BadRequestException("DepartmentId is not valid");
            }

            var department = await ValidateDepartmentExistence(id);
            
            GetDepartmentDto dto = new()
            {
                DepartmentId = department.DepartmentId,
                Name = department.Name,
                GroupName = department.GroupName
            };
            return dto;
        }

        public async Task<int> UpdateDepartment(UpdateDepartmentDto departmentDto)
        {
            if(departmentDto is null)
            {
                throw new BadRequestException("Department info is not valid.");
            }
            var department = await ValidateDepartmentExistence(departmentDto.DepartmentId);
            
            department.Name = string.IsNullOrWhiteSpace(departmentDto.Name) ? department.Name : departmentDto.Name;
            department.GroupName = string.IsNullOrWhiteSpace(departmentDto.GroupName) ? department.GroupName : departmentDto.GroupName;
           
            return await _departmentRepository.UpdateDepartment(department);
        }

        private async Task<Department> ValidateDepartmentExistence(int id)
        {
            var existingDepartment = await _departmentRepository.GetDepartmentById(id) 
                ?? throw new NotFoundException($"Department with Id: {id} was not found.");

            return existingDepartment;
        }

    }
}
