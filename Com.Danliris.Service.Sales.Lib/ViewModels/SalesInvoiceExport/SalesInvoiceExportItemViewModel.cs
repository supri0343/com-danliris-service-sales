using Com.Danliris.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.SalesInvoiceExport
{
    public class SalesInvoiceExportItemViewModel : BaseViewModel
    {
        public int? ProductId { get; set; }
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
