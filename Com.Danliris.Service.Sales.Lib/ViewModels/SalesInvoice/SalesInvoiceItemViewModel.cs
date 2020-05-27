using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.SalesInvoice
{
    public class SalesInvoiceItemViewModel : BaseViewModel
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public double? QuantityPacking { get; set; }
        public string PackingUom { get; set; }
        public string ItemUom { get; set; }
        public double? QuantityItem { get; set; }
        //Price => harga satuan item
        public double? Price { get; set; }
        //Amount => total yang harus dibayarkan
        public double? Amount { get; set; }
        public string ConvertUnit { get; set; }
        public double? ConvertValue { get; set; }
    }
}
