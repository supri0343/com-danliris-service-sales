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
        #endregion

        #region Currency
        public int CurrencyId { get; set; }
        [MaxLength(255)]
        public string CurrencyCode { get; set; }
        [MaxLength(255)]
        public string CurrencySymbol { get; set; }
        public double CurrencyRate { get; set; }
        #endregion

        public DateTimeOffset DueDate { get; set; }
        [MaxLength(255)]
        public string VatType { get; set; }
        public double Tempo { get; set; }

        //TotalPayment => jumlah yang ditangguhkan
        public double TotalPayment { get; set; }

        //TotalPaid => jumlah yang sudah dibayar
        public double TotalPaid { get; set; }

        //Paid => jumlah yang dibayarakan (TotalPaid + Nominal)
        public double Paid { get; set; }

        //Nominal => jumlah yang akan dibayar
        public double Nominal { get; set; }

        //Unpaid => jumlah yang belum dibayar (hutang)
        public double Unpaid { get; set; }

        //OverPaid => kelebihan bayar melebihi TotalPayment (bonus)
        public double OverPaid { get; set; }

        //IsPaidOff => status lunas/tidak
        public bool IsPaidOff { get; set; }

        //public int SalesReceiptId { get; set; }


        public virtual SalesReceiptModel SalesReceiptModel { get; set; }
    }
}
