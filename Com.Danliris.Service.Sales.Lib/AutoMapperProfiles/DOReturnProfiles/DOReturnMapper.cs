using AutoMapper;
using Com.Danliris.Service.Sales.Lib.Models.DOReturn;
using Com.Danliris.Service.Sales.Lib.ViewModels.DOReturn;

namespace Com.Danliris.Service.Sales.Lib.AutoMapperProfiles.DOReturnProfiles
{
    public class DOReturnMapper : Profile
    {
        public DOReturnMapper()
        {
            CreateMap<DOReturnModel, DOReturnViewModel>()
                
                .ReverseMap();
        }
    }
}
