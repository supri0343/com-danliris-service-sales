using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.SalesInvoiceExport;
using Com.Danliris.Service.Sales.Lib.Models.SalesInvoiceExport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Sales.Test.BussinesLogic.DataUtils.SalesInvoiceExport
{
    public class SalesInvoiceExportDataUtil : BaseDataUtil<SalesInvoiceExportFacade, SalesInvoiceExportModel>
    {
        public SalesInvoiceExportDataUtil(SalesInvoiceExportFacade facade) : base(facade)
        {
        }

        public override async Task<SalesInvoiceExportModel> GetNewData()
        {
            return new SalesInvoiceExportModel()
            {
                Code = "code",
                AutoIncreament = 1,
                SalesInvoiceNo = "SalesInvoiceNo",
                SalesInvoiceCategory = "DYEINGPRINTING",
                SalesInvoiceType = "L/C",
                SalesInvoiceDate = DateTimeOffset.UtcNow,
                FPType = "Printing",
                BuyerName = "BuyerName",
                BuyerAddress = "BuyerAddress",
                ContractNo = "ContractNo",
                Authorized = "Amumpuni",
                ShippedPer = "ShippedPer",
                SailingDate = DateTimeOffset.UtcNow,
                LetterOfCreditNumber = "LetterOfCreditNumber",
                LCDate = DateTimeOffset.UtcNow,
                BankName = "BankName",
                From = "From",
                To = "To",
                HSCode = "HSCode",
                TermOfPaymentType = "TermOfPaymentType",
                TermOfPaymentRemark = "TermOfPaymentRemark",
                Color = "Color",
                OrderNo = "OrderNo",
                Indent = "Indent",
                QuantityLength = 100,
                CartonNo = "CartonNo",
                ShippingRemark = "ShippingRemark",
                Remark = "Remark",
                SalesInvoiceExportDetails = new List<SalesInvoiceExportDetailModel>()
                {
                    new SalesInvoiceExportDetailModel()
                    {
                        ShippingOutId = 4,
                        BonNo = "BonNo",
                        Description = "Description",
                        GrossWeight = 100,
                        NetWeight = 100,
                        TotalMeas = 100,
                        WeightUom = "KG",
                        TotalUom = "CBM",
                        SalesInvoiceExportItems = new List<SalesInvoiceExportItemModel>()
                        {
                            new SalesInvoiceExportItemModel()
                            {
                                ProductCode = "ProductCode",
                                ProductName = "ProductName",
                                QuantityPacking = 100,
                                PackingUom = "PackingUom",
                                ItemUom = "MTR",
                                QuantityItem = 1,
                                Price = 1,
                                Amount = 1,
                            },
                        }
                    }
                }
            };
        }

        //public async Task<SalesInvoiceExportModel> GetNewData_2()
        //{
        //    return new SalesInvoiceExportModel()
        //    {

        //        Code = "code",
        //        AutoIncreament = 1,
        //        SalesInvoiceNo = "SalesInvoiceNo",
        //        SalesInvoiceCategory = "SPINNING",
        //        SalesInvoiceType = "P.P",
        //        SalesInvoiceDate = DateTimeOffset.UtcNow,
        //        BuyerName = "BuyerName",
        //        BuyerAddress = "BuyerAddress",
        //        Authorized = "Amumpuni",
        //        ShippedPer = "ShippedPer",
        //        SailingDate = DateTimeOffset.UtcNow,
        //        LetterOfCreditNumber = "LetterOfCreditNumber",
        //        HSCode = "HSCode",
        //        TermOfPaymentType = "TermOfPaymentType",
        //        TermOfPaymentRemark = "TermOfPaymentRemark",
        //        Color = "Color",
        //        OrderNo = "OrderNo",
        //        Indent = "Indent",
        //        QuantityLength = 100,
        //        CartonNo = "CartonNo",
        //        ShippingRemark = "ShippingRemark",
        //        Remark = "Remark",
        //        SalesInvoiceExportDetails = new List<SalesInvoiceExportDetailModel>()
        //        {
        //            new SalesInvoiceExportDetailModel()
        //            {
        //                ShippingOutId = 4,
        //                BonNo = "BonNo",
        //                GrossWeight = 100,
        //                NetWeight = 100,
        //                TotalMeas = 100,
        //                WeightUom = "KG",
        //                TotalUom = "CBM",
        //                SalesInvoiceExportItems = new List<SalesInvoiceExportItemModel>()
        //                {
        //                    new SalesInvoiceExportItemModel()
        //                    {
        //                        ProductCode = "ProductCode",
        //                        ProductName = "ProductName",
        //                        PackingUom = "PackingUom",
        //                        ItemUom = "YARD",
        //                        QuantityPacking = 100,
        //                        QuantityItem = 1,
        //                        Price = 1,
        //                        Amount = 1,
        //                    },
        //                }
        //            }
        //        }
        //    };
        //}
    }
}
