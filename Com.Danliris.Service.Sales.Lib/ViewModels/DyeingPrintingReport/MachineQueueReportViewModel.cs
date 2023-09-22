using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.DyeingPrintingReport
{
    public class MachineQueueReportViewModel
    {
        public string SPPNo { get; set; }
        public string UomUnit { get; set; }
        public double orderLength { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
    }
}
