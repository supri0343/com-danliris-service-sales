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
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.DOReturn
{
    public class DOReturnLogic : BaseLogic<DOReturnModel>
    {
        private DOReturnDetailLogic doReturnDetailLogic;

        public DOReturnLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
        }

        public override ReadResponse<DOReturnModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<DOReturnModel> Query = DbSet.Include(x => x.DOReturnDetails);

            List<string> SearchAttributes = new List<string>()
            {
                "DOReturnNo","Type",
            };

            Query = QueryHelper<DOReturnModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<DOReturnModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id","Code","DOReturnNo","Type","Date","ReturnFrom","LKTPNo","HeadOfStorage","Remark",
            };

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<DOReturnModel>.Order(Query, OrderDictionary);

            Pageable<DOReturnModel> pageable = new Pageable<DOReturnModel>(Query, page - 1, size);
            List<DOReturnModel> data = pageable.Data.ToList<DOReturnModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<DOReturnModel>(data, totalData, OrderDictionary, SelectedFields);
        }

        public override void Create(DOReturnModel model)
        {
            if (model.DOReturnDetails.Count > 0)
            {
                EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
                foreach (var detail in model.DOReturnDetails)
                {
                    EntityExtension.FlagForCreate(detail, IdentityService.Username, "sales-service");
                    foreach (var detailItem in detail.DOReturnDetailItems)
                    {
                        EntityExtension.FlagForCreate(detailItem, IdentityService.Username, "sales-service");
                        foreach (var item in detailItem.DOReturnItems)
                        {
                            EntityExtension.FlagForCreate(item, IdentityService.Username, "sales-service");
                        }
                    }
                }
            }
            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
            DbSet.Add(model);
        }

        public override async void UpdateAsync(long id, DOReturnModel model)
        {
            try
            {
                if (model.DOReturnDetails != null)
                {
                    HashSet<long> detailIds = doReturnDetailLogic.GetIds(id);
                    foreach (var itemId in detailIds)
                    {
                        DOReturnDetailModel data = model.DOReturnDetails.FirstOrDefault(prop => prop.Id.Equals(itemId));
                        if (data == null) 
                        {
                            foreach (var detail in model.DOReturnDetails)
                            {
                                EntityExtension.FlagForDelete(detail, IdentityService.Username, "sales-service", true);
                                foreach (var detailItem in detail.DOReturnDetailItems)
                                {
                                    EntityExtension.FlagForDelete(detailItem, IdentityService.Username, "sales-service", true);
                                    foreach (var item in detailItem.DOReturnItems)
                                    {
                                        EntityExtension.FlagForDelete(item, IdentityService.Username, "sales-service", true);
                                    }
                                }
                            }
                            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
                            DbSet.Update(model);
                        }
                        else
                        {
                            doReturnDetailLogic.UpdateAsync(itemId, data);
                        }
                    }

                    foreach (DOReturnDetailModel item in model.DOReturnDetails)
                    {
                        if (item.Id == 0)
                            doReturnDetailLogic.Create(item);
                    }
                }

                EntityExtension.FlagForUpdate(model, IdentityService.Username, "sales-service");
                DbSet.Update(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override async Task DeleteAsync(long id)
        {

            DOReturnModel model = await ReadByIdAsync(id);

            foreach (var detail in model.DOReturnDetails)
            {
                EntityExtension.FlagForDelete(detail, IdentityService.Username, "sales-service", true);
                foreach (var detailItem in detail.DOReturnDetailItems)
                {
                    EntityExtension.FlagForDelete(detailItem, IdentityService.Username, "sales-service", true);
                    foreach (var item in detailItem.DOReturnItems)
                    {
                        EntityExtension.FlagForDelete(item, IdentityService.Username, "sales-service", true);
                    }
                }
            }
            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
            DbSet.Update(model);
        }

        public override async Task<DOReturnModel> ReadByIdAsync(long id)
        {
            var SalesInvoice = await DbSet.Include(s => s.DOReturnDetails).ThenInclude(s => s.DOReturnDetailItems).ThenInclude(s => s.DOReturnItems).FirstOrDefaultAsync(s => s.Id == id);
            return SalesInvoice;
        }

    }
}
