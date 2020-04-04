using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.SalesReceipt
{
    public class SalesReceiptDetailViewModel : BaseViewModel
    {
        public int? SalesInvoiceId { get; set; }
        [MaxLength(255)]
        public string SalesInvoiceNo { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        [MaxLength(255)]
        public string VatType { get; set; }
        public double? Tempo { get; set; }
        public CurrencyViewModel Currency { get; set; }
        public double TotalPayment { get; set; }
        public double TotalPaid { get; set; }
        public double Paid { get; set; }
        public double Nominal { get; set; }
        public double Unpaid { get; set; }
        public double? OverPaid { get; set; }
        public bool IsPaidOff { get; set; }

        public int? SalesReceiptId { get; set; }

    }
}
