using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.DOSales;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.DOSales;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.DOSales;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Data;
using System.Globalization;
using Com.Danliris.Service.Sales.Lib.Helpers;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.DOSales
{
    public class DOSalesMonitoringFacade : IDOSalesMonitoring
    {
        private DOSalesMonitoringLogic logic;
        private IIdentityService identityService;

        public DOSalesMonitoringFacade(IServiceProvider serviceProvider)
        {
            logic = serviceProvider.GetService<DOSalesMonitoringLogic>();
            identityService = serviceProvider.GetService<IIdentityService>();
        }
        public Tuple<MemoryStream, string> GenerateExcel(string filter = "{}")
        {
            var Query = logic.GetQuery(filter);
            var data = Query.OrderBy(a => a.Date).ThenBy(a => a.DOSalesNo).ToList();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(int) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "No DO", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Tanggal DO", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Type DO", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Nama Buyer", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "No SPP", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Nama Sales", DataType = typeof(string) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Qty Order", DataType = typeof(double) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Qty Terkirim", DataType = typeof(double) });
            dataTable.Columns.Add(new DataColumn() { ColumnName = "Stok", DataType = typeof(double) });

            List<(string, Enum, Enum)> mergeCells = new List<(string, Enum, Enum)>() { };

            if (data != null && data.Count > 0)
            {
                int i = 0;
                foreach (var d in data)
                {
                    dataTable.Rows.Add(++i, d.DOSalesNo, d.Date.ToString("dd MMMM yyyy", new CultureInfo("id-ID")), d.BuyerType, d.BuyerName, d.ProductionOrderNo, d.SalesName, d.OrderQuantity, d.PackingQty, d.StockQty);
                }
            }
            else
            {
                dataTable.Rows.Add(null, null, null, null, null, null, null, null, null,null);
            }

            var excel = Excel.CreateExcel(new List<(DataTable, string, List<(string, Enum, Enum)>)>() { (dataTable, "Monitoring DO Penjualan Dyeing Printing", mergeCells) }, false);

            return Tuple.Create(excel, string.Concat("Monitoring DO Penjualan Dyeing Printing"));
        }

        public Tuple<List<DOSalesMonitoringViewModel>, int> Read(int page = 1, int size = 25, string filter = "{}")
        {
            var Query = logic.GetQuery(filter);
            var data = GetData(Query.OrderBy(a => a.Date).ThenBy(a=>a.DOSalesNo));

            return Tuple.Create(data, data.Count);
        }

        private List<DOSalesMonitoringViewModel> GetData(IEnumerable<DOSalesMonitoringViewModel> model)
        {
            var data = model.Select(a => new DOSalesMonitoringViewModel
            {
                BuyerName = a.BuyerName,
                BuyerType = a.BuyerType,
                Date = a.Date.ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).Date,
                DOSalesNo = a.DOSalesNo,
                OrderQuantity = a.OrderQuantity,
                PackingQty = a.PackingQty,
                SalesName = a.SalesName,
                ProductionOrderNo = a.ProductionOrderNo,
                StockQty = a.StockQty
            }).ToList();

            return data;
        }

    }
}
