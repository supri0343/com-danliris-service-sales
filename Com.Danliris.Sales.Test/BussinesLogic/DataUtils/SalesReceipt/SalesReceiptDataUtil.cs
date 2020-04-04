using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.SalesInvoice;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.SalesReceipt;
using Com.Danliris.Service.Sales.Lib.Models.SalesReceipt;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.Danliris.Sales.Test.BussinesLogic.DataUtils.SalesReceipt
{
    public class SalesReceiptDataUtil : BaseDataUtil<SalesReceiptFacade, SalesReceiptModel>
    {
        private readonly SalesInvoiceDataUtil salesInvoiceDataUtil;

        public SalesReceiptDataUtil(SalesReceiptFacade facade, SalesInvoiceDataUtil salesInvoiceDataUtil) : base(facade)
        {
            this.salesInvoiceDataUtil = salesInvoiceDataUtil;
        }

        public override async Task<SalesReceiptModel> GetNewData()
        {

            var salesInvoiceData = await salesInvoiceDataUtil.GetTestData();
            var data = await base.GetNewData();

            data.Code = "code";
            data.AutoIncreament = 1;
            data.SalesReceiptNo = "SalesReceiptNo";
            data.SalesReceiptDate = DateTimeOffset.UtcNow;
            //data.UnitId = 1;
            data.UnitName = "Dying";
            data.BuyerId = 1;
            data.BuyerName = "BuyerName";
            data.BuyerAddress = "BuyerAddress";
            data.OriginBankName = "OriginBankName";
            data.OriginAccountNumber = "OriginAccountNumber";
            data.CurrencyId = 1;
            data.CurrencyCode = "CurrencyCode";
            data.CurrencySymbol = "CurrencySymbol";
            data.CurrencyRate = 1;
            data.BankId = 1;
            data.AccountName = "AccountName";
            data.AccountNumber = "AccountNumber";
            data.BankName = "BankName";
            data.BankCode = "BankCode";
            data.AdministrationFee = 1;
            data.TotalPaid = 1;

            data.SalesReceiptDetails = new List<SalesReceiptDetailModel>()
                {
                    new SalesReceiptDetailModel()
                    {
                        SalesInvoiceId = Convert.ToInt32(salesInvoiceData.Id),
                        SalesInvoiceNo = salesInvoiceData.SalesInvoiceNo,
                        DueDate = salesInvoiceData.DueDate,
                        VatType = "PPN BUMN",
                        Tempo = 16,
                        CurrencyId = 1,
                        CurrencyCode = "IDR",
                        CurrencySymbol = "Rp",
                        CurrencyRate = 14000,
                        TotalPayment = 10000,
                        TotalPaid = 1000,
                        Paid = 1000,
                        Nominal = 1000,
                        Unpaid = 8000,
                        OverPaid = 0,
                        IsPaidOff = false,

                    }
                };
            return data;
        }
    }
}
