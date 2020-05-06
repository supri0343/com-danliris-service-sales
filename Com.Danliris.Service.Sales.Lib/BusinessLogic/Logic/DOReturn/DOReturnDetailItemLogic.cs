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

        public override void Create(DOReturnDetailItemModel model)
        {
            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
            foreach (var item in model.DOReturnItems)
            {
                EntityExtension.FlagForCreate(item, IdentityService.Username, "sales-service");

            }
            base.Create(model);
        }

        public override void UpdateAsync(long id, DOReturnDetailItemModel model)
        {
            EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
            foreach (var item in model.DOReturnItems)
            {
                EntityExtension.FlagForUpdate(item, IdentityService.Username, "sales-service");

            }
            base.UpdateAsync(id, model);
        }

        public override async Task DeleteAsync(long id)
        {
            var model = await ReadByIdAsync(id);
            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
            foreach (var item in model.DOReturnItems)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, "sales-service", true);

            }
            DbSet.Update(model);
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
