using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.SalesReceipt
{
    public class SalesReceiptViewModel : BaseViewModel, IValidatableObject
    {
        [MaxLength(255)]
        public string Code { get; set; }
        public long? AutoIncreament { get; set; }
        [MaxLength(255)]
        public string SalesReceiptNo { get; set; }
        public DateTimeOffset? SalesReceiptDate { get; set; }
        //public UnitViewModel Unit { get; set; }
        #region Unit
        //Hanya terima unit string dari frontend
        //public int? UnitId { get; set; }
        [MaxLength(255)]
        public string UnitName { get; set; }
        #endregion
        public BuyerViewModel Buyer { get; set; }
        [MaxLength(255)]
        public string OriginBankName { get; set; }
        [MaxLength(255)]
        public string OriginAccountNumber { get; set; }
        public CurrencyViewModel Currency { get; set; }
        public AccountBankViewModel Bank { get; set; }
        public double? AdministrationFee { get; set; }
        public double TotalPaid { get; set; }

        public ICollection<SalesReceiptDetailViewModel> SalesReceiptDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!SalesReceiptDate.HasValue || SalesReceiptDate.Value > DateTimeOffset.Now)
                yield return new ValidationResult("Tgl Kuitansi harus diisi & lebih kecil atau sama dengan hari ini", new List<string> { "SalesReceiptDate" });

            if (string.IsNullOrWhiteSpace(UnitName))
                yield return new ValidationResult("Unit harus diisi", new List<string> { "UnitName" });

            if (Bank == null || string.IsNullOrWhiteSpace(Bank.AccountName))
                yield return new ValidationResult("Nama Bank Tujuan harus diisi", new List<string> { "AccountName" });

            if (Buyer == null || string.IsNullOrWhiteSpace(Buyer.Name))
                yield return new ValidationResult("Nama Buyer harus di isi", new List<string> { "BuyerName" });

            if (string.IsNullOrWhiteSpace(OriginBankName))
                yield return new ValidationResult("Nama Bank Asal harus di isi", new List<string> { "OriginBankName" });

            if (string.IsNullOrWhiteSpace(OriginAccountNumber))
                yield return new ValidationResult("No Rek. Bank Asal harus di isi", new List<string> { "OriginAccountNumber" });

            if (Currency == null || string.IsNullOrWhiteSpace(Currency.Code))
                yield return new ValidationResult("Jenis Mata Uang harus diisi", new List<string> { "CurrencyCode" });

            if (AdministrationFee < 0)
                yield return new ValidationResult("Total Paid kosong", new List<string> { "AdministrationFee" });

            if (TotalPaid <= 0)
                yield return new ValidationResult("Total Paid kosong", new List<string> { "TotalPaid" });

            int Count = 0;
            string DetailErrors = "[";

            if (SalesReceiptDetails != null && SalesReceiptDetails.Count > 0)
            {
                foreach (SalesReceiptDetailViewModel detail in SalesReceiptDetails)
                {
                    DetailErrors += "{";

                    var rowErrorCount = 0;

                    if (detail.SalesInvoice == null || string.IsNullOrWhiteSpace(detail.SalesInvoice.SalesInvoiceNo))
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "SalesInvoiceNo : 'Kode Faktur harus diisi',";
                    }
                    if (detail.SalesInvoice.Currency == null || string.IsNullOrWhiteSpace(detail.SalesInvoice.Currency.Code))
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "CurrencyCode : 'Kurs harus diisi',";
                    }
                    if (string.IsNullOrWhiteSpace(detail.VatType))
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "VatType : 'Type PPN kosong',";
                    }
                    if (!detail.Tempo.HasValue)
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "Tempo : 'Kode Faktur dan tanggal kuitansi harus diisi untuk memperoleh tempo',";
                    }
                    if (detail.TotalPayment <= 0)
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "TotalPayment : 'Kode Faktur harus diisi untuk memperoleh jumlah pembayaran',";
                    }
                    if (detail.TotalPaid < 0)
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "TotalPaid : 'Kode Faktur harus diisi untuk memperoleh jumlah pembayaran sebelumnya',";
                    }
                    if (detail.Paid < 0)
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "Paid : 'Kode Faktur harus diisi untuk memperoleh jumlah yang sudah dibayar',";
                    }
                    if (detail.Nominal < 0)
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "Nominal : 'Nominal tidak boleh kosong & atau lebih kecil dari 0',";
                    }
                    if (detail.Unpaid < 0)
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "Unpaid : 'Kode Faktur & Nominal harus diisi untuk memperoleh sisa pembayaran',";
                    }
                    if (!detail.OverPaid.HasValue)
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "OverPaid : 'Kode Faktur & Nominal harus diisi untuk memperoleh kelebihan pembayaran',";
                    }

                    var mustSameType = SalesReceiptDetails.Where(f => f.SalesInvoice.Currency.Code != detail.SalesInvoice.Currency.Code).ToList();
                    
                    if (mustSameType.Count > 0)
                    {
                        Count++;
                        DetailErrors += "CurrencyCode : 'Tiap No. Jual harus memiliki kurs yang sama',";
                    }

                    if (rowErrorCount == 0)
                    {
                        var duplicateDetails = SalesReceiptDetails.Where(f =>
                                f.SalesInvoice.SalesInvoiceNo.Equals(detail.SalesInvoice.SalesInvoiceNo) &&
                                f.SalesInvoice.Id.Equals(detail.SalesInvoice.Id)
                            ).ToList();

                        if (duplicateDetails.Count > 1)
                        {
                            Count++;
                            DetailErrors += "SalesInvoiceNo : 'Nomor Faktur Penjualan tidak boleh duplikat',";
                        }
                    }


                    DetailErrors += "}, ";
                }
            }
            else
            {
                yield return new ValidationResult("Detail harus diisi", new List<string> { "SalesReceiptDetail" });
            }

            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "SalesReceiptDetails" });

        }


    }
}
