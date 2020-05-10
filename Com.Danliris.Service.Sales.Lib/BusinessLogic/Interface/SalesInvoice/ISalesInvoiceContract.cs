using Com.Danliris.Service.Sales.Lib.Models.SalesInvoice;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseInterface;
using Com.Danliris.Service.Sales.Lib.ViewModels.SalesInvoice;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.SalesInvoice
{
    public interface ISalesInvoiceContract : IBaseFacade<SalesInvoiceModel>
    {
        List<SalesInvoiceModel> ReadByBuyerId(int buyerId);
        Task<int> UpdateFromSalesReceiptAsync(int id, SalesInvoiceUpdateModel model);
        Task<List<SalesInvoiceReportViewModel>> GetReport(int buyerId, long salesInvoiceId, bool? isPaidOff, DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet);
        Task<MemoryStream> GenerateExcel(int buyerId, long salesInvoiceId, bool? isPaidOff, DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet);
    }
}
