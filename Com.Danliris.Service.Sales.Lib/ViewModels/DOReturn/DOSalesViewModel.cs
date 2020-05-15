using Com.Danliris.Service.Sales.Lib.Utilities;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.DOReturn
{
    public class DOSalesInvoiceViewModel : BaseViewModel
    {
        public int? ShipmentDocumentId { get; set; }
        public string ShipmentDocumentCode { get; set; }
        public string Design { get; set; }
        public string Type { get; set; }
        public decimal? PackingQuantity { get; set; }
        public decimal? LengthQuantity { get; set; }
        public decimal? WeightQuantity { get; set; }
    }
}