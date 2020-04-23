using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.Models.DOReturn
{
    public class DOReturnDetailModel : BaseModel
    {
        #region Sales Invoice
        public int SalesInvoiceId { get; set; }
        [MaxLength(255)]
        public string SalesInvoiceNo { get; set; }
        #endregion

        //public int DOReturnId { get; set; }
        public virtual DOReturnModel DOReturnModel { get; set; }
        public virtual ICollection<DOReturnDetailItemModel> DOReturnDetailItems { get; set; }
    }
}
