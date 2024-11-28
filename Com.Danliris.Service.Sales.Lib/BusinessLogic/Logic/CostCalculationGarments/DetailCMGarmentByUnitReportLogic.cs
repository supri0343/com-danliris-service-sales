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
    public class DetailCMGarmentByUnitReportLogic : BaseMonitoringLogic<DetailCMGarmentByUnitReportViewModel>
    {
        private IIdentityService identityService;
        private SalesDbContext dbContext;
        private DbSet<CostCalculationGarment> dbSet;

        public DetailCMGarmentByUnitReportLogic(IIdentityService identityService, SalesDbContext dbContext)
        {
            this.identityService = identityService;
            this.dbContext = dbContext;
            dbSet = dbContext.Set<CostCalculationGarment>();
        }

        public override IQueryable<DetailCMGarmentByUnitReportViewModel> GetQuery(string filter)
        {
            Dictionary<string, object> FilterDictionary = new Dictionary<string, object>(JsonConvert.DeserializeObject<Dictionary<string, object>>(filter), StringComparer.OrdinalIgnoreCase);

            IQueryable<CostCalculationGarment> Query = dbSet;

            try
            {
                var dateFrom = (DateTime) (FilterDictionary["dateFrom"]);
                var dateTo= (DateTime) (FilterDictionary["dateTo"]);

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

            Query = Query.OrderBy(o => o.UnitName).ThenBy(o => o.BuyerBrandName);

            var newQ = (from a in Query
                        join b in dbContext.CostCalculationGarment_Materials on a.Id equals b.CostCalculationGarmentId
                        where b.CategoryName != "PROCESS"

                        select new TempDetailCMGarmentByUnitReportViewModel
                        {                  
                            UnitName = a.UnitName,
                            BuyerCode = a.BuyerCode,
                            BuyerName = a.BuyerName,
                            BuyerBrandCode = a.BuyerBrandCode,
                            BuyerBrandName = a.BuyerBrandName,
                            Comodity = a.ComodityCode + " - " + a.Commodity,
                            RO_Number = a.RO_Number,
                            Article = a.Article,
                            Quantity = a.Quantity,
                            UOMUnit = a.UOMUnit,
                            DeliveryDate = a.DeliveryDate,
                            OTL1CalculatedRate = a.OTL1CalculatedRate,
                            OTL2CalculatedRate = a.OTL2CalculatedRate,
                            SMV_Cutting = a.SMV_Cutting,
                            SMV_Sewing = a.SMV_Sewing,
                            SMV_Finishing = a.SMV_Finishing,
                            SMV_Total = a.SMV_Total,
                            CommissionRate = a.CommissionRate,
                            Insurance = a.Insurance,
                            Freight = a.Freight,
                            ConfirmPrice = a.ConfirmPrice,
                            RateValue = a.RateValue,
                            BudgetAmount = b.isFabricCM == true && b.Price > 0 ? 0 : b.Price * b.BudgetQuantity,
                            CMPrice = b.CM_Price.GetValueOrDefault(),
                            CMIDR = 0,
                            CM = 0, 
                            FOB_Price = 0
                        });

                        // GROUPING DATA

                        var newQ1 = (from c in newQ
                                     group new { BgtAmt = c.BudgetAmount, CMP = c.CMPrice } by new { c.UnitName, c.BuyerCode, c.BuyerName, c.BuyerBrandCode, c.BuyerBrandName, c.Comodity,
                                     c.RO_Number, c.Article, c.Quantity, c.UOMUnit, c.DeliveryDate, c.OTL1CalculatedRate, c.OTL2CalculatedRate, c.SMV_Cutting, c.SMV_Sewing,
                                     c.SMV_Finishing, c.SMV_Total, c.CommissionRate, c.Insurance, c.Freight, c.ConfirmPrice, c.RateValue} into G

                        select new DetailCMGarmentByUnitReportViewModel
                         {
                            UnitName = G.Key.UnitName,
                            BuyerCode = G.Key.BuyerCode,
                            BuyerName = G.Key.BuyerName,
                            BrandCode = G.Key.BuyerBrandCode,
                            BrandName = G.Key.BuyerBrandName,
                            Comodity = G.Key.Comodity,
                            RO_Number = G.Key.RO_Number,
                            Article = G.Key.Article,
                            Quantity = G.Key.Quantity,
                            UOMUnit = G.Key.UOMUnit,
                            DeliveryDate = G.Key.DeliveryDate,
                            OTL1 = G.Key.OTL1CalculatedRate,
                            OTL2 = G.Key.OTL2CalculatedRate,
                            SMV_Cutting = G.Key.SMV_Cutting,
                            SMV_Sewing = G.Key.SMV_Sewing,
                            SMV_Finishing = G.Key.SMV_Finishing,
                            SMV_Total = G.Key.SMV_Total,
                            Commission = G.Key.CommissionRate,
                            Insurance = G.Key.Insurance,
                            Freight = G.Key.Freight,
                            ConfirmPrice = G.Key.ConfirmPrice,
                            CurrencyRate = G.Key.RateValue,
                            BudgetAmount = Math.Round(G.Sum(m => m.BgtAmt), 2),
                            CMPrice = Math.Round(G.Sum(m => m.CMP), 2),
                            CMIDR = (G.Key.ConfirmPrice * G.Key.RateValue) - G.Key.CommissionRate - (Math.Round(G.Sum(m => m.BgtAmt), 2) / G.Key.Quantity),
                            CM = ((G.Key.ConfirmPrice * G.Key.RateValue) - G.Key.CommissionRate - (Math.Round(G.Sum(m => m.BgtAmt), 2) / G.Key.Quantity)) / G.Key.RateValue,
                            FOB_Price = G.Key.ConfirmPrice + ((Math.Round(G.Sum(m => m.CMP), 2) / G.Key.RateValue) * 1.05) - (G.Key.Insurance + G.Key.Freight),
                        });
            return newQ1;
        }
    }
}
