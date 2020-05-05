using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.Models.DOReturn
{
    public class DOReturnModel : BaseModel
    {
        [MaxLength(255)]
        public string Code { get; set; }
        public long AutoIncreament { get; set; }
        [MaxLength(255)]
        public string DOReturnNo { get; set; }
        [MaxLength(255)]
        public string Type { get; set; }
        public DateTimeOffset Date { get; set; }
        [MaxLength(255)]
        public string ReturnFrom { get; set; }
        [MaxLength(255)]
        public string LKTPNo { get; set; }
        [MaxLength(255)]
        public string HeadOfStorage { get; set; }
        [MaxLength(1000)]
        public string Remark { get; set; }
        public virtual ICollection<DOReturnDetailModel> DOReturnDetails { get; set; }

    }
}
