using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.DyeingPrintingReportInterface;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.DyeingPrintingReportLogics;
using Com.Danliris.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.Models.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Data;
using System.Linq;
using Com.Danliris.Service.Sales.Lib.ViewModels.DyeingPrintingReport;
using OfficeOpenXml;
using System.Globalization;
using Newtonsoft.Json;
using OfficeOpenXml.Style;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.DyeingPrintingReportFacades
{
    public class MachineQueueReportFacade : IMachineQueueReport
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<ProductionOrderModel> dbSet;
        private readonly DbSet<FinishingPrintingSalesContractModel> scDbSet;
        private IdentityService IdentityService;
        private MachineQueueReportLogic MachineQueueReportLogic;

        public MachineQueueReportFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.dbSet = this.DbContext.Set<ProductionOrderModel>();
            this.scDbSet = this.DbContext.Set<FinishingPrintingSalesContractModel>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
            this.MachineQueueReportLogic = serviceProvider.GetService<MachineQueueReportLogic>();
        }

        public Tuple<MemoryStream, string> GenerateExcel(string filter = "{}")
        {
            var Query = MachineQueueReportLogic.GetQuery(filter);
            var data = Query.ToList();
            DataTable result = new DataTable();
            var offset = 7;
            Filter _filter = JsonConvert.DeserializeObject<Filter>(filter);

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "No SPP", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Panjang Order", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Delivery", DataType = typeof(String) });

            Dictionary<string, string> Rowcount = new Dictionary<string, string>();
            var index = 0;

            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", ""); 
            else
            {
                foreach (MachineQueueReportViewModel item in Query.ToList())
                {
                    index++;
                    string length = item.orderLength + " " + item.UomUnit;
                    string date = item.DeliveryDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    result.Rows.Add(index, item.SPPNo, length, date);
                }
            }
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                var type = !string.IsNullOrEmpty(_filter.orderTypeName ) ? _filter.orderTypeName : "-";
                var dateFrom = _filter.dateFrom !=null ? _filter.dateFrom.GetValueOrDefault().ToString("dd MMM yyyy")  : "-";
                var dateTo = _filter.dateTo != null ? _filter.dateTo.GetValueOrDefault().ToString("dd MMM yyyy") : "-";
                worksheet.Cells["A1"].Value = "LAPORAN ORDER BELUM DIPRODUKSI MESIN";
                worksheet.Cells["A2"].Value = "JENIS ORDER : " + type;
                worksheet.Cells["A3"].Value = "TANGGAL AWAL : " + dateFrom + "  TANGGAL AKHIR : " + dateTo;

                worksheet.Cells["A" + 1 + ":D" + 1 + ""].Merge = true;
                worksheet.Cells["A" + 2 + ":D" + 2 + ""].Merge = true;
                worksheet.Cells["A" + 3 + ":D" + 3 + ""].Merge = true;
                worksheet.Cells["A" + 4 + ":D" + 4 + ""].Merge = true;
                worksheet.Cells["A" + 1 + ":D" + 5 + ""].Style.Font.Bold = true;
                worksheet.Cells["A5"].LoadFromDataTable(result, true);
                worksheet.Cells["A" + 5 + ":D" + (index + 5) + ""].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A" + 5 + ":D" + (index + 5) + ""].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A" + 5 + ":D" + (index + 5) + ""].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A" + 5 + ":D" + (index + 5) + ""].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                worksheet.Cells["A" + 1 + ":D" + (index + 5) + ""].AutoFitColumns();


                var stream = new MemoryStream();
                package.SaveAs(stream);
                string fileName = string.Concat("Laporan Order Belum Diproduksi Mesin", ".xlsx");

                return Tuple.Create(stream, fileName);
            }
            
        }

        public Tuple<List<MachineQueueReportViewModel>, int> Read(int page = 1, int size = 25, string filter = "{}")
        {
            var Query = MachineQueueReportLogic.GetQuery(filter);
            var data = Query.ToList();

            int TotalData = data.Count();
            return Tuple.Create(data, TotalData);
        }
        private class Filter
        {
            public string orderType { get; set; }
            public DateTimeOffset? dateFrom { get; set; }
            public DateTimeOffset? dateTo { get; set; }
            public string orderTypeName { get; set; }
        }
    }
}
