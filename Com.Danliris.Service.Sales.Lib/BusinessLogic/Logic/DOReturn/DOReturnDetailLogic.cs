using Com.Danliris.Service.Sales.Lib.Models.DOReturn;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.DOReturn
{
    public class DOReturnDetailLogic : BaseLogic<DOReturnDetailModel>
    {
        private DOReturnDetailItemLogic doReturnDetailItemLogic;
        private SalesDbContext _dbContext;

        public DOReturnDetailLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
        }

        public override void Create(DOReturnDetailModel model)
        {
            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
            foreach (var detailItem in model.DOReturnDetailItems)
            {
                EntityExtension.FlagForCreate(detailItem, IdentityService.Username, "sales-service");

            }
            base.Create(model);
        }

        public override void UpdateAsync(long id, DOReturnDetailModel model)
        {
            EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
            foreach (var detailItem in model.DOReturnDetailItems)
            {
                EntityExtension.FlagForUpdate(detailItem, IdentityService.Username, "sales-service");

            }
            base.UpdateAsync(id, model);
        }

        public override async Task DeleteAsync(long id)
        {
            var model = await ReadByIdAsync(id);
            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
            foreach (var detailItem in model.DOReturnDetailItems)
            {
                EntityExtension.FlagForDelete(detailItem, IdentityService.Username, "sales-service", true);

            }
            DbSet.Update(model);
        }

        //public override async void UpdateAsync(long id, DOReturnDetailModel model)
        //{
        //    try
        //    {
        //        if (model.DOReturnDetailItems != null)
        //        {
        //            HashSet<long> detailIds = doReturnDetailItemLogic.GetIds(id);
        //            foreach (var itemId in detailIds)
        //            {
        //                DOReturnDetailItemModel data = model.DOReturnDetailItems.FirstOrDefault(prop => prop.Id.Equals(itemId));
        //                if (data == null)
        //                    await doReturnDetailItemLogic.DeleteAsync(itemId);
        //                else
        //                {
        //                    doReturnDetailItemLogic.UpdateAsync(itemId, data);
        //                }
        //            }

        //            foreach (DOReturnDetailItemModel item in model.DOReturnDetailItems)
        //            {
        //                if (item.Id == 0)
        //                    doReturnDetailItemLogic.Create(item);
        //            }
        //        }

        //        EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
        //        DbSet.Update(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public override void Create(DOReturnDetailModel model)
        //{
        //    if (model.DOReturnDetailItems.Count > 0)
        //    {
        //        foreach (var detail in model.DOReturnDetailItems)
        //        {
        //            doReturnDetailItemLogic.Create(detail);
        //        }
        //    }

        //    EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
        //    DbSet.Add(model);
        //}

        //public override async Task DeleteAsync(long id)
        //{

        //    DOReturnDetailModel model = await ReadByIdAsync(id);

        //    foreach (var detail in model.DOReturnDetailItems)
        //    {
        //        await doReturnDetailItemLogic.DeleteAsync(detail.Id);
        //    }

        //    EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
        //    DbSet.Update(model);
        //}



        ///========================================================

        public override ReadResponse<DOReturnDetailModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<DOReturnDetailModel> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
              ""
            };

            Query = QueryHelper<DOReturnDetailModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<DOReturnDetailModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "SalesInvoiceId","SalesInvoiceNo",
            };

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<DOReturnDetailModel>.Order(Query, OrderDictionary);

            Pageable<DOReturnDetailModel> pageable = new Pageable<DOReturnDetailModel>(Query, page - 1, size);
            List<DOReturnDetailModel> data = pageable.Data.ToList<DOReturnDetailModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<DOReturnDetailModel>(data, totalData, OrderDictionary, SelectedFields);
        }
        public HashSet<long> GetIds(long id)
        {
            return new HashSet<long>(DbSet.Where(d => d.DOReturnModel.Id == id).Select(d => d.Id));
        }
    }
}
