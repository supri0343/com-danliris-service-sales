using Com.Danliris.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.SalesInvoice
{
    public class SalesInvoiceDetailViewModel : BaseViewModel
    {
        [MaxLength(255)]
        public string UnitCode { get; set; }
        [MaxLength(255)]
        public string Quantity { get; set; }
        public double? Total { get; set; }

        /*Uom*/
        public int? UomId { get; set; }
        [MaxLength(255)]
        public string UomUnit { get; set; }

        [MaxLength(255)]
        public string UnitName { get; set; }
        public double? UnitPrice { get; set; }
        public double Amount { get; set; }
        public int? SalesInvoiceId { get; set; }
    }
}
