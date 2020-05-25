using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.SalesInvoice;
using Com.Danliris.Service.Sales.Lib.Models.SalesInvoice;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.Danliris.Sales.Test.BussinesLogic.DataUtils.SalesInvoice
{
    public class SalesInvoiceDataUtil : BaseDataUtil<SalesInvoiceFacade, SalesInvoiceModel>
    {
        public SalesInvoiceDataUtil(SalesInvoiceFacade facade) : base(facade)
        {
        }

        public override async Task<SalesInvoiceModel> GetNewData()
        {
            return new SalesInvoiceModel()
            {
                Code = "code",
                AutoIncreament = 1,
                SalesInvoiceNo = "SalesInvoiceNo",
                SalesInvoiceType = "BPF",
                SalesInvoiceDate = DateTimeOffset.UtcNow,
                DueDate = DateTimeOffset.UtcNow.AddDays(-2),
                DeliveryOrderNo = "DeliveryOrderNo",
                BuyerId = 1,
                BuyerName = "BuyerName",
                BuyerCode = "BuyerCode",
                BuyerAddress = "BuyerAddress",
                BuyerNPWP = "BuyerNPWP",
                BuyerNIK = "BuyerNIK",
                CurrencyId = 1,
                CurrencyCode = "IDR",
                CurrencySymbol = "Rp",
                CurrencyRate = 14000,
                PaymentType = "Meter",
                VatType = "PPN Kawasan Berikat",
                Remark = "Remark",
                TotalPayment = 100,
                TotalPaid = 0,

                SalesInvoiceDetails = new List<SalesInvoiceDetailModel>()
                {
                    new SalesInvoiceDetailModel()
                    {
                        ShipmentDocumentId = 1,
                        ShipmentDocumentCode = "ShipmentDocumentCode",
                        SalesInvoiceItems = new List<SalesInvoiceItemModel>()
                        {
                            new SalesInvoiceItemModel()
                            {
                                ProductCode = "ProductCode",
                                ProductName = "ProductName",
                                Quantity = "Quantity",
                                PackingUom = "PackingUom",
                                UomId = 1,
                                UomUnit = "PCS",
                                Total = 1,
                                Price = 1,
                                Amount = 1,
                            },
                        }
                    }
                }
            };
        }

        public async Task<SalesInvoiceModel> GetNewData_SalesInvoiceType_Is_BLL()
        {
            return new SalesInvoiceModel()
            {
                Code = "code",
                AutoIncreament = 1,
                SalesInvoiceNo = "SalesInvoiceNo",
                SalesInvoiceType = "BLL",
                SalesInvoiceDate = DateTimeOffset.UtcNow,
                DueDate = DateTimeOffset.UtcNow.AddDays(-2),
                DeliveryOrderNo = "DeliveryOrderNo",
                BuyerId = 1,
                BuyerName = "BuyerName",
                BuyerCode = "BuyerCode",
                BuyerAddress = "BuyerAddress",
                BuyerNPWP = "BuyerNPWP",
                BuyerNIK = "BuyerNIK",
                CurrencyId = 1,
                CurrencyCode = "IDR",
                CurrencySymbol = "Rp",
                CurrencyRate = 14000,
                PaymentType = "Meter",
                VatType = "PPN Kawasan Berikat",
                Remark = "Remark",
                TotalPayment = 100,
                TotalPaid = 0,

                SalesInvoiceDetails = new List<SalesInvoiceDetailModel>()
                {
                    new SalesInvoiceDetailModel()
                    {
                        ShipmentDocumentId = 1,
                        ShipmentDocumentCode = "ShipmentDocumentCode",
                        SalesInvoiceItems = new List<SalesInvoiceItemModel>()
                        {
                            new SalesInvoiceItemModel()
                            {
                                ProductCode = "ProductCode",
                                ProductName = "ProductName",
                                Quantity = "Quantity",
                                PackingUom = "PackingUom",
                                UomId = 1,
                                UomUnit = "PCS",
                                Total = 1,
                                Price = 1,
                                Amount = 1,
                            },
                        }
                    }
                }
            };
        }

        public async Task<SalesInvoiceModel> GetNewData_SalesInvoiceType_Is_BON()
        {
            return new SalesInvoiceModel()
            {
                Code = "code",
                AutoIncreament = 1,
                SalesInvoiceNo = "SalesInvoiceNo",
                SalesInvoiceType = "BON",
                SalesInvoiceDate = DateTimeOffset.UtcNow,
                DueDate = DateTimeOffset.UtcNow.AddDays(-2),
                DeliveryOrderNo = "DeliveryOrderNo",
                BuyerId = 1,
                BuyerName = "BuyerName",
                BuyerCode = "BuyerCode",
                BuyerAddress = "BuyerAddress",
                BuyerNPWP = "BuyerNPWP",
                BuyerNIK = "BuyerNIK",
                CurrencyId = 1,
                CurrencyCode = "IDR",
                CurrencySymbol = "Rp",
                CurrencyRate = 14000,
                PaymentType = "Meter",
                VatType = "PPN Kawasan Berikat",
                Remark = "Remark",
                TotalPayment = 100,
                TotalPaid = 0,

                SalesInvoiceDetails = new List<SalesInvoiceDetailModel>()
                {
                    new SalesInvoiceDetailModel()
                    {
                        ShipmentDocumentId = 1,
                        ShipmentDocumentCode = "ShipmentDocumentCode",
                        SalesInvoiceItems = new List<SalesInvoiceItemModel>()
                        {
                            new SalesInvoiceItemModel()
                            {
                                ProductCode = "ProductCode",
                                ProductName = "ProductName",
                                Quantity = "Quantity",
                                PackingUom = "PackingUom",
                                UomId = 1,
                                UomUnit = "PCS",
                                Total = 1,
                                Price = 1,
                                Amount = 1,
                            },
                        }
                    }
                }
            };
        }

        public async Task<SalesInvoiceModel> GetNewData_SalesInvoiceType_Is_BGM()
        {
            return new SalesInvoiceModel()
            {
                Code = "code",
                AutoIncreament = 1,
                SalesInvoiceNo = "SalesInvoiceNo",
                SalesInvoiceType = "BGM",
                SalesInvoiceDate = DateTimeOffset.UtcNow,
                DueDate = DateTimeOffset.UtcNow.AddDays(-2),
                DeliveryOrderNo = "DeliveryOrderNo",
                BuyerId = 1,
                BuyerName = "BuyerName",
                BuyerCode = "BuyerCode",
                BuyerAddress = "BuyerAddress",
                BuyerNPWP = "BuyerNPWP",
                BuyerNIK = "BuyerNIK",
                CurrencyId = 1,
                CurrencyCode = "IDR",
                CurrencySymbol = "Rp",
                CurrencyRate = 14000,
                PaymentType = "Meter",
                VatType = "PPN Kawasan Berikat",
                Remark = "Remark",
                TotalPayment = 100,
                TotalPaid = 0,

                SalesInvoiceDetails = new List<SalesInvoiceDetailModel>()
                {
                    new SalesInvoiceDetailModel()
                    {
                        ShipmentDocumentId = 1,
                        ShipmentDocumentCode = "ShipmentDocumentCode",
                        SalesInvoiceItems = new List<SalesInvoiceItemModel>()
                        {
                            new SalesInvoiceItemModel()
                            {
                                ProductCode = "ProductCode",
                                ProductName = "ProductName",
                                Quantity = "Quantity",
                                PackingUom = "PackingUom",
                                UomId = 1,
                                UomUnit = "PCS",
                                Total = 1,
                                Price = 1,
                                Amount = 1,
                            },
                        }
                    }
                }
            };
        }

        public async Task<SalesInvoiceModel> GetNewData_SalesInvoiceType_Is_BPF()
        {
            return new SalesInvoiceModel()
            {
                Code = "code",
                AutoIncreament = 1,
                SalesInvoiceNo = "SalesInvoiceNo",
                SalesInvoiceType = "BPF",
                SalesInvoiceDate = DateTimeOffset.UtcNow,
                DueDate = DateTimeOffset.UtcNow.AddDays(-2),
                DeliveryOrderNo = "DeliveryOrderNo",
                BuyerId = 1,
                BuyerName = "BuyerName",
                BuyerCode = "BuyerCode",
                BuyerAddress = "BuyerAddress",
                BuyerNPWP = "BuyerNPWP",
                BuyerNIK = "BuyerNIK",
                CurrencyId = 1,
                CurrencyCode = "IDR",
                CurrencySymbol = "Rp",
                CurrencyRate = 14000,
                PaymentType = "Meter",
                VatType = "PPN Kawasan Berikat",
                Remark = "Remark",
                TotalPayment = 100,
                TotalPaid = 0,

                SalesInvoiceDetails = new List<SalesInvoiceDetailModel>()
                {
                    new SalesInvoiceDetailModel()
                    {
                        ShipmentDocumentId = 1,
                        ShipmentDocumentCode = "ShipmentDocumentCode",
                        SalesInvoiceItems = new List<SalesInvoiceItemModel>()
                        {
                            new SalesInvoiceItemModel()
                            {
                                ProductCode = "ProductCode",
                                ProductName = "ProductName",
                                Quantity = "Quantity",
                                PackingUom = "PackingUom",
                                UomId = 1,
                                UomUnit = "PCS",
                                Total = 1,
                                Price = 1,
                                Amount = 1,
                            },
                        }
                    }
                }
            };
        }

        public async Task<SalesInvoiceModel> GetNewData_SalesInvoiceType_Is_BPR()
        {
            return new SalesInvoiceModel()
            {
                Code = "code",
                AutoIncreament = 1,
                SalesInvoiceNo = "SalesInvoiceNo",
                SalesInvoiceType = "BPR",
                SalesInvoiceDate = DateTimeOffset.UtcNow,
                DueDate = DateTimeOffset.UtcNow.AddDays(-2),
                DeliveryOrderNo = "DeliveryOrderNo",
                BuyerId = 1,
                BuyerName = "BuyerName",
                BuyerCode = "BuyerCode",
                BuyerAddress = "BuyerAddress",
                BuyerNPWP = "BuyerNPWP",
                BuyerNIK = "BuyerNIK",
                CurrencyId = 1,
                CurrencyCode = "IDR",
                CurrencySymbol = "Rp",
                CurrencyRate = 14000,
                PaymentType = "Meter",
                VatType = "PPN Kawasan Berikat",
                Remark = "Remark",
                TotalPayment = 100,
                TotalPaid = 0,

                SalesInvoiceDetails = new List<SalesInvoiceDetailModel>()
                {
                    new SalesInvoiceDetailModel()
                    {
                        ShipmentDocumentId = 1,
                        ShipmentDocumentCode = "ShipmentDocumentCode",
                        SalesInvoiceItems = new List<SalesInvoiceItemModel>()
                        {
                            new SalesInvoiceItemModel()
                            {
                                ProductCode = "ProductCode",
                                ProductName = "ProductName",
                                Quantity = "Quantity",
                                PackingUom = "PackingUom",
                                UomId = 1,
                                UomUnit = "PCS",
                                Total = 1,
                                Price = 1,
                                Amount = 1,
                            },
                        }
                    }
                }
            };
        }
    }
}
