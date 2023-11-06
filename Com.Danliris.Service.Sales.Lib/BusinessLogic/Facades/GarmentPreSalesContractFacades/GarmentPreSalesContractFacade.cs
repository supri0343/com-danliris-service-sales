using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentPreSalesContractInterface;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentSalesContractLogics;
using Com.Danliris.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.Models.GarmentPreSalesContractModel;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Com.DanLiris.Service.Purchasing.Lib.Interfaces;
using Com.Danliris.Service.Sales.Lib.Helpers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Com.Danliris.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.CostCalculationGarmentLogic;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentPreSalesContractLogics;
using System.Linq;
using Com.Moonlay.Models;
using Microsoft.AspNetCore.JsonPatch;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentPreSalesContractFacades
{
    public class GarmentPreSalesContractFacade : IGarmentPreSalesContract
    {
        private string USER_AGENT = "Facade";

        private readonly SalesDbContext DbContext;
        private readonly DbSet<GarmentPreSalesContract> DbSet;
        private readonly IdentityService identityService;
        private readonly GarmentPreSalesContractLogic garmentPreSalesContractLogic;
        private readonly LogHistoryLogic logHistoryLogic;
        public GarmentPreSalesContractFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<GarmentPreSalesContract>();
            identityService = serviceProvider.GetService<IdentityService>();
            garmentPreSalesContractLogic = serviceProvider.GetService<GarmentPreSalesContractLogic>();
            logHistoryLogic = serviceProvider.GetService<LogHistoryLogic>();
        }

        public async Task<int> CreateAsync(GarmentPreSalesContract model)
        {
            int Created = 0;

            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    garmentPreSalesContractLogic.Create(model);

                    //Create Log History
                    logHistoryLogic.Create("PENJUALAN", "Create Pre Sales Kontrak - " + model.SCNo);

                    Created = await DbContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
            return Created;
        }

        public ReadResponse<GarmentPreSalesContract> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return garmentPreSalesContractLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<GarmentPreSalesContract> ReadByIdAsync(int id)
        {
            return await garmentPreSalesContractLogic.ReadByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, GarmentPreSalesContract model)
        {
            int Updated = 0;

            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    garmentPreSalesContractLogic.UpdateAsync(id, model);

                    //Create Log History
                    logHistoryLogic.Create("PENJUALAN", "Update Pre Sales Kontrak - " + model.SCNo);

                    Updated = await DbContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
            return Updated;
        }

        public async Task<int> DeleteAsync(int id)
        {
            int Deleted = 0;

            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    var model = await ReadByIdAsync(id);
                    await garmentPreSalesContractLogic.DeleteAsync(id);

                    //Create Log History
                    logHistoryLogic.Create("PENJUALAN", "Delete Pre Sales Kontrak - " + model.SCNo);

                    Deleted = await DbContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
            return Deleted;
        }

        public async Task<int> Patch(long id, JsonPatchDocument<GarmentPreSalesContract> jsonPatch)
        {
            int Updated = 0;

            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    garmentPreSalesContractLogic.Patch(id, jsonPatch);
                    Updated = await DbContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }

            return Updated;
        }

        public async Task<int> PreSalesPost(List<long> listId, string user)
        {
            int Updated = 0;

            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var listData = DbSet.
                        Where(w => listId.Contains(w.Id))
                        .ToList();

                    foreach (var data in listData)
                    {
                        EntityExtension.FlagForUpdate(data, user, USER_AGENT);
                        data.IsPosted = true;

                        //Create Log History
                        logHistoryLogic.Create("PENJUALAN", "Post Pre Sales Kontrak - " + data.SCNo);

                    }

                    Updated = await DbContext.SaveChangesAsync();

                    if (Updated < 1)
                    {
                        throw new Exception("No data updated");
                    }

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }

            return Updated;
        }

        public async Task<int> PreSalesUnpost(long id, string user)
        {
            int Updated = 0;

            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var data = DbSet
                        .Where(w => w.Id == id)
                        .Single();

                    EntityExtension.FlagForUpdate(data, user, USER_AGENT);
                    data.IsPosted = false;

                    //Create Log History
                    logHistoryLogic.Create("PENJUALAN", "UnPost Pre Sales Kontrak - " + data.SCNo);

                    Updated = await DbContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }

            return Updated;
        }
    }
}