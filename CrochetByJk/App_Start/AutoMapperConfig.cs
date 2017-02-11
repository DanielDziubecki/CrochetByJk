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
                cfg.CreateMap<Product, ProductTileViewModel>()
                    .ForMember(vm => vm.PictureUri, opt => opt
                        .MapFrom(m => m.ProductGallery.Single(e => e.IsMainPhoto).Url));

                cfg.CreateMap<Product, ProductViewModel>()
                    .ForMember(vm => vm.PictureUrls, opt => opt
                        .MapFrom(m => m.ProductGallery.Select(x => x.Url)));
            });
            return config.CreateMapper();
        }
    }
}