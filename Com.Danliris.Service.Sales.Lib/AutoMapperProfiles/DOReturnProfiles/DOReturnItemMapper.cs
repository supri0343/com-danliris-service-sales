using AutoMapper;
using Com.Danliris.Service.Sales.Lib.Models.DOReturn;
using Com.Danliris.Service.Sales.Lib.ViewModels.DOReturn;

namespace Com.Danliris.Service.Sales.Lib.AutoMapperProfiles.DOReturnProfiles
{
    public class DOReturnItemMapper : Profile
    {
        public DOReturnItemMapper()
        {
            CreateMap<DOReturnItemModel, DOReturnItemViewModel>()

                .ForPath(d => d.Uom.Id, opt => opt.MapFrom(s => s.UomId))
                .ForPath(d => d.Uom.Unit, opt => opt.MapFrom(s => s.UomUnit))

                .ReverseMap();
        }
    }
}
