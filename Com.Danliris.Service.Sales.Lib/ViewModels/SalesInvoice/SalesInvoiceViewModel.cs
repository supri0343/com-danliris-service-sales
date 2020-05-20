using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.SalesInvoice
{
    public class SalesInvoiceViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public long AutoIncreament { get; set; }
        public string SalesInvoiceNo { get; set; }
        public string SalesInvoiceType { get; set; }
        public DateTimeOffset? SalesInvoiceDate { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public string DeliveryOrderNo { get; set; }
        public string DeliveryOrderType { get; set; }
        public BuyerViewModel Buyer { get; set; }
        public CurrencyViewModel Currency { get; set; }
        public string PaymentType { get; set; }
        public string VatType { get; set; }
        public double? TotalPayment { get; set; }
        public double? TotalPaid { get; set; }
        public bool IsPaidOff { get; set; }
        public string Remark { get; set; }
        public string Sales { get; set; }
        public UnitViewModel Unit { get; set; }


        public ICollection<SalesInvoiceDetailViewModel> SalesInvoiceDetails { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(SalesInvoiceType) || SalesInvoiceType == "")
                yield return new ValidationResult("Kode Faktur Penjualan harus diisi", new List<string> { "SalesInvoiceType" });

            if (!SalesInvoiceDate.HasValue || SalesInvoiceDate.Value > DateTimeOffset.Now)
                yield return new ValidationResult("Tgl Faktur Penjualan harus diisi & lebih kecil atau sama dengan hari ini", new List<string> { "SalesInvoiceDate" });

            if (Buyer == null || string.IsNullOrWhiteSpace(Buyer.Name))
                yield return new ValidationResult("Buyer harus diisi", new List<string> { "BuyerName" });


            if (Buyer == null || string.IsNullOrWhiteSpace(Buyer.NPWP))
                yield return new ValidationResult("NPWP Buyer harus diisi", new List<string> { "BuyerNPWP" });

            if (Buyer == null || string.IsNullOrWhiteSpace(Buyer.NIK))
                yield return new ValidationResult("NIK Buyer harus diisi", new List<string> { "BuyerNIK" });

            //if (string.IsNullOrWhiteSpace(DeliveryOrderNo))
            //    yield return new ValidationResult("No. Surat Jalan harus diisi", new List<string> { "DeliveryOrderNo" });

            if (string.IsNullOrWhiteSpace(DeliveryOrderType) || DeliveryOrderType == "")
                yield return new ValidationResult("Kode Surat Jalan harus diisi", new List<string> { "DeliveryOrderType" });

            if (Currency == null || string.IsNullOrWhiteSpace(Currency.Code))
                yield return new ValidationResult("Kurs harus diisi", new List<string> { "CurrencyCode" });

            if (string.IsNullOrWhiteSpace(PaymentType) || PaymentType == "")
                yield return new ValidationResult("Pembayaran dalam satuan harus diisi", new List<string> { "PaymentType" });

            if (!DueDate.HasValue || Id == 0 && DueDate.Value < DateTimeOffset.Now.AddDays(-1))
                yield return new ValidationResult("Tanggal jatuh tempo kosong, Tempo belum diisi", new List<string> { "DueDate" });

            if (string.IsNullOrWhiteSpace(VatType) || VatType == "")
                yield return new ValidationResult("Jenis PPN harus diisi", new List<string> { "VatType" });

            if (!TotalPayment.HasValue || TotalPayment <= 0)
                yield return new ValidationResult("Total termasuk PPN kosong", new List<string> { "TotalPayment" });

            if (TotalPaid < 0)
                yield return new ValidationResult("Total Paid harus lebih besar atau sama dengan 0", new List<string> { "TotalPayment" });

            int Count = 0;
            string DetailErrors = "[";

            if (SalesInvoiceDetails != null && SalesInvoiceDetails.Count > 0)
            {
                foreach (var detail in SalesInvoiceDetails)
                {
                    int ErrorCount = 0;
                    DetailErrors += "{";

                    if (!detail.ShipmentDocumentId.HasValue || string.IsNullOrWhiteSpace(detail.ShipmentDocumentCode))
                    {
                        Count++;
                        ErrorCount++;
                        DetailErrors += "ShipmentDocumentCode : 'No. Bon Pengiriman kosong / tidak ditemukan',";
                    }

                    var duplicate = SalesInvoiceDetails.Where(w => w.ShipmentDocumentId.Equals(detail.ShipmentDocumentId.GetValueOrDefault()) && w.ShipmentDocumentCode.Equals(detail.ShipmentDocumentCode)).ToList();

                    if (duplicate.Count > 1)
                    {
                        Count++;
                        DetailErrors += "ShipmentDocumentCode : 'No. Bon Pengiriman duplikat',";
                    }

                    if (ErrorCount == 0)
                    {
                        if (detail.SalesInvoiceItems == null || detail.SalesInvoiceItems.Count == 0)
                        {
                            Count++;
                            DetailErrors += "SalesInvoiceItem : 'Item Kosong',";
                        }
                        else
                        {
                            DetailErrors += "SalesInvoiceItems: [";

                            foreach (var item in detail.SalesInvoiceItems)
                            {
                                DetailErrors += "{";

                                if (string.IsNullOrWhiteSpace(item.ProductCode))
                                {
                                    Count++;
                                    DetailErrors += "ProductCode : 'Kode produk harus diisi',";
                                }

                                if (!item.Price.HasValue || item.Price.Value <= 0)
                                {
                                    Count++;
                                    DetailErrors += "Price : 'Harga barang harus diisi dan lebih besar dari 0',";
                                }

                                if (item.Uom == null || string.IsNullOrWhiteSpace(item.Uom.Unit))
                                {
                                    Count++;
                                    DetailErrors += "UomUnit : 'Satuan harus diisi',";
                                }

                                DetailErrors += "}, ";
                            }

                            DetailErrors += "], ";
                        }
                    }

                    DetailErrors += "}, ";
                }
            }
            else
            {
                yield return new ValidationResult("Detail harus diisi", new List<string> { "SalesInvoiceDetail" });
            }

            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "SalesInvoiceDetails" });

            if (string.IsNullOrEmpty(Sales))
                yield return new ValidationResult("Sales Harus Diisi", new List<string> { "Sales" });

            if (Unit == null)
                yield return new ValidationResult("Unit Harus Diisi", new List<string> { "Unit" });

        }
    }
}
