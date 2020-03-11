using System;
using System.Collections.Generic;
using System.Linq;
using Com.Danliris.Service.Sales.Lib.Models.DOSales;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.NetCore.Lib;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.DOSales
{
    public class DOSalesLocalLogic : BaseLogic<DOSalesLocalModel>
    {
        public DOSalesLocalLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
        }

        public override ReadResponse<DOSalesLocalModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<DOSalesLocalModel> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
              ""
            };

            Query = QueryHelper<DOSalesLocalModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<DOSalesLocalModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id","ProductionOrderNo","MaterialConstruction","UnitOrCode","TotalPacking","TotalImperial","TotalMetric"
            };

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<DOSalesLocalModel>.Order(Query, OrderDictionary);

            Pageable<DOSalesLocalModel> pageable = new Pageable<DOSalesLocalModel>(Query, page - 1, size);
            List<DOSalesLocalModel> data = pageable.Data.ToList<DOSalesLocalModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<DOSalesLocalModel>(data, totalData, OrderDictionary, SelectedFields);
        }
        public HashSet<long> GetIds(long id)
        {
            return new HashSet<long>(DbSet.Where(d => d.DOSalesModel.Id == id).Select(d => d.Id));
        }
    }
}
