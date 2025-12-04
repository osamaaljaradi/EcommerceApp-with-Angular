using AutoMapper;
using ECOM.CORE.DTO;
using ECOM.CORE.Entites.Product;

namespace ECOM.API.Mapping
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductDTO>
                ().ForMember(x => x.CategoryName,
                op => op.MapFrom(src => src.Category.Name)).ReverseMap();
            CreateMap<Photo,CORE.DTO.PhotoDTO>().ReverseMap();
            CreateMap<AddProductDTO, Product>()
                .ForMember(m=>m.Photos,op=>op.Ignore())
                .ReverseMap();
            CreateMap<UpdateProductDTO, Product>()
                .ForMember(m => m.Photos, op => op.Ignore())
                .ReverseMap();
        }
    }
}
