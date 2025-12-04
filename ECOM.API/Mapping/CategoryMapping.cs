using AutoMapper;
using ECOM.CORE.DTO;
using ECOM.CORE.Entites.Product;

namespace ECOM.API.Mapping
{
    public class CategoryMapping:Profile
    {
        public CategoryMapping()
        {
            CreateMap<CategoryDTO,Category>().ReverseMap();
            CreateMap<UpdateCategoryDTO, Category>().ReverseMap();
        }
    }
}
