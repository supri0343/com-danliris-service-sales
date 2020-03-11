using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.Models.DOSales
{
    public class DOSalesLocalModel : BaseModel
    {
        #region ProductionOrder
        public int ProductionOrderId { get; set; }
        [MaxLength(64)]
        public string ProductionOrderNo { get; set; }
        #endregion
        #region Material Construction
        public long MaterialConstructionId { get; set; }
        [MaxLength(1000)]
        public string MaterialConstructionName { get; set; }
        [MaxLength(255)]
        public string MaterialConstructionCode { get; set; }
        public string MaterialConstructionRemark { get; set; }
        #endregion
        //#region Unit
        //public int UnitId { get; set; }
        //[MaxLength(128)]
        //public string UnitCode { get; set; }
        //[MaxLength(512)]
        //public string UnitName { get; set; }
        //#endregion
        [MaxLength(512)]
        public string UnitOrCode { get; set; }
        public double TotalPacking { get; set; }
        public double TotalImperial { get; set; }
        public double TotalMetric { get; set; }

        public int DOSalesId { get; set; }
        public virtual DOSalesModel DOSalesModel { get; set; }
    }
}
