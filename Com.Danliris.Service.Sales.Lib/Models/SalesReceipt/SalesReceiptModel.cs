using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.Models.SalesReceipt
{
    public class SalesReceiptModel : BaseModel
    {
        [MaxLength(255)]
        public string Code { get; set; }
        public long AutoIncreament { get; set; }
        [MaxLength(255)]
        public string SalesReceiptNo { get; set; }
        public DateTimeOffset SalesReceiptDate { get; set; }

        #region Unit
        public int UnitId { get; set; }
        [MaxLength(255)]
        public string UnitName { get; set; }
        #endregion

        #region Buyer
        public int BuyerId { get; set; }
        [MaxLength(255)]
        public string BuyerName { get; set; }
        [MaxLength(1000)]
        public string BuyerAddress { get; set; }
        #endregion

        [MaxLength(255)]
        public string OriginAccountNumber { get; set; }

        #region Currency
        public int CurrencyId { get; set; }
        [MaxLength(255)]
        public string CurrencyCode { get; set; }
        [MaxLength(255)]
        public string CurrencySymbol { get; set; }
        public double CurrencyRate { get; set; }
        #endregion

        #region Bank
        public int BankId { get; set; }
        [MaxLength(255)]
        public string AccountName { get; set; }
        [MaxLength(255)]
        public string AccountNumber { get; set; }
        [MaxLength(255)]
        public string BankName { get; set; }
        [MaxLength(255)]
        public string BankCode { get; set; }

        //#region BankCurrency
        //public int BankCurrencyId { get; set; }
        //[MaxLength(255)]
        //public string BankCurrencyCode { get; set; }
        //[MaxLength(255)]
        //public string BankCurrencySymbol { get; set; }
        //public double BankCurrencyRate { get; set; }
        //#endregion

        #endregion

        public double AdministrationFee { get; set; }
        public double TotalPaid { get; set; }

        public virtual ICollection<SalesReceiptDetailModel> SalesReceiptDetails { get; set; }

    }
}
