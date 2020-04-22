using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.DOReturn
{
    public class DOReturnItemViewModel : BaseViewModel
    {
        [MaxLength(255)]
        public string ProductCode { get; set; }
        [MaxLength(255)]
        public string ProductName { get; set; }
        [MaxLength(255)]
        public string Quantity { get; set; }
        public UomViewModel Uom { get; set; }
        public double? Total { get; set; }
        public double? Price { get; set; }
        public double Amount { get; set; }
        public int? DOReturnDetailItemId { get; set; }
    }
}
