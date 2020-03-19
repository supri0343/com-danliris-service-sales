using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.Models.DOSales
{
    public class DOSalesModel : BaseModel
    {
        #region DOSalesTemplate
        [MaxLength(255)]
        public string Code { get; set; }
        public long AutoIncreament { get; set; }
        [MaxLength(255)]
        public string DOSalesNo { get; set; }
        [MaxLength(255)]
        public string DOSalesType { get; set; }
        [MaxLength(255)]
        public string Status { get; set; }
        public bool Accepted { get; set; }
        public bool Declined { get; set; }
        #endregion

        #region Lokal
        [MaxLength(255)]
        public string LocalType { get; set; }
        public DateTimeOffset LocalDate { get; set; }
        #region Sales Contract
        public int LocalSalesContractId { get; set; }
        [MaxLength(255)]
        public string LocalSalesContractNo { get; set; }
        #endregion
        #region Material
        public long LocalMaterialId { get; set; }
        [MaxLength(255)]
        public string LocalMaterialCode { get; set; }
        [MaxLength(1000)]
        public string LocalMaterialName { get; set; }
        public double LocalMaterialPrice { get; set; }
        [MaxLength(255)]
        public string LocalMaterialTags { get; set; }
        #endregion
        #region Material Construction
        public int LocalMaterialConstructionId { get; set; }
        [MaxLength(25)]
        public string LocalMaterialConstructionCode { get; set; }
        [MaxLength(255)]
        public string LocalMaterialConstructionName { get; set; }
        //[MaxLength(1000)]
        //public string LocalMaterialConstructionRemark { get; set; }
        [MaxLength(255)]
        public string MaterialWidth { get; set; }
        [MaxLength(255)]
        public string ColorRequest { get; set; }
        [MaxLength(255)]
        public string ColorTemplate { get; set; }
        #endregion
        #region Buyer
        public long LocalBuyerId { get; set; }
        [MaxLength(255)]
        public string LocalBuyerCode { get; set; }
        [MaxLength(1000)]
        public string LocalBuyerName { get; set; }
        [MaxLength(255)]
        public string LocalBuyerType { get; set; }
        [MaxLength(1000)]
        public string LocalBuyerAddress { get; set; }
        #endregion
        [MaxLength(255)]
        public string DestinationBuyerName { get; set; }
        [MaxLength(1000)]
        public string DestinationBuyerAddress { get; set; }
        //#region Sales
        //public string SalesId { get; set; }
        //[MaxLength(255)]
        //public string SalesFirstName { get; set; }
        //[MaxLength(255)]
        //public string SalesLastName { get; set; }
        //#endregion
        [MaxLength(255)]
        public string SalesName { get; set; }
        [MaxLength(255)]
        public string LocalHeadOfStorage { get; set; }
        [MaxLength(255)]
        public string PackingUom { get; set; }
        [MaxLength(255)]
        public string MetricUom { get; set; }
        [MaxLength(255)]
        public string ImperialUom { get; set; }
        public int Disp { get; set; }
        public int Op { get; set; }
        public int Sc { get; set; }
        [MaxLength(1000)]
        public string LocalRemark { get; set; }
        #endregion

        #region Ekspor
        [MaxLength(255)]
        public string ExportType { get; set; }
        public DateTimeOffset ExportDate { get; set; }
        [MaxLength(255)]
        public string DoneBy { get; set; }
        #region Sales Contract
        public int ExportSalesContractId { get; set; }
        [MaxLength(255)]
        public string ExportSalesContractNo { get; set; }
        #endregion
        #region Material Construction
        public int ExportMaterialConstructionId { get; set; }
        [MaxLength(25)]
        public string ExportMaterialConstructionCode { get; set; }
        [MaxLength(255)]
        public string ExportMaterialConstructionName { get; set; }
        //[MaxLength(1000)]
        //public string ExportMaterialConstructionRemark { get; set; }
        #endregion
        #region Buyer
        public long ExportBuyerId { get; set; }
        [MaxLength(255)]
        public string ExportBuyerCode { get; set; }
        [MaxLength(1000)]
        public string ExportBuyerName { get; set; }
        [MaxLength(255)]
        public string ExportBuyerType { get; set; }
        #endregion
        #region Commodity
        public int CommodityId { get; set; }
        [MaxLength(25)]
        public string CommodityCode { get; set; }
        [MaxLength(255)]
        public string CommodityName { get; set; }
        [MaxLength(1000)]
        public string CommodityDescription { get; set; }
        #endregion
        [MaxLength(255)]
        public string PieceLength { get; set; }
        public double OrderQuantity { get; set; }
        public double FillEachBale { get; set; }
        [MaxLength(1000)]
        public string ExportRemark { get; set; }
        #endregion
        
        public virtual ICollection<DOSalesLocalModel> DOSalesLocalItems { get; set; }
    }
}
