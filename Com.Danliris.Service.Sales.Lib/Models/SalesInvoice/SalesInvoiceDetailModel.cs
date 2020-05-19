using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.Models.SalesInvoice
{
    public class SalesInvoiceDetailModel : BaseModel
    {
        #region Shipment Document
        public int ShipmentDocumentId { get; set; }
        [MaxLength(255)]
        public string ShipmentDocumentCode { get; set; }
        #endregion

        public virtual SalesInvoiceModel SalesInvoiceModel { get; set; }
        public virtual ICollection<SalesInvoiceItemModel> SalesInvoiceItems { get; set; }
    }
}
