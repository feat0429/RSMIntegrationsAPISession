namespace RSMEnterpriseIntegrationsAPI.Application.Services
{
    using AutoMapper;
    using FluentValidation;
    using RSMEnterpriseIntegrationsAPI.Application.DTOs.ProductCategory;
    using RSMEnterpriseIntegrationsAPI.Application.Exceptions;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces.ProductCategory;
    using RSMEnterpriseIntegrationsAPI.Domain.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IValidator<CreateProductCategoryDto> _createProductCategoryDtoValidator;
        private readonly IValidator<UpdateProductCategoryDto> _updateProductCategoryDtoValidator;
        private readonly IMapper _mapper;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IValidator<CreateProductCategoryDto> createProductCategoryDtoValidator, IValidator<UpdateProductCategoryDto> updateProductCategoryDtoValidator, IMapper mapper)
        {
            _productCategoryRepository = productCategoryRepository;
            _createProductCategoryDtoValidator = createProductCategoryDtoValidator;
            _updateProductCategoryDtoValidator = updateProductCategoryDtoValidator;
            _mapper = mapper;
        }

        public async Task<int> CreateProductCategory(CreateProductCategoryDto productCategoryDto)
        {
            if (productCategoryDto is null)
                throw new BadRequestException("Request body cannot be null.");

            var validationResults = _createProductCategoryDtoValidator.Validate(productCategoryDto);

            if (!validationResults.IsValid)
                throw new ValidationException(validationResults.Errors);

            var productCategory = _mapper.Map<ProductCategory>(productCategoryDto);

            return await _productCategoryRepository.CreateProductCategory(productCategory);
        }

        public async Task<int> DeleteProductCategory(int id)
        {
            if (id <= 0)
                throw new BadRequestException("Id is not valid.");

            var productCategory = await ValidateProductCategoryExistence(id);

            return await _productCategoryRepository.DeleteProductCategory(productCategory);
        }

        public async Task<IEnumerable<GetProductCategoryDto>> GetAllProductCategories()
        {
            var productsCategories = await _productCategoryRepository.GetAllProductCategories();
            var productCategoriesDto = _mapper.Map<List<GetProductCategoryDto>>(productsCategories);

            return productCategoriesDto;
        }

        public async Task<GetProductCategoryDto?> GetProductCategoryById(int id)
        {
            if (id <= 0)
                throw new BadRequestException("ProductCategoryID is not valid");

            var productCategory = await ValidateProductCategoryExistence(id);
            var dto = _mapper.Map<GetProductCategoryDto>(productCategory);

            return dto;
        }

        public async Task<int> UpdateProductCategory(UpdateProductCategoryDto productCategoryDto)
        {
            if (productCategoryDto is null)
                throw new BadRequestException("ProductCategory request cannot be null.");

            var validationResults = _updateProductCategoryDtoValidator.Validate(productCategoryDto);

            if (!validationResults.IsValid)
                throw new ValidationException(validationResults.Errors);

            var productCategory = await ValidateProductCategoryExistence(productCategoryDto.ProductCategoryId);
            productCategory.Name = productCategoryDto.Name;

            return await _productCategoryRepository.UpdateProductCategory(productCategory);
        }

        private async Task<ProductCategory> ValidateProductCategoryExistence(int id)
        {
            var existingProductCategory = await _productCategoryRepository.GetProductCategoryById(id)
                ?? throw new NotFoundException($"ProductCategory with Id: {id} was not found.");

            return existingProductCategory;
        }
    }
}
