using AutoMapper;
using Com.Danliris.Service.Sales.Lib.Models.DOSales;
using Com.Danliris.Service.Sales.Lib.ViewModels.DOSales;

namespace Com.Danliris.Service.Sales.Lib.AutoMapperProfiles.DOSalesProfiles
{
    public class DOSalesLocalMapper : Profile
    {
        public DOSalesLocalMapper()
        {
            CreateMap<DOSalesLocalModel, DOSalesLocalViewModel>()

                .ForPath(d => d.ProductionOrder.Id, opt => opt.MapFrom(s => s.ProductionOrderId))
                .ForPath(d => d.ProductionOrder.OrderNo, opt => opt.MapFrom(s => s.ProductionOrderNo))
                .ForPath(d => d.ProductionOrder.MaterialWidth, opt => opt.MapFrom(s => s.MaterialWidth))

                .ForPath(d => d.Material.Id, opt => opt.MapFrom(s => s.MaterialId))
                .ForPath(d => d.Material.Code, opt => opt.MapFrom(s => s.MaterialCode))
                .ForPath(d => d.Material.Name, opt => opt.MapFrom(s => s.MaterialName))
                .ForPath(d => d.Material.Price, opt => opt.MapFrom(s => s.MaterialPrice))
                .ForPath(d => d.Material.Tags, opt => opt.MapFrom(s => s.MaterialTags))

                .ForPath(d => d.MaterialConstruction.Id, opt => opt.MapFrom(s => s.MaterialConstructionId))
                .ForPath(d => d.MaterialConstruction.Name, opt => opt.MapFrom(s => s.MaterialConstructionName))
                .ForPath(d => d.MaterialConstruction.Code, opt => opt.MapFrom(s => s.MaterialConstructionCode))
                .ForPath(d => d.MaterialConstruction.Remark, opt => opt.MapFrom(s => s.MaterialConstructionRemark))

                //.ForPath(d => d.Unit.Id, opt => opt.MapFrom(s => s.UnitId))
                //.ForPath(d => d.Unit.Name, opt => opt.MapFrom(s => s.UnitName))

                .ReverseMap();
        }
    }
}
