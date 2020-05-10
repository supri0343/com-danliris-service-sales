using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.Models.SalesInvoice
{
    public class SalesInvoiceItemModel : BaseModel
    {
        #region Product
        [MaxLength(255)]
        public string ProductCode { get; set; }
        [MaxLength(255)]
        public string ProductName { get; set; }
        [MaxLength(255)]
        public string Quantity { get; set; }
        [MaxLength(255)]
        public string PackingUom { get; set; }

        #region Uom
        public int UomId { get; set; }
        [MaxLength(255)]
        public string UomUnit { get; set; }
        #endregion

        public double Total { get; set; }
        #endregion

        //Price => harga satuan item
        public double Price { get; set; }
        //Amount => total yang harus dibayarkan
        public double Amount { get; set; }
        //public int SalesInvoiceDetailId { get; set; }
        public string ConvertUnit { get; set; }
        public double ConvertValue { get; set; }

        public virtual SalesInvoiceDetailModel SalesInvoiceDetailModel { get; set; }
    }
}
