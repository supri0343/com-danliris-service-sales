using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.Models.DOReturn
{
    public class DOReturnItemModel : BaseModel
    {
        public int ShipmentDocumentId { get; set; }
        [MaxLength(255)]
        public string ShipmentDocumentCode { get; set; }
        [MaxLength(255)]
        public string ProductCode { get; set; }
        [MaxLength(255)]
        public string ProductName { get; set; }
        [MaxLength(255)]
        public string Quantity { get; set; }
        [MaxLength(255)]
        public string PackingUom { get; set; }
        public int UomId { get; set; }
        [MaxLength(255)]
        public string UomUnit { get; set; }
        public double? Total { get; set; }

        public virtual DOReturnDetailModel DOReturnDetailModel { get; set; }
    }
}
