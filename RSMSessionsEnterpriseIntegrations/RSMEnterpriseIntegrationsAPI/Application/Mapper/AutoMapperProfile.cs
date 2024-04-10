namespace RSMEnterpriseIntegrationsAPI.Application.Mappers
{
    using AutoMapper;
    using RSMEnterpriseIntegrationsAPI.Application.DTOs.Department;
    using RSMEnterpriseIntegrationsAPI.Application.DTOs.PagedList;
    using RSMEnterpriseIntegrationsAPI.Application.DTOs.Product;
    using RSMEnterpriseIntegrationsAPI.Application.DTOs.ProductCategory;
    using RSMEnterpriseIntegrationsAPI.Application.DTOs.SalesOrderHeader;
    using RSMEnterpriseIntegrationsAPI.Domain.Models;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region SalesOrderHeader
            CreateMap<SalesOrderHeader, GetSalesOrderHeaderDto>().ReverseMap();
            CreateMap<PagedList<SalesOrderHeader>, GetPagedListDto<GetSalesOrderHeaderDto>>().ReverseMap();
            CreateMap<CreateSalesOrderHeaderDto, SalesOrderHeader>().ReverseMap();
            CreateMap<UpdateSalesOrderHeaderDto, SalesOrderHeader>().ReverseMap();
            #endregion

            #region Product
            CreateMap<Product, GetProductDto>().ReverseMap();
            CreateMap<CreateProudctDto, Product>().ReverseMap();
            CreateMap<UpdateProductDto, Product>().ReverseMap();
            #endregion

            #region ProductCategory
            CreateMap<ProductCategory, GetProductCategoryDto>().ReverseMap();
            CreateMap<CreateProductCategoryDto, ProductCategory>().ReverseMap();
            CreateMap<UpdateProductCategoryDto, ProductCategory>().ReverseMap();
            #endregion

            #region Department
            CreateMap<Department, GetDepartmentDto>().ReverseMap();
            CreateMap<CreateDepartmentDto, Department>().ReverseMap();
            CreateMap<UpdateDepartmentDto, Department>().ReverseMap();
            #endregion
        }
    }
}
