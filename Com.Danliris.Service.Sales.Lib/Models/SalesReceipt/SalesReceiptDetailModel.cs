using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using System;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.Models.SalesReceipt
{
    public class SalesReceiptDetailModel : BaseModel
    {
        #region SalesInvoice
        public int SalesInvoiceId { get; set; }
        [MaxLength(255)]
        public string SalesInvoiceNo { get; set; }

        #region Currency
        public int CurrencyId { get; set; }
        [MaxLength(255)]
        public string CurrencyCode { get; set; }
        [MaxLength(255)]
        public string CurrencySymbol { get; set; }
        public double CurrencyRate { get; set; }
        #endregion
        #endregion

        public DateTimeOffset DueDate { get; set; }
        [MaxLength(255)]
        public string VatType { get; set; }
        public double Tempo { get; set; }
        public double TotalPayment { get; set; }
        public double TotalPaid { get; set; }
        public double Paid { get; set; }
        public double Nominal { get; set; }
        public double Unpaid { get; set; }
        public double OverPaid { get; set; }
        public bool IsPaidOff { get; set; }

        public int SalesReceiptId { get; set; }


        public virtual SalesReceiptModel SalesReceiptModel { get; set; }
    }
}
