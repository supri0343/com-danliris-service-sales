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

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "No SPP", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Panjang Order", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Delivery", DataType = typeof(String) });

            Dictionary<string, string> Rowcount = new Dictionary<string, string>();
            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", ""); 
            else
            {
                var index = 0;
                
                foreach (MachineQueueReportViewModel item in Query.ToList())
                {
                    index++;
                    string length = item.orderLength + " " + item.UomUnit;
                    string date = item.DeliveryDate.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    result.Rows.Add(index, item.SPPNo, length, date);
                }
            }
            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("LAPORAN ORDER BELUM DIPRODUKSI MESIN");
            sheet.Cells["A1"].LoadFromDataTable(result, true, OfficeOpenXml.Table.TableStyles.Light16);

            sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            MemoryStream streamExcel = new MemoryStream();
            package.SaveAs(streamExcel);

            string fileName = string.Concat("Budget Export Garment", ".xlsx");

            return Tuple.Create(streamExcel, fileName);
        }

        public Tuple<List<MachineQueueReportViewModel>, int> Read(int page = 1, int size = 25, string filter = "{}")
        {
            var Query = MachineQueueReportLogic.GetQuery(filter);
            var data = Query.ToList();

            int TotalData = data.Count();
            return Tuple.Create(data, TotalData);
        }
    }
}
