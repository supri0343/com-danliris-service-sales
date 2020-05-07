using Com.Danliris.Service.Sales.Lib.Models.DOReturn;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.DOReturn
{
    public class DOReturnDetailItemLogic : BaseLogic<DOReturnDetailItemModel>
    {
        public DOReturnDetailItemLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
        }

        public override void Create(DOReturnDetailItemModel detailItem)
        {
            EntityExtension.FlagForCreate(detailItem, IdentityService.Username, "sales-service");
            foreach (var item in detailItem.DOReturnItems)
            {
                EntityExtension.FlagForCreate(item, IdentityService.Username, "sales-service");

            }
            base.Create(detailItem);
        }

        public override void UpdateAsync(long id, DOReturnDetailItemModel detailItem)
        {
            EntityExtension.FlagForUpdate(detailItem, IdentityService.Username, "sales-service");
            foreach (var item in detailItem.DOReturnItems)
            {
                EntityExtension.FlagForUpdate(item, IdentityService.Username, "sales-service");

            }
            base.UpdateAsync(id, detailItem);
        }

        public override async Task DeleteAsync(long id)
        {
            var detailItem = await ReadByIdAsync(id);
            EntityExtension.FlagForDelete(detailItem, IdentityService.Username, "sales-service", true);
            foreach (var item in detailItem.DOReturnItems)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, "sales-service", true);

            }
            DbSet.Update(detailItem);
        }

        public override ReadResponse<DOReturnDetailItemModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<DOReturnDetailItemModel> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
              ""
            };

            Query = QueryHelper<DOReturnDetailItemModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<DOReturnDetailItemModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "ShipmentDocumentId","ShipmentDocumentCode",
            };

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<DOReturnDetailItemModel>.Order(Query, OrderDictionary);

            Pageable<DOReturnDetailItemModel> pageable = new Pageable<DOReturnDetailItemModel>(Query, page - 1, size);
            List<DOReturnDetailItemModel> data = pageable.Data.ToList<DOReturnDetailItemModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<DOReturnDetailItemModel>(data, totalData, OrderDictionary, SelectedFields);
        }

        public HashSet<long> GetIds(long id)
        {
            return new HashSet<long>(DbSet.Where(d => d.DOReturnDetailModel.Id == id).Select(d => d.Id));
        }
    }
}
