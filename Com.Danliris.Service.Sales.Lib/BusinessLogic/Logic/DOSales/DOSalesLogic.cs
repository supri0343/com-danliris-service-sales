using Com.Danliris.Service.Sales.Lib.Models.DOSales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.DOSales
{
    public class DOSalesLogic : BaseLogic<DOSalesModel>
    {
        private DOSalesLocalLogic doSalesLocalLogic;
        private SalesDbContext _dbContext;

        public DOSalesLogic(IServiceProvider serviceProvider, IIdentityService identityService, SalesDbContext dbContext) : base(identityService, serviceProvider, dbContext)
        {
            this.doSalesLocalLogic = serviceProvider.GetService<DOSalesLocalLogic>();
            _dbContext = dbContext;
        }

        public override ReadResponse<DOSalesModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<DOSalesModel> Query = DbSet.Include(x => x.DOSalesLocalItems);

            List<string> SearchAttributes = new List<string>()
            {
                "Code","DOSalesNo","DOSalesType",
                //"LocalType","ExportType"
            };

            Query = QueryHelper<DOSalesModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<DOSalesModel>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id","Code","DOSalesNo","DOSalesType","Status","Accepted","Declined",
                "LocalType","LocalDate","LocalSalesContract","LocalBuyer","DestinationBuyerName","DestinationBuyerAddress","SalesName","LocalHeadOfStorage","PackingUom","MetricUom","ImperialUom","Disp","Op","Sc","LocalRemark",
                "ExportType","ExportDate","DoneBy","ExportSalesContract","LocalMaterialConstruction","ExportMaterialConstruction","ExportBuyer","Commodity","FillEachBale","ExportRemark"
            };

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<DOSalesModel>.Order(Query, OrderDictionary);

            Pageable<DOSalesModel> pageable = new Pageable<DOSalesModel>(Query, page - 1, size);
            List<DOSalesModel> data = pageable.Data.ToList<DOSalesModel>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<DOSalesModel>(data, totalData, OrderDictionary, SelectedFields);
        }

        public override async void UpdateAsync(long id, DOSalesModel model)
        {
            try
            {
                if (model.DOSalesLocalItems != null)
                {
                    HashSet<long> detailIds = doSalesLocalLogic.GetIds(id);
                    foreach (var itemId in detailIds)
                    {
                        DOSalesLocalModel data = model.DOSalesLocalItems.FirstOrDefault(prop => prop.Id.Equals(itemId));
                        if (data == null)
                            await doSalesLocalLogic.DeleteAsync(itemId);
                        else
                        {
                            doSalesLocalLogic.UpdateAsync(itemId, data);
                        }
                    }

                    foreach (DOSalesLocalModel item in model.DOSalesLocalItems)
                    {
                        if (item.Id == 0)
                            doSalesLocalLogic.Create(item);
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

        public override void Create(DOSalesModel model)
        {
            if (model.DOSalesLocalItems.Count > 0)
            {
                foreach (var detail in model.DOSalesLocalItems)
                {
                    doSalesLocalLogic.Create(detail);
                }
            }

            EntityExtension.FlagForCreate(model, IdentityService.Username, "sales-service");
            DbSet.Add(model);
        }

        public override async Task DeleteAsync(long id)
        {

            DOSalesModel model = await ReadByIdAsync(id);

            foreach (var detail in model.DOSalesLocalItems)
            {
                await doSalesLocalLogic.DeleteAsync(detail.Id);
            }

            EntityExtension.FlagForDelete(model, IdentityService.Username, "sales-service", true);
            DbSet.Update(model);
        }

        public override async Task<DOSalesModel> ReadByIdAsync(long id)
        {
            //var DOSales = await DbSet.Where(p => p.DOSalesLocalItems.Select(d => d.DOSalesModel.Id)
            //.Contains(p.Id)).Include(p => p.DOSalesLocalItems).FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
            //DOSales.DOSalesLocalItems = DOSales.DOSalesLocalItems.OrderBy(s => s.Id).ToArray();

            var DOSales = DbSet.Include(x => x.DOSalesLocalItems).FirstOrDefault(x => x.Id == id);
            return DOSales;
        }

    }
}
