namespace RSMEnterpriseIntegrationsAPI.Application.Services
{
    using AutoMapper;
    using FluentValidation;
    using RSMEnterpriseIntegrationsAPI.Application.DTOs.Department;
    using RSMEnterpriseIntegrationsAPI.Application.Exceptions;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces.Department;
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IValidator<CreateDepartmentDto> _createDepartmentDtoValidator;
        private readonly IValidator<UpdateDepartmentDto> _updateDepartmentDtoValidator;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository repository, IValidator<CreateDepartmentDto> createDepartmentDtoValidator, IValidator<UpdateDepartmentDto> updateDepartmentDtoValidator, IMapper mapper)
        {
            _departmentRepository = repository;
            _createDepartmentDtoValidator = createDepartmentDtoValidator;
            _updateDepartmentDtoValidator = updateDepartmentDtoValidator;
            _mapper = mapper;
        }

        public async Task<int> CreateDepartment(CreateDepartmentDto departmentDto)
        {
            if (departmentDto is null)
            {
                throw new BadRequestException("Department info is not valid.");
            }

            var validationResults = _createDepartmentDtoValidator.Validate(departmentDto);

            if (!validationResults.IsValid)
                throw new ValidationException(validationResults.Errors);

            var department = _mapper.Map<Department>(departmentDto);

            return await _departmentRepository.CreateDepartment(department);
        }

        public async Task<int> DeleteDepartment(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id is not valid.");
            }
            var department = await ValidateDepartmentExistence(id);

            return await _departmentRepository.DeleteDepartment(department);
        }

        public async Task<IEnumerable<GetDepartmentDto>> GetAll()
        {
            var departments = await _departmentRepository.GetAllDepartments();
            var departmentsDto = _mapper.Map<List<GetDepartmentDto>>(departments);

            return departmentsDto;
        }

        public async Task<GetDepartmentDto?> GetDepartmentById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("DepartmentId is not valid");
            }

            var department = await ValidateDepartmentExistence(id);
            var dto = _mapper.Map<GetDepartmentDto>(department);

            return dto;
        }

        public async Task<int> UpdateDepartment(UpdateDepartmentDto departmentDto)
        {
            if (departmentDto is null)
            {
                throw new BadRequestException("Department info is not valid.");
            }

            var validationResults = _updateDepartmentDtoValidator.Validate(departmentDto);

            if (!validationResults.IsValid)
                throw new ValidationException(validationResults.Errors);

            var department = await ValidateDepartmentExistence(departmentDto.DepartmentId);

            department.Name = departmentDto.Name;
            department.GroupName = departmentDto.GroupName;

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
