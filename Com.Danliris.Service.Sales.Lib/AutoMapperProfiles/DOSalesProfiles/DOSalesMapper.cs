using AutoMapper;
using Com.Danliris.Service.Sales.Lib.Models.DOSales;
using Com.Danliris.Service.Sales.Lib.ViewModels.DOSales;

namespace Com.Danliris.Service.Sales.Lib.AutoMapperProfiles.DOSalesProfiles
{
    public class DOSalesMapper : Profile
    {
        public DOSalesMapper()
        {
            CreateMap<DOSalesModel, DOSalesViewModel>()

                //Local
                .ForPath(d => d.LocalSalesContract.Id, opt => opt.MapFrom(s => s.LocalSalesContractId))
                .ForPath(d => d.LocalSalesContract.CostCalculation.PreSalesContract.No, opt => opt.MapFrom(s => s.LocalSalesContractNo))
                .ForPath(d => d.LocalSalesContract.CostCalculation.PreSalesContract.Buyer.Id, opt => opt.MapFrom(s => s.LocalBuyerId))

                .ForPath(d => d.LocalSalesContract.CostCalculation.PreSalesContract.Buyer.Code, opt => opt.MapFrom(s => s.LocalBuyerCode))
                .ForPath(d => d.LocalSalesContract.CostCalculation.PreSalesContract.Buyer.Name, opt => opt.MapFrom(s => s.LocalBuyerName))
                .ForPath(d => d.LocalSalesContract.CostCalculation.PreSalesContract.Buyer.Type, opt => opt.MapFrom(s => s.LocalBuyerType))

                .ForPath(d => d.LocalSalesContract.CostCalculation.Sales._id, opt => opt.MapFrom(s => s.SalesId))
                .ForPath(d => d.LocalSalesContract.CostCalculation.Sales.FirstName, opt => opt.MapFrom(s => s.SalesFirstName))
                .ForPath(d => d.LocalSalesContract.CostCalculation.Sales.LastName, opt => opt.MapFrom(s => s.SalesLastName))

                //Ekspor
                .ForPath(d => d.ExportSalesContract.Id, opt => opt.MapFrom(s => s.ExportSalesContractId))
                .ForPath(d => d.ExportSalesContract.CostCalculation.PreSalesContract.No, opt => opt.MapFrom(s => s.ExportSalesContractNo))

                .ForPath(d => d.ExportSalesContract.MaterialConstruction.Id, opt => opt.MapFrom(s => s.MaterialConstructionId))
                .ForPath(d => d.ExportSalesContract.MaterialConstruction.Name, opt => opt.MapFrom(s => s.MaterialConstructionCode))
                .ForPath(d => d.ExportSalesContract.MaterialConstruction.Code, opt => opt.MapFrom(s => s.MaterialConstructionName))
                .ForPath(d => d.ExportSalesContract.MaterialConstruction.Remark, opt => opt.MapFrom(s => s.MaterialConstructionRemark))

                .ForPath(d => d.ExportSalesContract.CostCalculation.PreSalesContract.Buyer.Id, opt => opt.MapFrom(s => s.ExportBuyerId))
                .ForPath(d => d.ExportSalesContract.CostCalculation.PreSalesContract.Buyer.Code, opt => opt.MapFrom(s => s.ExportBuyerCode))
                .ForPath(d => d.ExportSalesContract.CostCalculation.PreSalesContract.Buyer.Name, opt => opt.MapFrom(s => s.ExportBuyerName))
                .ForPath(d => d.ExportSalesContract.CostCalculation.PreSalesContract.Buyer.Type, opt => opt.MapFrom(s => s.ExportBuyerType))

                .ForPath(d => d.ExportSalesContract.Commodity.Id, opt => opt.MapFrom(s => s.CommodityId))
                .ForPath(d => d.ExportSalesContract.Commodity.Code, opt => opt.MapFrom(s => s.CommodityCode))
                .ForPath(d => d.ExportSalesContract.Commodity.Name, opt => opt.MapFrom(s => s.CommodityName))
                .ForPath(d => d.ExportSalesContract.CommodityDescription, opt => opt.MapFrom(s => s.CommodityDescription))

                .ReverseMap();
        }
    }
}
