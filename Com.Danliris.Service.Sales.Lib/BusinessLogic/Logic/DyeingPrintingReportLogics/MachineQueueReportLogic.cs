using Com.Danliris.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.Models.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Sales.Lib.ViewModels.DyeingPrintingReport;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.DyeingPrintingReportLogics
{
    public class MachineQueueReportLogic : BaseMonitoringLogic<MachineQueueReportViewModel>
    {
        private IIdentityService identityService;
        private SalesDbContext dbContext;
        private DbSet<ProductionOrderModel> dbSet;
        private DbSet<FinishingPrintingSalesContractModel> scDbSet;

        public MachineQueueReportLogic(IIdentityService identityService, SalesDbContext dbContext)
        {
            this.identityService = identityService;
            this.dbContext = dbContext;
            dbSet = dbContext.Set<ProductionOrderModel>();
            scDbSet = dbContext.Set<FinishingPrintingSalesContractModel>();
        }

        public override IQueryable<MachineQueueReportViewModel> GetQuery(string filter)
        {
            Filter _filter = JsonConvert.DeserializeObject<Filter>(filter);

            IQueryable<ProductionOrderModel> Query = dbSet;
            IQueryable<FinishingPrintingSalesContractModel> SCQuery = scDbSet;

            if (!string.IsNullOrWhiteSpace(_filter.orderType))
            {
                Query = Query.Where(spp=>spp.OrderTypeName== _filter.orderType);
            }

            if (_filter.dateFrom != null)
            {
                var filterDate = _filter.dateFrom.GetValueOrDefault().ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).Date;
                SCQuery = SCQuery.Where(cc => cc.CreatedUtc.AddHours(identityService.TimezoneOffset).Date >= filterDate);
            }
            if (_filter.dateTo != null)
            {
                var filterDate = _filter.dateTo.GetValueOrDefault().ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).AddDays(1).Date;
                SCQuery = SCQuery.Where(cc => cc.CreatedUtc.AddHours(identityService.TimezoneOffset).Date < filterDate);
            }

            var newQ = (from a in Query
                        join b in SCQuery on a.SalesContractNo equals b.SalesContractNo
                        //where b.CategoryName != "PROCESS" && a.IsApprovedKadivMD == true
                        select new MachineQueueReportViewModel
                        {
                            SPPNo = a.OrderNo,
                            orderLength = a.OrderQuantity,
                            UomUnit = a.UomUnit,
                            DeliveryDate = a.DeliveryDate
                        });

            return newQ;
        }

        private class Filter
        {
            public string orderType { get; set; }
            public DateTimeOffset? dateFrom { get; set; }
            public DateTimeOffset? dateTo { get; set; }
        }
    }
}
