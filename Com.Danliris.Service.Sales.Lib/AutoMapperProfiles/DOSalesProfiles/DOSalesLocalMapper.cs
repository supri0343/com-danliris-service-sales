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
                .ForPath(d => d.ProductionOrder.FinishingPrintingSalesContract.CostCalculation.ProductionOrderNo, opt => opt.MapFrom(s => s.ProductionOrderNo))

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
