using System;
using System.Collections.Generic;
using System.Linq;
using Com.Danliris.Service.Sales.Lib.Models.SalesInvoice;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.NetCore.Lib;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.SalesInvoice
{
    public class SalesInvoiceDetailLogic : BaseLogic<SalesInvoiceDetailModel>
    {
        public SalesInvoiceDetailLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
        }

        public override ReadResponse<SalesInvoiceDetailModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<SalesInvoiceDetailModel> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
              ""
            };

            Query = QueryHelper<SalesInvoiceDetailModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<SalesInvoiceDetailModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id","Quantity","Total","UomId","UomUnit","UnitCode","UnitName","UnitPrice","Amount","LastModifiedUtc"
            };

            Query = Query
                .Select(field => new SalesInvoiceDetailModel
                {
                    Id = field.Id,
                    Quantity = field.Quantity,
                    Total = field.Total,
                    UomId = field.UomId,
                    UomUnit = field.UomUnit,
                    UnitCode = field.UnitCode,
                    UnitName = field.UnitName,
                    UnitPrice = field.UnitPrice,
                    Amount = field.Amount,
                    LastModifiedUtc = field.LastModifiedUtc,
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<SalesInvoiceDetailModel>.Order(Query, OrderDictionary);

            Pageable<SalesInvoiceDetailModel> pageable = new Pageable<SalesInvoiceDetailModel>(Query, page - 1, size);
            List<SalesInvoiceDetailModel> data = pageable.Data.ToList<SalesInvoiceDetailModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<SalesInvoiceDetailModel>(data, totalData, OrderDictionary, SelectedFields);
        }
        public HashSet<long> GetIds(long id)
        {
            return new HashSet<long>(DbSet.Where(d => d.SalesInvoiceModel.Id == id).Select(d => d.Id));
        }
    }
}
