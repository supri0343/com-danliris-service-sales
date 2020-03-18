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
                .ForPath(d => d.LocalSalesContract.SalesContractNo, opt => opt.MapFrom(s => s.LocalSalesContractNo))
                .ForPath(d => d.LocalSalesContract.MaterialWidth, opt => opt.MapFrom(s => s.MaterialWidth))

                .ForPath(d => d.LocalSalesContract.Buyer.Id, opt => opt.MapFrom(s => s.LocalBuyerId))
                .ForPath(d => d.LocalSalesContract.Buyer.Code, opt => opt.MapFrom(s => s.LocalBuyerCode))
                .ForPath(d => d.LocalSalesContract.Buyer.Name, opt => opt.MapFrom(s => s.LocalBuyerName))
                .ForPath(d => d.LocalSalesContract.Buyer.Type, opt => opt.MapFrom(s => s.LocalBuyerType))
                .ForPath(d => d.LocalSalesContract.Buyer.Address, opt => opt.MapFrom(s => s.LocalBuyerAddress))

                .ForPath(d => d.LocalMaterial.Id, opt => opt.MapFrom(s => s.LocalMaterialId))
                .ForPath(d => d.LocalMaterial.Code, opt => opt.MapFrom(s => s.LocalMaterialCode))
                .ForPath(d => d.LocalMaterial.Name, opt => opt.MapFrom(s => s.LocalMaterialName))

                .ForPath(d => d.LocalMaterialConstruction.Id, opt => opt.MapFrom(s => s.LocalMaterialConstructionId))
                .ForPath(d => d.LocalMaterialConstruction.Code, opt => opt.MapFrom(s => s.LocalMaterialConstructionCode))
                .ForPath(d => d.LocalMaterialConstruction.Name, opt => opt.MapFrom(s => s.LocalMaterialConstructionName))
                //.ForPath(d => d.LocalMaterialConstruction.Remark, opt => opt.MapFrom(s => s.LocalMaterialConstructionRemark))

                //.ForPath(d => d.LocalSalesContract.Sales._id, opt => opt.MapFrom(s => s.SalesId))
                //.ForPath(d => d.LocalSalesContract.Sales.FirstName, opt => opt.MapFrom(s => s.SalesFirstName))
                //.ForPath(d => d.LocalSalesContract.Sales.LastName, opt => opt.MapFrom(s => s.SalesLastName))

                //Ekspor
                .ForPath(d => d.ExportSalesContract.Id, opt => opt.MapFrom(s => s.ExportSalesContractId))
                .ForPath(d => d.ExportSalesContract.SalesContractNo, opt => opt.MapFrom(s => s.ExportSalesContractNo))
                .ForPath(d => d.ExportSalesContract.OrderQuantity, opt => opt.MapFrom(s => s.OrderQuantity))
                .ForPath(d => d.ExportSalesContract.PieceLength, opt => opt.MapFrom(s => s.PieceLength))

                .ForPath(d => d.ExportSalesContract.MaterialConstruction.Id, opt => opt.MapFrom(s => s.ExportMaterialConstructionId))
                .ForPath(d => d.ExportSalesContract.MaterialConstruction.Name, opt => opt.MapFrom(s => s.ExportMaterialConstructionCode))
                .ForPath(d => d.ExportSalesContract.MaterialConstruction.Code, opt => opt.MapFrom(s => s.ExportMaterialConstructionName))
                //.ForPath(d => d.ExportSalesContract.MaterialConstruction.Remark, opt => opt.MapFrom(s => s.ExportMaterialConstructionRemark))

                .ForPath(d => d.ExportSalesContract.Buyer.Id, opt => opt.MapFrom(s => s.ExportBuyerId))
                .ForPath(d => d.ExportSalesContract.Buyer.Code, opt => opt.MapFrom(s => s.ExportBuyerCode))
                .ForPath(d => d.ExportSalesContract.Buyer.Name, opt => opt.MapFrom(s => s.ExportBuyerName))
                .ForPath(d => d.ExportSalesContract.Buyer.Type, opt => opt.MapFrom(s => s.ExportBuyerType))

                .ForPath(d => d.ExportSalesContract.Commodity.Id, opt => opt.MapFrom(s => s.CommodityId))
                .ForPath(d => d.ExportSalesContract.Commodity.Code, opt => opt.MapFrom(s => s.CommodityCode))
                .ForPath(d => d.ExportSalesContract.Commodity.Name, opt => opt.MapFrom(s => s.CommodityName))
                .ForPath(d => d.ExportSalesContract.CommodityDescription, opt => opt.MapFrom(s => s.CommodityDescription))

                .ReverseMap();
        }
    }
}
