using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Com.Danliris.Service.Sales.Lib.ViewModels.ProductionOrder;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.DOSales
{
    public class DOSalesLocalViewModel : BaseViewModel
    {
        [MaxLength(64)]
        public ShinProductionOrderViewModel ProductionOrder { get; set; }
        public MaterialConstructionViewModel MaterialConstruction { get; set; }
        //public UnitViewModel Unit { get; set; }
        [MaxLength(512)]
        public string UnitOrCode { get; set; }
        public double TotalPacking { get; set; }
        public double TotalImperial { get; set; }
        public double TotalMetric { get; set; }

        public int? DOSalesId { get; set; }
    }
}
