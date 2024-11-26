using System;
using System.Collections.Generic;
using System.Linq;
using Com.Danliris.Service.Sales.Lib.Models.GarmentMasterPlan.WeeklyPlanModels;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MonitoringViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.MonitoringLogics
{
    public class MonitoringRemainingEHLogic : BaseMonitoringLogic<MonitoringRemainingEHViewModel>
    {
        private IIdentityService identityService;
        private SalesDbContext dbContext;
        private DbSet<GarmentWeeklyPlan> dbSet;

        public MonitoringRemainingEHLogic(IIdentityService identityService, SalesDbContext dbContext)
        {
            this.identityService = identityService;
            this.dbContext = dbContext;
            dbSet = dbContext.Set<GarmentWeeklyPlan>();
        }

        public override IQueryable<MonitoringRemainingEHViewModel> GetQuery(string filter)
        {
            Dictionary<string, string> FilterDictionary = new Dictionary<string, string>(JsonConvert.DeserializeObject<Dictionary<string, string>>(filter), StringComparer.OrdinalIgnoreCase);

            IQueryable<GarmentWeeklyPlan> Query = dbSet;//.Include(i => i.Items);

            try
            {
                var year = short.Parse(FilterDictionary["year"]);
                Query = dbSet.Where(d => d.Year == year);
            }
            catch (KeyNotFoundException e)
            {
                throw new Exception(string.Concat("[year]", e.Message));
            }

            if (FilterDictionary.TryGetValue("unit", out string unit))
            {
                Query = Query.Where(d => d.UnitCode == unit);
            }

            var result = (from a in Query
                          join b in dbContext.GarmentWeeklyPlanItems on a.Id equals b.WeeklyPlanId
                          group new { b.Operator, b.RemainingEH } by new { a.UnitCode, b.WeekNumber } into data
                          select new
                          {
                              UnitCode=data.Key.UnitCode,
                              weeknumber= data.Key.WeekNumber,
                              op=data.Sum(x=>x.Operator),
                              eh= data.Sum(x=>x.RemainingEH),
                          });

            var groupedData = Query.GroupBy(d => d.UnitCode).Select( s => s.FirstOrDefault().UnitCode).ToList();

            var datas = groupedData.Select(group => new MonitoringRemainingEHViewModel
            {
                Unit = group,
                Items = result
                .Where(r => r.UnitCode == group) // Filter berdasarkan UnitCode grup
                .Select(r => new MonitoringRemainingEHItemViewModel
                {
                    WeekNumber = r.weeknumber,
                    Operator = r.op,
                    RemainingEH = r.eh
                })
                .ToList()
            }).AsQueryable();
            return datas;
        }
    }
}
