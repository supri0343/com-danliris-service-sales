using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.DOSales
{
    public class DOSalesViewModel : BaseViewModel, IValidatableObject
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
        public DateTimeOffset? LocalDate { get; set; }
        public FinishingPrintingSalesContractViewModel LocalSalesContract { get; set; }
        public MaterialViewModel LocalMaterial { get; set; }
        public MaterialConstructionViewModel LocalMaterialConstruction { get; set; }
        public BuyerViewModel LocalBuyer { get; set; }
        [MaxLength(255)]
        public string DestinationBuyerName { get; set; }
        [MaxLength(1000)]
        public string DestinationBuyerAddress { get; set; }
        //public AccountViewModel Sales { get; set; }[MaxLength(255)]
        public string SalesName { get; set; }
        [MaxLength(255)]
        public string LocalHeadOfStorage { get; set; }
        [MaxLength(255)]
        public string PackingUom { get; set; }
        [MaxLength(255)]
        public string MetricUom { get; set; }
        [MaxLength(255)]
        public string ImperialUom { get; set; }
        public int? Disp { get; set; }
        public int? Op { get; set; }
        public int? Sc { get; set; }
        [MaxLength(1000)]
        public string LocalRemark { get; set; }
        #endregion

        # region Ekspor
        [MaxLength(255)]
        public string ExportType { get; set; }
        public DateTimeOffset? ExportDate { get; set; }
        [MaxLength(255)]
        public string DoneBy { get; set; }
        public FinishingPrintingSalesContractViewModel ExportSalesContract { get; set; }
        public MaterialConstructionViewModel ExportMaterialConstruction { get; set; }
        public BuyerViewModel ExportBuyer { get; set; }
        public CommodityViewModel Commodity { get; set; }
        public double? FillEachBale { get; set; }
        [MaxLength(1000)]
        public string ExportRemark { get; set; }
        #endregion

        public ICollection<DOSalesLocalViewModel> DOSalesLocalItems { get; set; }
        public BuyerViewModel Buyer { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (string.IsNullOrWhiteSpace(DOSalesType) || DOSalesType == "")
            {
                yield return new ValidationResult("Jenis DO harus dipilih", new List<string> { "DOSalesType" });
            }
            if (DOSalesType == "Ekspor")
            {
                if (string.IsNullOrWhiteSpace(ExportType) || ExportType == "")
                    yield return new ValidationResult("Seri DO Ekspor harus dipilih", new List<string> { "ExportType" });

                if (!ExportDate.HasValue)
                    yield return new ValidationResult("Tanggal DO Ekspor harus diisi", new List<string> { "ExportDate" });

                if (string.IsNullOrWhiteSpace(DoneBy))
                    yield return new ValidationResult("Dikerjakan oleh harus diisi", new List<string> { "DoneBy" });

                if (ExportSalesContract == null || string.IsNullOrWhiteSpace(ExportSalesContract.SalesContractNo))
                    yield return new ValidationResult("No. Sales Contract harus diisi", new List<string> { "ExportSalesContract" });

                if (!FillEachBale.HasValue || FillEachBale.Value <= 0)
                    yield return new ValidationResult("Isi tiap bale harus lebih besar dari 0", new List<string> { "FillEachBale" });
            }
            else if (DOSalesType == "Lokal")
            {
                if (string.IsNullOrWhiteSpace(LocalType) || LocalType == "")
                    yield return new ValidationResult("Seri DO Lokal harus dipilih", new List<string> { "LocalType" });

                if (!LocalDate.HasValue)
                    yield return new ValidationResult("Tanggal DO Lokal harus diisi", new List<string> { "LocalDate" });

                if (LocalSalesContract == null || string.IsNullOrWhiteSpace(LocalSalesContract.SalesContractNo))
                    yield return new ValidationResult("No. Sales Contract harus diisi", new List<string> { "LocalSalesContract" });

                if (string.IsNullOrWhiteSpace(DestinationBuyerName))
                    yield return new ValidationResult("Nama Penerima harus diisi", new List<string> { "DestinationBuyerName" });

                if (string.IsNullOrWhiteSpace(DestinationBuyerAddress))
                    yield return new ValidationResult("Alamat Tujuan harus diisi", new List<string> { "DestinationBuyerAddress" });

                if (string.IsNullOrWhiteSpace(LocalHeadOfStorage))
                    yield return new ValidationResult("Nama Kepala Gudang harus diisi", new List<string> { "LocalHeadOfStorage" });

                if (string.IsNullOrWhiteSpace(SalesName))
                    yield return new ValidationResult("Nama Sales harus diisi", new List<string> { "SalesName" });

                if (string.IsNullOrWhiteSpace(PackingUom))
                    yield return new ValidationResult("Satuan Imperial harus dipilih", new List<string> { "PackingUom" });

                if (string.IsNullOrWhiteSpace(ImperialUom))
                    yield return new ValidationResult("Satuan Imperial harus dipilih", new List<string> { "ImperialUom" });

                if (string.IsNullOrWhiteSpace(MetricUom))
                    yield return new ValidationResult("Satuan Imperial harus dipilih", new List<string> { "MetricUom" });

                if (!Disp.HasValue || Disp <= 0)
                    yield return new ValidationResult("Disp harus diisi", new List<string> { "Disp" });

                if (!Op.HasValue || Op <= 0)
                    yield return new ValidationResult("Op harus diisi", new List<string> { "Op" });

                if (!Sc.HasValue || Sc <= 0)
                    yield return new ValidationResult("Sc harus diisi", new List<string> { "Sc" });


                int Count = 0;
                string DetailErrors = "[";

                if (DOSalesLocalItems != null && DOSalesLocalItems.Count > 0)
                {
                    foreach (DOSalesLocalViewModel detail in DOSalesLocalItems)
                    {
                        DetailErrors += "{";

                        var rowErrorCount = 0;
                        
                        if (detail.Material == null)
                        {
                            Count++;
                            rowErrorCount++;
                            DetailErrors += "Material : 'Material gagal di load',";
                        }


                        if (detail.MaterialConstruction == null)
                        {
                            Count++;
                            rowErrorCount++;
                            DetailErrors += "MaterialConstruction : MaterialConstruction gagal di load',";
                        }

                        if (string.IsNullOrWhiteSpace(detail.UnitOrCode))
                        {
                            Count++;
                            rowErrorCount++;
                            DetailErrors += "UnitOrCode : 'Unit/Kode harus diisi',";
                        }
                        if (detail.TotalImperial <= 0)
                        {
                            Count++;
                            rowErrorCount++;
                            DetailErrors += "TotalImperial : 'Total Imperial harus lebih besar dari 0',";
                        }
                        if (detail.TotalMetric <= 0)
                        {
                            Count++;
                            rowErrorCount++;
                            DetailErrors += "TotalMetric : 'Total Metric harus lebih besar dari 0',";
                        }
                        if (detail.TotalPacking <= 0)
                        {
                            Count++;
                            rowErrorCount++;
                            DetailErrors += "TotalPacking : 'Total Packing harus lebih besar dari 0',";
                        }
                        DetailErrors += "}, ";
                    }
                }
                else
                {
                    yield return new ValidationResult("Detail harus diisi", new List<string> { "LocalItem" });
                }

                DetailErrors += "]";

                if (Count > 0)
                    yield return new ValidationResult(DetailErrors, new List<string> { "DOSalesLocalItems" });

            }
        }
    }
}
