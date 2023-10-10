using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Sales.Test.ViewModels.ProductionOrder
{
    public class ProductionOrderForDPViewModel
    {
        public string standardTestName { get; set; }
        public string buyer { get; set; }
        public string yarnMaterialName { get; set; }
        public string finishType { get; set; }
        public DateTimeOffset deliveryDate{ get; set; }
}
}
