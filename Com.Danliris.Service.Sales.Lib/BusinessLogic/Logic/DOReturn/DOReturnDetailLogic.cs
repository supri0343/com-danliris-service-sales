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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.DOReturn
{
    public class DOReturnDetailLogic : BaseLogic<DOReturnDetailModel>
    {
        private DOReturnDetailItemLogic doReturnDetailItemLogic;
        private SalesDbContext _dbContext;

        public DOReturnDetailLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
            this.doReturnDetailItemLogic = serviceProvider.GetService<DOReturnDetailItemLogic>();
            _dbContext = dbContext;
        }

        public override void Create(DOReturnDetailModel detail)
        {
            EntityExtension.FlagForCreate(detail, IdentityService.Username, "sales-service");
            foreach (var detailItem in detail.DOReturnDetailItems)
            {
                EntityExtension.FlagForCreate(detailItem, IdentityService.Username, "sales-service");
                doReturnDetailItemLogic.Create(detailItem);

            }
            base.Create(detail);
        }

        public override async void UpdateAsync(long id, DOReturnDetailModel detail)
        {
            EntityExtension.FlagForUpdate(detail, IdentityService.Username, "sales-service");

            HashSet<long> detailIds = doReturnDetailItemLogic.GetIds(id);
            foreach (var itemId in detailIds)
            {
                DOReturnDetailItemModel data = detail.DOReturnDetailItems.FirstOrDefault(prop => prop.Id.Equals(itemId));
                if (data == null)
                    await doReturnDetailItemLogic.DeleteAsync(itemId);
                else
                {
                    doReturnDetailItemLogic.UpdateAsync(itemId, data);
                }
            }

            foreach (DOReturnDetailItemModel detailItem in detail.DOReturnDetailItems)
            {
                EntityExtension.FlagForUpdate(detailItem, IdentityService.Username, "sales-service");
                if (detailItem.Id == 0)
                    doReturnDetailItemLogic.Create(detailItem);
            }
            base.UpdateAsync(id, detail);
        }

        public override async Task DeleteAsync(long id)
        {
            var detail = await ReadByIdAsync(id);
            EntityExtension.FlagForDelete(detail, IdentityService.Username, "sales-service", true);
            foreach (var detailItem in detail.DOReturnDetailItems)
            {
                await doReturnDetailItemLogic.DeleteAsync(detailItem.Id);

            }
            DbSet.Update(detail);
        }

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

        public override async Task<DOReturnDetailModel> ReadByIdAsync(long id)
        {
            var SalesInvoice = await DbSet.Include(s => s.DOReturnDetailItems).ThenInclude(s => s.DOReturnItems).FirstOrDefaultAsync(s => s.Id == id);
            return SalesInvoice;
        }
    }
}
