using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.Models.SalesInvoiceExport
{
    public class SalesInvoiceExportDetailModel : BaseModel
    {
        public int ShippingOutId { get; set; }
        [MaxLength(255)]
        public string BonNo { get; set; }
        [MaxLength(255)]
        public string WeightUom { get; set; }
        [MaxLength(255)]
        public string TotalUom { get; set; }
        public double GrossWeight { get; set; }
        public double NetWeight { get; set; }
        public double TotalMeas { get; set; }

        public virtual SalesInvoiceExportModel SalesInvoiceExportModel { get; set; }
        public virtual ICollection<SalesInvoiceExportItemModel> SalesInvoiceExportItems { get; set; }
    }
}
