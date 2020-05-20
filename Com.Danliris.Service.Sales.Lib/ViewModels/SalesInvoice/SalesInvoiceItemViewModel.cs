using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.SalesInvoice
{
    public class SalesInvoiceItemViewModel : BaseViewModel
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Quantity { get; set; }
        public string PackingUom { get; set; }
        public UomViewModel Uom { get; set; }
        public double? Total { get; set; }
        public double? Price { get; set; }
        public double? Amount { get; set; }
        public double? ConvertValue { get; set; }
        public string ConvertUnit { get; set; }
    }
}
