using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentSalesContractInterface;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentSalesContractLogics;
using Com.Danliris.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.Models.GarmentSalesContractModel;
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
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentSalesContractFacades
{
    public class GarmentSalesContractFacade : IGarmentSalesContract
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<GarmentSalesContract> DbSet;
        private readonly IdentityService identityService;
        private readonly GarmentSalesContractLogic garmentSalesContractLogic;
        private readonly ICostCalculationGarment costCalGarmentLogic;
        private readonly LogHistoryLogic logHistoryLogic;

        public GarmentSalesContractFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<GarmentSalesContract>();
            identityService = serviceProvider.GetService<IdentityService>();
            garmentSalesContractLogic = serviceProvider.GetService<GarmentSalesContractLogic>();
            costCalGarmentLogic = serviceProvider.GetService<ICostCalculationGarment>();
            logHistoryLogic = serviceProvider.GetService<LogHistoryLogic>();
        }

        public async Task<int> CreateAsync(GarmentSalesContract model)
        {
            //do
            //{
            //    model.Code = CodeGenerator.Generate();
            //}
            //while (this.DbSet.Any(d => d.Code.Equals(model.Code)));

            int Created = 0;
           
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    CostCalculationGarment costCal = await costCalGarmentLogic.ReadByIdAsync(model.CostCalculationId); //await DbContext.CostCalculationGarments.FirstOrDefaultAsync(a => a.Id.Equals(model.CostCalculationId));
                                                                                                                       //costCal.SCGarmentId=
                    garmentSalesContractLogic.Create(model);

                    //Create Log History
                    logHistoryLogic.Create("PENJUALAN", "Create Sales Contract - " + model.SalesContractNo);
                    await DbContext.SaveChangesAsync();
                    //Update CC
                    costCal.SCGarmentId = (long)model.Id;

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
            //return Created += await UpdateCostCalAsync(costCal, (int)model.Id);

        }

        public async Task<int> UpdateCostCalAsync(CostCalculationGarment costCalculationGarment, int Id)
        {
            costCalculationGarment.SCGarmentId = Id;
            int result = await costCalGarmentLogic.UpdateAsync((int)costCalculationGarment.Id, costCalculationGarment);

            return result += await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            int Deleted = 0;

            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    GarmentSalesContract sc = await ReadByIdAsync(id);
                    CostCalculationGarment costCal = await DbContext.CostCalculationGarments.Include(cc => cc.CostCalculationGarment_Materials).FirstOrDefaultAsync(a => a.Id.Equals(sc.CostCalculationId));
                    costCal.SCGarmentId = null;

                    await garmentSalesContractLogic.DeleteAsync(id);

                    //Create Log History
                    logHistoryLogic.Create("PENJUALAN", "Delete Sales Contract - " + sc.SalesContractNo);

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
            //return Deleted += await costCalGarmentLogic.UpdateAsync((int)sc.CostCalculationId, costCal);
        }

        public ReadResponse<GarmentSalesContract> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return garmentSalesContractLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<GarmentSalesContract> ReadByIdAsync(int id)
        {
            return await garmentSalesContractLogic.ReadByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, GarmentSalesContract model)
        {
            int Updated = 0;

            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    garmentSalesContractLogic.UpdateAsync(id, model);

                    //Create Log History
                    logHistoryLogic.Create("PENJUALAN", "Update Sales Contract - " + model.SalesContractNo);

                    Updated =  await DbContext.SaveChangesAsync();

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

        public async Task<int> UpdatePrinted(int id, GarmentSalesContract model)
        {
            //garmentSalesContractLogic.UpdateAsync(id, model);

            int Updated = 0;

            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    model.DocPrinted = true;
                    DbSet.Update(model);
                    Updated =  await DbContext.SaveChangesAsync();

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

        public GarmentSalesContract ReadByCostCal(int id)
        {
            return  garmentSalesContractLogic.ReadByCostCal(id);
        }
    }
}
