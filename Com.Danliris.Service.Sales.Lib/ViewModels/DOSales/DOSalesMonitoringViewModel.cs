using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.DOSales
{
    public class DOSalesMonitoringViewModel
    {
        public DateTimeOffset Date { get; set; }
        public string DOSalesNo { get; set; }
        public string BuyerType { get; set; }
        public string BuyerName { get; set; }
        public string ProductionOrderNo { get; set; }
        public int ProductionOrderId { get; set; }

        
        public string SalesName { get; set; }
        public double OrderQuantity { get; set; }
        public double PackingQty { get; set; }
        public double StockQty { get; set; }
    }
}
