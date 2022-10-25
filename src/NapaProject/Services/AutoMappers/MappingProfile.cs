using AutoMapper;
using NapaProject.Models;
using NapaProject.Services.ViewModels.Products;
using NapaProject.Services.ViewModels.Users;

namespace NapaProject.Services.AutoMappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserPatchViewModel>().ReverseMap();
            CreateMap<User, UserCreateViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<Product, ProductCreateViewModel>().ReverseMap();
            CreateMap<Product, ProductPatchViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModelForAdmin>().ReverseMap();
            CreateMap<ProductAudit, ProductAuditViewModel>().ReverseMap();
        }
    }
}
