using Com.Danliris.Service.Sales.Lib.Models.SalesInvoice;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseInterface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.SalesInvoice
{
    public interface ISalesInvoiceContract : IBaseFacade<SalesInvoiceModel>
    {
        List<SalesInvoiceModel> ReadByBuyerId(int buyerId);
        Task<int> UpdateFromSalesReceiptAsync(int id, SalesInvoiceUpdateModel model);
    }
}
