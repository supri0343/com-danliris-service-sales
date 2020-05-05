using Com.Danliris.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.DOReturn
{
    public class DOReturnViewModel : BaseViewModel, IValidatableObject
    {
        [MaxLength(255)]
        public string Code { get; set; }
        public long AutoIncreament { get; set; }
        [MaxLength(255)]
        public string DOReturnNo { get; set; }
        [MaxLength(255)]
        public string Type { get; set; }
        public DateTimeOffset? Date { get; set; }
        [MaxLength(255)]
        public string ReturnFrom { get; set; }
        [MaxLength(255)]
        public string LKTPNo { get; set; }
        [MaxLength(255)]
        public string HeadOfStorage { get; set; }
        [MaxLength(1000)]
        public string Remark { get; set; }
        public virtual ICollection<DOReturnDetailViewModel> DOReturnDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Type) || Type == "")
                yield return new ValidationResult("Kode Retur harus diisi", new List<string> { "Type" });

            if (!Date.HasValue || Date.Value > DateTimeOffset.Now)
                yield return new ValidationResult("Tgl Retur harus diisi & lebih kecil atau sama dengan hari ini", new List<string> { "Date" });

            if (string.IsNullOrWhiteSpace(ReturnFrom))
                yield return new ValidationResult("No. LTKP harus diisi", new List<string> { "ReturnFrom" });

            if (string.IsNullOrWhiteSpace(LKTPNo))
                yield return new ValidationResult("No. LTKP harus diisi", new List<string> { "LKTPNo" });

            if (string.IsNullOrWhiteSpace(HeadOfStorage))
                yield return new ValidationResult("Nama Kepala Gudang harus diisi", new List<string> { "HeadOfStorage" });

            int Count = 0;
            string DetailErrors = "[";

            if (DOReturnDetails != null && DOReturnDetails.Count > 0)
            {
                foreach (var detail in DOReturnDetails)
                {
                    DetailErrors += "{";

                    var ErrorCount = 0;

                    if (detail.SalesInvoice == null || string.IsNullOrWhiteSpace(detail.SalesInvoice.SalesInvoiceNo))
                    {
                        Count++;
                        ErrorCount++;
                        DetailErrors += "SalesInvoiceNo : 'No. Ex. Faktur Penjualan kosong / tidak ditemukan',";
                    }
                    //else
                    //{

                    //    var duplicate = DOReturnDetails.Where(w => w.SalesInvoice.Id.Equals(detail.SalesInvoice.Id) && w.SalesInvoice.SalesInvoiceNo.Equals(detail.SalesInvoice.SalesInvoiceNo)).ToList();

                    //    if (duplicate.Count > 1)
                    //    {
                    //        Count++;
                    //        DetailErrors += "SalesInvoiceNo : 'No. Ex. Faktur Penjualan duplikat',";
                    //    }
                    //}

                    if (ErrorCount == 0)
                    {
                        if (detail.DOReturnDetailItems == null || detail.DOReturnDetailItems.Count == 0)
                        {
                            Count++;
                            DetailErrors += "DOReturnDetailItem : 'Detail Item Kosong',";
                        }
                        else
                        {
                            DetailErrors += "DOReturnDetailItems: [";

                            foreach (var detailItem in detail.DOReturnDetailItems)
                            {
                                DetailErrors += "{";

                                if (ErrorCount == 0)
                                {
                                    if (detailItem.DOReturnItems == null || detailItem.DOReturnItems.Count == 0)
                                    {
                                        Count++;
                                        DetailErrors += "DOReturnItem : 'Item Kosong',";
                                    }
                                    else
                                    {
                                        DetailErrors += "DOReturnItems: [";

                                        foreach (var item in detailItem.DOReturnItems)
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
                                else
                                {
                                    yield return new ValidationResult("Item kosong", new List<string> { "DOReturnItem" });
                                }

                                DetailErrors += "}, ";

                            }

                            DetailErrors += "], ";
                        }
                    }
                    else
                    {
                        yield return new ValidationResult("DetailItem kosong", new List<string> { "DOReturnDetailItem" });
                    }


                    DetailErrors += "}, ";
                }
            }
            else
            {
                yield return new ValidationResult("Detail harus diisi", new List<string> { "DOReturnDetail" });
            }

            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "DOReturnDetails" });

        }
    }
}
