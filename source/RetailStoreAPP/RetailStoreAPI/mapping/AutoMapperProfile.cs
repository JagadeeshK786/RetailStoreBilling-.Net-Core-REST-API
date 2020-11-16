using AutoMapper;
using RetailStoreAPI.Models;
using StoreDAC.Entities;
using StoreServices.ServiceModels;

namespace RetailStoreAPI.configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<Barcode, BarcodeModel>();

            CreateMap<Bill, BillModel>();
            CreateMap<Bill, BillDetailsModel>();
            CreateMap<BillNewModel, Bill>();
            CreateMap<BillItemNewModel, BillItem>();
            CreateMap<BillItemUpdateModel, BillItemUpdateSM>();

            CreateMap<BillItemUpdateModel, BillItem>()
                .ForMember(dest => dest.Sno, opt => opt.MapFrom(src => src.ItemId));

            CreateMap<BillItem, BillItemModel>()                
            .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.Sno))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName));

            CreateMap<Employee, EmployeeModel>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.RoleName));

            CreateMap<ProductCategory, ProductCategoryModel>()
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.CategoryId));
        }
    }
}

