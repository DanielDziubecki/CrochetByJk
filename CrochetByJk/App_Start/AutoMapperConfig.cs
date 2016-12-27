using System.Linq;
using AutoMapper;
using CrochetByJk.Model.Model;
using CrochetByJk.ViewModel;

namespace CrochetByJk
{
    public static class AutoMapperConfig
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductViewModel>()
                    .ForMember(model => model.PictureUri, opt => opt
                    .MapFrom(x => x.ProductGallery.Single(e => e.IsMainPhoto).Uri));
            });
            return config.CreateMapper();
        }
    }
}