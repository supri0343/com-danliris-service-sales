using Com.Danliris.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.CostCalculationGarments
{
    public class BudgetExportGarmentReportLogic : BaseMonitoringLogic<BudgetExportGarmentReportViewModel>
    {
        private IIdentityService identityService;
        private SalesDbContext dbContext;
        private DbSet<CostCalculationGarment> dbSet;

        public BudgetExportGarmentReportLogic(IIdentityService identityService, SalesDbContext dbContext)
        {
            this.identityService = identityService;
            this.dbContext = dbContext;
            dbSet = dbContext.Set<CostCalculationGarment>();
        }

        public override IQueryable<BudgetExportGarmentReportViewModel> GetQuery(string filter)
        {
            Dictionary<string, object> FilterDictionary = new Dictionary<string, object>(JsonConvert.DeserializeObject<Dictionary<string, object>>(filter), StringComparer.OrdinalIgnoreCase);

            IQueryable<CostCalculationGarment> Query = dbSet;

            try
            {
                var dateFrom = (DateTime)(FilterDictionary["dateFrom"]);
                var dateTo = (DateTime)(FilterDictionary["dateTo"]);

                Query = dbSet.Where(d => d.DeliveryDate >= dateFrom &&
                                         d.DeliveryDate <= dateTo
                );
            }
            catch (KeyNotFoundException e)
            {
                throw new Exception(e.Message);
            }

            if (FilterDictionary.TryGetValue("unitName", out object unitName))
            {
                Query = Query.Where(d => d.UnitName == unitName.ToString());
            }

            if (FilterDictionary.TryGetValue("section", out object section))
            {
                Query = Query.Where(d => d.Section == section.ToString());
            }

            Query = Query.OrderBy(o => o.RO_Number).ThenBy(o => o.BuyerBrandCode);

            var newQ = (from a in Query
                        join b in dbContext.CostCalculationGarment_Materials on a.Id equals b.CostCalculationGarmentId 
                        where b.CategoryName != "PROCESS" && a.IsApprovedKadivMD == true

                        select new BudgetExportGarmentReportViewModel
                        {
                            RO_Number = a.RO_Number,
                            UnitName = a.UnitCode + "-" + a.UnitName,
                            Section = a.Section,
                            BuyerCode = a.BuyerBrandCode,
                            BuyerName = a.BuyerBrandName,
                            Article = a.Article,
                            DeliveryDate = a.DeliveryDate,
                            PONumber = b.PO_SerialNumber,
                            CategoryName = b.CategoryName,
                            ProductCode = b.ProductCode,
                            ProductName = b.Description,
                            BudgetQuantity = b.BudgetQuantity,
                            BudgetUOM = b.UOMPriceName,
                            BudgetPrice = b.Price,
                            BudgetAmount = b.Price * b.BudgetQuantity, 
                        });

            return newQ;
        }
    }
}
