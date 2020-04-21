using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.ViewModels.SalesInvoice;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.DOReturn
{
    public class DOReturnDetailViewModel : BaseViewModel
    {
        public SalesInvoiceViewModel SalesInvoice { get; set; }

        public int? DOReturnId { get; set; }
    }
}
