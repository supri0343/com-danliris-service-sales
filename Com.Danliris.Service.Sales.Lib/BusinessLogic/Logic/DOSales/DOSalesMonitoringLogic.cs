using Com.Danliris.Service.Sales.Lib.Models.DOSales;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Sales.Lib.ViewModels.DOSales;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.DOSales
{
    public class DOSalesMonitoringLogic : BaseMonitoringLogic<DOSalesMonitoringViewModel>
    {
        private SalesDbContext dbContext;
        private readonly IIdentityService identityService;

        public DOSalesMonitoringLogic(SalesDbContext dbContext, IIdentityService identityService)
        {
            this.dbContext = dbContext;
            this.identityService = identityService;
        }

        public override IQueryable<DOSalesMonitoringViewModel> GetQuery(string filterString)
        {
            Filter filter = JsonConvert.DeserializeObject<Filter>(filterString);

            IQueryable<DOSalesModel> dOSales = dbContext.DOSales;

            if (!string.IsNullOrWhiteSpace(filter.doSalesNo))
            {
                dOSales = dOSales.Where(cc => cc.DOSalesNo == filter.doSalesNo);
            }
            if (!string.IsNullOrWhiteSpace(filter.buyerName))
            {
                dOSales = dOSales.Where(cc => cc.BuyerName == filter.buyerName);
            }
            if (!string.IsNullOrWhiteSpace(filter.salesName))
            {
                dOSales = dOSales.Where(cc => cc.SalesName.ToUpper().Contains(filter.salesName.ToUpper()));
            }

            if (filter.dateStart != null)
            {
                var filterDate = filter.dateStart.GetValueOrDefault().ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).Date;
                dOSales = dOSales.Where(cc => cc.Date.AddHours(identityService.TimezoneOffset).Date >= filterDate);
            }
            if (filter.dateEnd != null)
            {
                var filterDate = filter.dateEnd.GetValueOrDefault().ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).AddDays(1).Date;
                dOSales = dOSales.Where(cc => cc.Date.AddHours(identityService.TimezoneOffset).Date < filterDate);
            }

            IQueryable<DOSalesDetailModel> doSalesItem = dbContext.DOSalesLocalItems;

            if (!string.IsNullOrWhiteSpace(filter.productionOrderNo))
            {
                doSalesItem=doSalesItem.Where(a => a.ProductionOrderNo == filter.productionOrderNo);
            }
            var grouping = (from a in dOSales
                           join doItem in doSalesItem on a.Id equals doItem.DOSalesModel.Id
                           select new DOSalesMonitoringViewModel {
                               PackingQty = doItem.ConvertionValue,
                               ProductionOrderNo = doItem.ProductionOrderNo,
                               ProductionOrderId = doItem.ProductionOrderId,
                               BuyerName = a.BuyerName,
                               BuyerType = a.BuyerType,
                               Date = a.Date,
                               DOSalesNo = a.DOSalesNo,
                               SalesName = a.SalesName,
                           })
                .GroupBy(s => s.ProductionOrderNo)
                .Select(x => new DOSalesMonitoringViewModel
                {
                    PackingQty = x.Sum(c => c.PackingQty),
                    ProductionOrderNo = x.First().ProductionOrderNo,
                    ProductionOrderId = x.First().ProductionOrderId,
                    BuyerName = x.First().BuyerName,
                    BuyerType = x.First().BuyerType,
                    Date = x.First().Date,
                    DOSalesNo = x.First().DOSalesNo,
                    SalesName = x.First().SalesName,
                });
            //IQueryable<DOSalesMonitoringViewModel> Query = from a in dOSales
            //                                               join doItem in doSalesItem on a.Id equals doItem.DOSalesModel.Id
            //                                               join po in dbContext.ProductionOrder on doItem.ProductionOrderId equals po.Id
            //                                               //join g in grouping on po.OrderNo equals g.ProductionOrderNo
            //                                               select new DOSalesMonitoringViewModel
            //                                               {
            //                                                   BuyerName=a.BuyerName,
            //                                                   BuyerType=a.BuyerType,
            //                                                   Date= a.Date,
            //                                                   DOSalesNo= a.DOSalesNo,
            //                                                   OrderQuantity= po.OrderQuantity,
            //                                                   PackingQty=doItem.Packing,
            //                                                   SalesName=a.SalesName,
            //                                                   ProductionOrderNo=doItem.ProductionOrderNo,
            //                                                   StockQty= po.OrderQuantity//-g.PackingQty
            //                                               };
            IQueryable<DOSalesMonitoringViewModel> Query = from a in grouping
                                                           join po in dbContext.ProductionOrder on a.ProductionOrderId equals po.Id
                                                           //join g in grouping on po.OrderNo equals g.ProductionOrderNo
                                                           select new DOSalesMonitoringViewModel
                                                           {
                                                               BuyerName = a.BuyerName,
                                                               BuyerType = a.BuyerType,
                                                               Date = a.Date,
                                                               DOSalesNo = a.DOSalesNo,
                                                               OrderQuantity = po.OrderQuantity,
                                                               PackingQty = a.PackingQty,
                                                               SalesName = a.SalesName,
                                                               ProductionOrderNo = a.ProductionOrderNo,
                                                               StockQty = po.OrderQuantity-a.PackingQty
                                                           };


            
            return Query;
        }

        private class Filter
        {
            public string doSalesNo { get; set; }
            public string productionOrderNo { get; set; }
            public string buyerName { get; set; }
            public string salesName { get; set; }
            public DateTimeOffset? dateStart { get; set; }
            public DateTimeOffset? dateEnd { get; set; }
        }
    }
}
