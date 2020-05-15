using Com.Danliris.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.DOReturn
{
    public class DOReturnViewModel : BaseViewModel, IValidatableObject
    {
        public string SalesReturnType { get; set; }
        public DateTimeOffset? Date { get; set; }
        public string Origin { get; set; }
        public string LKTPNo { get; set; }
        public string Destination { get; set; }
        public string HeadOfStorage { get; set; }
        public string Remark { get; set; }
        public List<DOReturnSalesInvoiceViewModel> Items { get; set; }
        //[MaxLength(255)]
        //public string Code { get; set; }
        //public long AutoIncreament { get; set; }
        //[MaxLength(255)]
        //public string DOReturnNo { get; set; }
        //[MaxLength(255)]
        //public string Type { get; set; }
        //public DateTimeOffset? Date { get; set; }
        //[MaxLength(255)]
        //public string ReturnFrom { get; set; }
        //[MaxLength(255)]
        //public string LKTPNo { get; set; }
        //[MaxLength(255)]
        //public string HeadOfStorage { get; set; }
        //[MaxLength(1000)]
        //public string Remark { get; set; }

        //public virtual ICollection<DOReturnDetailViewModel> DOReturnDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(SalesReturnType) || SalesReturnType == "")
                yield return new ValidationResult("Tipe Retur Penjualan harus diisi", new List<string> { "ReturnType" });

            if (!Date.HasValue || Date.GetValueOrDefault() > DateTimeOffset.Now)
                yield return new ValidationResult("Tgl Retur harus diisi & lebih kecil atau sama dengan hari ini", new List<string> { "Date" });

            if (string.IsNullOrWhiteSpace(Origin))
                yield return new ValidationResult("Asal harus diisi", new List<string> { "ReturnFrom" });

            if (string.IsNullOrWhiteSpace(LKTPNo))
                yield return new ValidationResult("No. LTKP harus diisi", new List<string> { "LKTPNo" });

            if (string.IsNullOrWhiteSpace(HeadOfStorage))
                yield return new ValidationResult("Nama Kepala Gudang harus diisi", new List<string> { "HeadOfStorage" });

            //int Count = 0;
            //string DetailErrors = "[";

            //if (DOReturnDetails != null && DOReturnDetails.Count > 0)
            //{
            //    foreach (var detail in DOReturnDetails)
            //    {
            //        DetailErrors += "{";

            //        var ErrorCount = 0;

            //        if (detail.SalesInvoice == null || string.IsNullOrWhiteSpace(detail.SalesInvoice.SalesInvoiceNo))
            //        {
            //            Count++;
            //            ErrorCount++;
            //            DetailErrors += "SalesInvoiceNo : 'No. Ex. Faktur Penjualan kosong / tidak ditemukan',";
            //        }

            //        var duplicate = DOReturnDetails.Where(w => w.SalesInvoice != null && detail.SalesInvoice != null && w.SalesInvoice.Id.Equals(detail.SalesInvoice.Id) && w.SalesInvoice.SalesInvoiceNo.Equals(detail.SalesInvoice.SalesInvoiceNo)).ToList();

            //        if (duplicate.Count > 1)
            //        {
            //            Count++;
            //            DetailErrors += "SalesInvoiceNo : 'No. Ex. Faktur Penjualan duplikat',";
            //        }

            //        if (ErrorCount == 0)
            //        {
            //            if (detail.DOReturnDetailItems == null || detail.DOReturnDetailItems.Count == 0)
            //            {
            //                Count++;
            //                DetailErrors += "DOReturnDetailItem : 'Detail Item Kosong',";
            //            }
            //            else
            //            {
            //                DetailErrors += "DOReturnDetailItems: [";

            //                foreach (var detailItem in detail.DOReturnDetailItems)
            //                {
            //                    DetailErrors += "{";

            //                    if (detailItem.DOReturnItems == null || detailItem.DOReturnItems.Count == 0)
            //                    {
            //                        Count++;
            //                        DetailErrors += "DOReturnItem : 'Item Kosong',";
            //                    }
            //                    else
            //                    {
            //                        DetailErrors += "DOReturnItems: [";

            //                        foreach (var item in detailItem.DOReturnItems)
            //                        {
            //                            DetailErrors += "{";

            //                            if (string.IsNullOrWhiteSpace(item.Quantity))
            //                            {
            //                                Count++;
            //                                DetailErrors += "Quantity : 'Jumlah packing harus diisi',";
            //                            }

            //                            if (!item.Total.HasValue || item.Total.Value <= 0)
            //                            {
            //                                Count++;
            //                                DetailErrors += "Total : 'Total panjang harus diisi dan lebih besar dari 0',";
            //                            }

            //                            DetailErrors += "}, ";

            //                        }

            //                        DetailErrors += "], ";
            //                    }

            //                    DetailErrors += "}, ";

            //                }

            //                DetailErrors += "], ";
            //            }
            //        }
            //        else
            //        {
            //            yield return new ValidationResult("DetailItem kosong", new List<string> { "DOReturnDetailItem" });
            //        }

            //        DetailErrors += "}, ";
            //    }
            ////}
            //else
            //{
            //    yield return new ValidationResult("Detail harus diisi", new List<string> { "DOReturnDetail" });
            //}

            //DetailErrors += "]";

            //if (Count > 0)
            //    yield return new ValidationResult(DetailErrors, new List<string> { "DOReturnDetails" });

        }
    }
}
