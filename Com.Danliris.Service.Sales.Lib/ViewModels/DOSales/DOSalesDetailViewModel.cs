using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Com.Danliris.Service.Sales.Lib.ViewModels.ProductionOrder;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.DOSales
{
    public class DOSalesDetailViewModel : BaseViewModel
    {
        [MaxLength(64)]
        public ProductionOrderViewModel ProductionOrder { get; set; }
        public MaterialViewModel Material { get; set; }
        public MaterialConstructionViewModel MaterialConstruction { get; set; }
        [MaxLength(1000)]
        public string ConstructionName { get; set; }
        //public UnitViewModel Unit { get; set; }
        [MaxLength(255)]
        public string ColorRequest { get; set; }
        [MaxLength(255)]
        public string ColorTemplate { get; set; }
        [MaxLength(512)]
        public string UnitOrCode { get; set; }
        public double Packing { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public double ConvertionValue { get; set; }

        public int? DOSalesId { get; set; }
    }
}
