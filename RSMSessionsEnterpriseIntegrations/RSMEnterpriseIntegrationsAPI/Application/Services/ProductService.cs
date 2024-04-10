namespace RSMEnterpriseIntegrationsAPI.Application.Services
{
    using AutoMapper;
    using FluentValidation;
    using RSMEnterpriseIntegrationsAPI.Application.DTOs.Product;
    using RSMEnterpriseIntegrationsAPI.Application.Exceptions;
    using RSMEnterpriseIntegrationsAPI.Domain.Interfaces.Product;
    using RSMEnterpriseIntegrationsAPI.Domain.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidator<CreateProudctDto> _createProductDtoValidator;
        private readonly IValidator<UpdateProductDto> _updateProductDtoValidator;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IValidator<CreateProudctDto> createProductDtoValidator, IValidator<UpdateProductDto> updateProductDtoValidator, IMapper mapper)
        {
            _productRepository = productRepository;
            _createProductDtoValidator = createProductDtoValidator;
            _updateProductDtoValidator = updateProductDtoValidator;
            _mapper = mapper;
        }

        public async Task<int> CreateProduct(CreateProudctDto productDto)
        {
            if (productDto is null)
                throw new BadRequestException("Request body cannot be null.");

            var validationResults = _createProductDtoValidator.Validate(productDto);

            if (!validationResults.IsValid)
                throw new ValidationException(validationResults.Errors);

            var product = _mapper.Map<Product>(productDto);

            return await _productRepository.CreateProduct(product);
        }

        public async Task<int> DeleteProduct(int id)
        {
            if (id <= 0)
                throw new BadRequestException("Id is not valid.");

            var product = await ValidateProductExistence(id);
            /*Here I would validate if requested record has related records from other tables 
            to avoid conflicts with the database before deleting, but I would have to add more
            models than allowed. Hence, only new added products without related records can be deleted.*/

            return await _productRepository.DeleteProduct(product);
        }

        public async Task<IEnumerable<GetProductDto>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProducts();
            var productsDto = _mapper.Map<List<GetProductDto>>(products); ;

            return productsDto;
        }

        public async Task<GetProductDto?> GetProductById(int id)
        {
            if (id <= 0)
                throw new BadRequestException("ProductID is not valid");

            var product = await ValidateProductExistence(id);

            var dto = _mapper.Map<GetProductDto>(product);

            return dto;
        }

        public async Task<int> UpdateProduct(UpdateProductDto productDto)
        {
            if (productDto is null)
                throw new BadRequestException("Product info is not valid.");

            var validationResults = _updateProductDtoValidator.Validate(productDto);

            if (!validationResults.IsValid)
                throw new ValidationException(validationResults.Errors);

            var product = await ValidateProductExistence(productDto.ProductId);
            product.Name = productDto.Name;
            product.ProductNumber = productDto.ProductNumber;
            product.SafetyStockLevel = productDto.SafetyStockLevel;
            product.ReorderPoint = productDto.ReorderPoint;
            product.StandardCost = productDto.StandardCost;
            product.ListPrice = productDto.ListPrice;
            product.DaysToManufacture = productDto.DaysToManufacture;
            product.SellStartDate = productDto.SellStartDate;

            return await _productRepository.UpdateProduct(product);
        }

        private async Task<Product> ValidateProductExistence(int id)
        {
            var existingProduct = await _productRepository.GetProductById(id)
                ?? throw new NotFoundException($"Product with Id: {id} was not found.");

            return existingProduct;
        }
    }
}
