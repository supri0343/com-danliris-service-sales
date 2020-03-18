using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.DOSales;
using Com.Danliris.Service.Sales.Lib.Models.DOSales;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.Danliris.Sales.Test.BussinesLogic.DataUtils.DOSales
{
    public class DOSalesDataUtil : BaseDataUtil<DOSalesFacade, DOSalesModel>
    {
        public DOSalesDataUtil(DOSalesFacade facade) : base(facade)
        {
        }

        public override async Task<DOSalesModel> GetNewData()
        {
            return new DOSalesModel()
            {
                Code = "code",
                AutoIncreament = 1,
                DOSalesNo = "DOSalesNo",
                DOSalesType = "DOSalesType",
                Status = "Status",
                Accepted = false,
                Declined = false,

                //Lokal
                LocalType = "US",
                LocalDate = DateTimeOffset.UtcNow,
                LocalSalesContractId = 1,
                LocalSalesContractNo = "LocalSalesContractNo",
                LocalMaterialId = 1,
                LocalMaterialCode = "LocalMaterialCode",
                LocalMaterialName = "LocalMaterialName",
                LocalMaterialPrice = 100,
                LocalMaterialTags = "LocalMaterialTags",
                LocalMaterialConstructionId = 1,
                LocalMaterialConstructionCode = "LocalMaterialConstructionCode",
                LocalMaterialConstructionName = "LocalMaterialConstructionName",
                //LocalMaterialConstructionRemark = "LocalMaterialConstructionRemark",
                MaterialWidth = "MaterialWidth",
                ColorRequest = "ColorRequest",
                ColorTemplate = "ColorTemplate",
                LocalBuyerId = 1,
                LocalBuyerCode = "LocalBuyerCode",
                LocalBuyerName = "LocalBuyerName",
                LocalBuyerType = "LocalBuyerType",
                LocalBuyerAddress = "LocalBuyerAddress",
                DestinationBuyerName = "DestinationBuyerName",
                DestinationBuyerAddress = "DestinationBuyerAddress",
                SalesName = "SalesName",
                LocalHeadOfStorage = "LocalHeadOfStorage",
                PackingUom = "PCS",
                ImperialUom = "YDS",
                MetricUom = "MTR",
                Disp = 1,
                Op = 1,
                Sc = 1,
                LocalRemark = "LocalRemark",

                //Ekspor
                ExportType = "KKF",
                ExportDate = DateTimeOffset.UtcNow,
                DoneBy = "DoneBy",
                ExportSalesContractId = 1,
                ExportSalesContractNo = "ExportSalesContractNo",
                ExportMaterialConstructionId = 1,
                ExportMaterialConstructionCode = "ExportMaterialConstructionCode",
                ExportMaterialConstructionName = "ExportMaterialConstructionName",
                //ExportMaterialConstructionRemark = "ExportMaterialConstructionRemark",
                ExportBuyerId = 1,
                ExportBuyerCode = "ExportBuyerCode",
                ExportBuyerName = "ExportBuyerName",
                ExportBuyerType = "ExportBuyerType",
                CommodityId = 1,
                CommodityCode = "CommodityCode",
                CommodityName = "CommodityName",
                CommodityDescription = "CommodityDescription",
                PieceLength = "PieceLength",
                OrderQuantity = 1,
                FillEachBale = 1,
                ExportRemark = "ExportRemark",

                DOSalesLocalItems = new List<DOSalesLocalModel>()
                {
                    new DOSalesLocalModel()
                    {
                        ProductionOrderId = 1,
                        ProductionOrderNo = "OrderNo",
                        MaterialId = 1,
                        MaterialCode = "MaterialCode",
                        MaterialName = "MaterialName",
                        MaterialPrice = 1,
                        MaterialTags = "MaterialTags",
                        MaterialConstructionId = 1,
                        MaterialConstructionName = "MaterialConstructionName",
                        MaterialConstructionCode = "MaterialConstructionCode",
                        MaterialConstructionRemark = "MaterialConstructionRemark",
                        MaterialWidth = "MaterialWidth",
                        ConstructionName = "ConstructionName",
                        ColorRequest = "ColorRequest",
                        ColorTemplate = "ColorTemplate",
                        UnitOrCode = "UnitOrCode",
                        TotalPacking = 1,
                        TotalImperial = 1,
                        TotalMetric = 1,
                    }
                }
            };
    }
}
}
