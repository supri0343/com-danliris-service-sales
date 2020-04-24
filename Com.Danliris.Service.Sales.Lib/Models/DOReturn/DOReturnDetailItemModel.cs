using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.Models.DOReturn
{
    public class DOReturnDetailItemModel : BaseModel
    {
        #region Shipment Document
        public int ShipmentDocumentId { get; set; }
        [MaxLength(255)]
        public string ShipmentDocumentCode { get; set; }
        #endregion

        //public int DOReturnDetailId { get; set; }
        public virtual DOReturnDetailModel DOReturnDetailModel { get; set; }
        public virtual ICollection<DOReturnItemModel> DOReturnItems { get; set; }
    }
}
