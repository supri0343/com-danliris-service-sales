using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.ViewModels.DOSales;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.DOReturn
{
    public class DOReturnDetailItemViewModel : BaseViewModel
    {
        public DOSalesInvoiceViewModel DOSales { get; set; }
    }
}
