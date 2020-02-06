using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.Models.SalesInvoice
{
    public class SalesInvoiceDetailModel : BaseModel
    {
        [MaxLength(255)]
        public string UnitCode { get; set; }
        [MaxLength(255)]
        public string Quantity { get; set; }
        public double Total { get; set; }

        /*Uom*/
        public int UomId { get; set; }
        [MaxLength(255)]
        public string UomUnit { get; set; }

        [MaxLength(255)]
        public string UnitName { get; set; }
        public double UnitPrice { get; set; }
        public double Amount { get; set; }
        public int SalesInvoiceId { get; set; }


        public virtual SalesInvoiceModel SalesInvoiceModel { get; set; }
    }
}
