using AutoMapper;
using CMSNews.Model.Models;
using CMSNews.Models.ViewModels;


namespace CMSNews.Mappings
{
    public class MappingProfile: Profile
    {
        public static IMapper mapper;
        public MappingProfile()
        {
            CreateMap<tblNewsGroup, NewsGroupViewModel>().ReverseMap();
        }
        //public static void ConfigureMapping()
        //{
        //    MapperConfiguration config = new MapperConfiguration(t => {
        //        t.CreateMap<tblNewsGroup,NewsGroupViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
        //    });
        //    mapper = config.CreateMapper();
        //}
    }
}
