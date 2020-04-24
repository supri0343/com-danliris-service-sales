using Com.Danliris.Service.Sales.Lib.Utilities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.SalesInvoice
{
    public class SalesInvoiceDetailViewModel : BaseViewModel
    {
        /*Shipment Document*/
        public int? ShipmentDocumentId { get; set; }
        [MaxLength(255)]
        public string ShipmentDocumentCode { get; set; }
        //public int? SalesInvoiceId { get; set; }

        public ICollection<SalesInvoiceItemViewModel> SalesInvoiceItems { get; set; }
    }
}
