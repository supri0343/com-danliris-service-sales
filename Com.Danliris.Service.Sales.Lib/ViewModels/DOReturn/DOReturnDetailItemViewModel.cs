﻿using Com.Danliris.Service.Sales.Lib.Utilities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.DOReturn
{
    public class DOReturnDetailItemViewModel : BaseViewModel
    {
        #region Shipment Document
        public int? ShipmentDocumentId { get; set; }
        [MaxLength(255)]
        public string ShipmentDocumentCode { get; set; }
        #endregion

        //public int? DOReturnDetailId { get; set; }
        public virtual ICollection<DOReturnItemViewModel> DOReturnItems { get; set; }
    }
}
