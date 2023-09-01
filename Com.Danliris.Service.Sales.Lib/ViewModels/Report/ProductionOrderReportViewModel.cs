using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.Report
{
    public class ProductionOrderReportViewModel
    {
        internal string standardTest;

        public long id { get; set; }
        public string status { get; set; }
        public string detail { get; set; }
        public string orderNo { get; set; }
        public string NoSalesContract { get; set; }
        public string colorType { get; set; }
        public double Price { get; set; }
        public string CurrCode { get; set; }
        public double orderQuantity { get; set; }
        public string orderType { get; set; }
        public int orderTypeId { get; set; }
        public string processType { get; set; }
        public string construction { get; set; }
        public string designCode { get; set; }
        public string designNumber { get; set; }
        public string colorTemplate { get; set; }
        public string colorRequest { get; set; }
        public string buyer { get; set; }
        public string buyerType { get; set; }
        public string staffName { get; set; }
        public DateTimeOffset _createdDate { get; set; }
        public DateTimeOffset deliveryDate { get; set; }

        public string finishTypeName { get; set; } // nvarchar(255), null
        public string finishWidth { get; set; }    // nvarchar(255), null
        public string materialName { get; set; }   // nvarchar(1000), null
        public string yarnMaterialName { get; set; } // nvarchar(1000), null
        public string materialWidth { get; set; }  // nvarchar(1000), null
        public string handlingStandard { get; set; } // nvarchar(255), null


    }
}
