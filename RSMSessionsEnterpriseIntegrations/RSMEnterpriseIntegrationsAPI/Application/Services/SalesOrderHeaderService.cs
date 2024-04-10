namespace RSMEnterpriseIntegrationsAPI.Application.Services
{
    using AutoMapper;
    using FluentValidation;
    using RSMEnterpriseIntegrationsAPI.Application.DTOs.PagedList;
    using RSMEnterpriseIntegrationsAPI.Application.DTOs.SalesOrderHeader;
    using RSMEnterpriseIntegrationsAPI.Application.Exceptions;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces.SalesOrderHeader;
    using RSMEnterpriseIntegrationsAPI.Domain.Models;
    using System.Threading.Tasks;

    public class SalesOrderHeaderService : ISalesOrderHeaderService
    {
        private readonly ISalesOrderHeaderRepository _salesOrderHeaderRepository;
        private readonly IValidator<CreateSalesOrderHeaderDto> _createSalesOrderHeaderDtoValidator;
        private readonly IValidator<UpdateSalesOrderHeaderDto> _updateSalesOrderHeaderDtoValidator;
        private readonly IMapper _mapper;

        public SalesOrderHeaderService(ISalesOrderHeaderRepository salesOrderHeaderRepository, IValidator<CreateSalesOrderHeaderDto> createSalesOrderHeaderDtoValidator, IValidator<UpdateSalesOrderHeaderDto> updateSalesOrderHeaderDtoValidator, IMapper mapper)
        {
            _salesOrderHeaderRepository = salesOrderHeaderRepository;
            _createSalesOrderHeaderDtoValidator = createSalesOrderHeaderDtoValidator;
            _updateSalesOrderHeaderDtoValidator = updateSalesOrderHeaderDtoValidator;
            _mapper = mapper;
        }

        public async Task<int> CreateSalesOrderHeader(CreateSalesOrderHeaderDto salesOrderHeaderDto)
        {
            if (salesOrderHeaderDto is null)
                throw new BadRequestException("Request body cannot be null.");

            var validationResults = _createSalesOrderHeaderDtoValidator.Validate(salesOrderHeaderDto);

            if (!validationResults.IsValid)
                throw new ValidationException(validationResults.Errors);

            var salesOrderHeader = _mapper.Map<SalesOrderHeader>(salesOrderHeaderDto);

            return await _salesOrderHeaderRepository.CreateSalesOrderHeader(salesOrderHeader);
        }

        public async Task<int> DeleteSalesOrderHeader(int id)
        {
            if (id <= 0)
                throw new BadRequestException("Id is not valid.");

            var salesOrderHeader = await ValidateSalesOrderHeaderExistence(id);


            return await _salesOrderHeaderRepository.DeleteSalesOrderHeader(salesOrderHeader);
        }

        public async Task<GetPagedListDto<GetSalesOrderHeaderDto>> GetPaginatedSalesOrderHeaders(int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
                throw new BadHttpRequestException("Pagination parameters must be greater than 0.");

            var salesOrderHeadersPagination = await _salesOrderHeaderRepository.GetPaginatedSalesOrderHeaders(page, pageSize);

            var salesOrderHeadersPaginationDto = _mapper.Map<GetPagedListDto<GetSalesOrderHeaderDto>>(salesOrderHeadersPagination);

            return salesOrderHeadersPaginationDto;
        }

        public async Task<GetSalesOrderHeaderDto?> GetSalesOrderHeaderById(int id)
        {
            if (id <= 0)
                throw new BadRequestException("SalesOrderID is not valid");

            var salesOrderHeader = await ValidateSalesOrderHeaderExistence(id);

            var dto = _mapper.Map<GetSalesOrderHeaderDto>(salesOrderHeader);


            return dto;
        }

        public async Task<int> UpdateSalesOrderHeader(UpdateSalesOrderHeaderDto salesOrderHeaderDto)
        {
            if (salesOrderHeaderDto is null)
                throw new BadRequestException("SalesOrder info is not valid.");

            var validationResults = _updateSalesOrderHeaderDtoValidator.Validate(salesOrderHeaderDto);

            if (!validationResults.IsValid)
                throw new ValidationException(validationResults.Errors);

            var salesOrderHeader = await ValidateSalesOrderHeaderExistence(salesOrderHeaderDto.SalesOrderId);
            salesOrderHeader.SalesOrderId = salesOrderHeaderDto.SalesOrderId;
            salesOrderHeader.OrderDate = salesOrderHeaderDto.OrderDate;
            salesOrderHeader.DueDate = salesOrderHeaderDto.DueDate;
            salesOrderHeader.ShipDate = salesOrderHeaderDto.ShipDate;
            salesOrderHeader.Status = salesOrderHeaderDto.Status;
            salesOrderHeader.SubTotal = salesOrderHeaderDto.SubTotal;
            salesOrderHeader.TaxAmt = salesOrderHeaderDto.TaxAmt;
            salesOrderHeader.Freight = salesOrderHeaderDto.Freight;
            salesOrderHeader.Comment = salesOrderHeaderDto.Comment;

            return await _salesOrderHeaderRepository.UpdateSalesOrderHeader(salesOrderHeader);
        }

        private async Task<SalesOrderHeader> ValidateSalesOrderHeaderExistence(int id)
        {
            var existingSalesOrderHeader = await _salesOrderHeaderRepository.GetSalesOrderHeaderById(id)
                ?? throw new NotFoundException($"SalesOrderHeader record with Id: {id} was not found.");

            return existingSalesOrderHeader;
        }
    }
}
