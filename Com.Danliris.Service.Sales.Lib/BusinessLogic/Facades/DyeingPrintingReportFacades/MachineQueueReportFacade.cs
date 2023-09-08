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
using System.Threading.Tasks;
using Com.Danliris.Service.Sales.Lib.Helpers;
using System.Net.Http;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.DyeingPrintingReportFacades
{
    public class MachineQueueReportFacade : IMachineQueueReport
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<ProductionOrderModel> dbSet;
        private readonly DbSet<FinishingPrintingSalesContractModel> scDbSet;
        private IdentityService IdentityService;
        private MachineQueueReportLogic MachineQueueReportLogic;
        private readonly IServiceProvider _serviceProvider;

        public MachineQueueReportFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            this.DbContext = dbContext;
            _serviceProvider = serviceProvider;
            this.dbSet = this.DbContext.Set<ProductionOrderModel>();
            this.scDbSet = this.DbContext.Set<FinishingPrintingSalesContractModel>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
            this.MachineQueueReportLogic = serviceProvider.GetService<MachineQueueReportLogic>();
        }

        public async Task<Tuple<MemoryStream,string>> GenerateExcel(string filter = "{}")
        {
            var Query = MachineQueueReportLogic.GetQuery(filter);
            var data = Query.ToList();
            List<string> noOrder = new List<string>();
            List<MachineQueueReportViewModel> reportData = new List<MachineQueueReportViewModel>();
            foreach (var a in data)
            {
                noOrder.Add(a.SPPNo + ";" + a.OrderType + ";" + a.ProcessType);
            }
            var production = await GetProductionByOrderNo(noOrder);
            var productionResults = production.data;
            foreach (var i in data)
            {
                var inProd = productionResults.Where(a => a.noorder == i.SPPNo).FirstOrDefault();
                if (inProd == null)
                {
                    reportData.Add(i);
                }
            }
            DataTable result = new DataTable();
            var offset = 7;
            Filter _filter = JsonConvert.DeserializeObject<Filter>(filter);

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "No SPP", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Panjang Order", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Delivery", DataType = typeof(String) });

            Dictionary<string, string> Rowcount = new Dictionary<string, string>();
            var index = 0;

            if (reportData.Count() == 0)
                result.Rows.Add("", "", "", ""); 
            else
            {
                foreach (MachineQueueReportViewModel item in reportData)
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

        public async Task<Tuple<List<MachineQueueReportViewModel>, int>> Read(int page = 1, int size = 25, string filter = "{}")
        {
            var Query = MachineQueueReportLogic.GetQuery(filter);
            var data = Query.ToList();
            List<string> noOrder = new List<string>();
            List<MachineQueueReportViewModel> reportData = new List<MachineQueueReportViewModel>();
            foreach (var a in data)
            {
                noOrder.Add(a.SPPNo + ";" + a.OrderType + ";" + a.ProcessType);
            }
            var production = await GetProductionByOrderNo(noOrder);
            var productionResults = production.data;
            foreach(var i in data)
            {
                var inProd = productionResults.Where(a => a.noorder == i.SPPNo).FirstOrDefault();
                if (inProd == null)
                {
                    reportData.Add(i);
                }
            }

            int TotalData = reportData.Count();
            return  Tuple.Create(reportData, TotalData);
        }
        private class Filter
        {
            public string orderType { get; set; }
            public DateTimeOffset? dateFrom { get; set; }
            public DateTimeOffset? dateTo { get; set; }
            public string orderTypeName { get; set; }
        }

        public class ProductionVM
        {
            public double qtyin { get; set; }
            public string noorder { get; set; }
        }
        public class APiResult
        {
            public string apiVersion { get; set; }
            public List<ProductionVM> data { get; set; }
            public object info { get; set; }
            public string message { get; set; }
            public string statusCode { get; set; }
        }


        public async Task<APiResult> GetProductionByOrderNo(List<string> orderno)
        {
            APiResult spp = new APiResult();

            var filter = string.Join(",", orderno.Distinct());
            var dpUri = $"GetProductionOsthoffStatusOrder";
            IHttpClientService httpClient = (IHttpClientService)_serviceProvider.GetService(typeof(IHttpClientService));
            var garmentProductionUri = APIEndpoint.DyeingPrinting + dpUri;
            var response = await httpClient.SendAsync(HttpMethod.Get, garmentProductionUri, new StringContent(JsonConvert.SerializeObject(filter), Encoding.Unicode, "application/json"));
            spp.data = new List<ProductionVM>();

            if (response.IsSuccessStatusCode)
            {
                var contentString = await response.Content.ReadAsStringAsync();
                Dictionary<string, object> content = JsonConvert.DeserializeObject<Dictionary<string, object>>(contentString);
                var dataString = content.GetValueOrDefault("data").ToString();

                var listdata = JsonConvert.DeserializeObject<List<ProductionVM>>(dataString);

                foreach (var i in listdata)
                {
                    spp.data.Add(i);
                }
            }

            return spp;
        }
    }
}
