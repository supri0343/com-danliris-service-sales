using Com.Danliris.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.SalesInvoiceExport
{
    public class SalesInvoiceExportDetailViewModel : BaseViewModel
    {
        public int? ShippingOutId { get; set; }
        public string BonNo { get; set; }
        public string WeightUom { get; set; }
        public string TotalUom { get; set; }
        public double? GrossWeight { get; set; }
        public double? NetWeight { get; set; }
        public double? TotalMeas { get; set; }

        public ICollection<SalesInvoiceExportItemViewModel> SalesInvoiceExportItems { get; set; }
    }
}
