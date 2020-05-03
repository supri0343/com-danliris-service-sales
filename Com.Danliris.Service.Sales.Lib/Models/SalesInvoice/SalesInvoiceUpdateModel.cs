using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.Models.SalesInvoice
{
    public class SalesInvoiceUpdateModel
    {
        public double TotalPaid { get; set; }
        public bool IsPaidOff { get; set; }
    }
}
