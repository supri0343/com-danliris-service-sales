using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.FinisihingPrintingSalesContract;
using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.ProductionOrder;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.DyeingPrintingReportFacades;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.DyeingPrintingReportInterface;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.DyeingPrintingReportLogics;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.DyeingPrintingReportTests
{
    public class MachineQueueReportFacadeTest
    {
        private const string ENTITY = "MachineQueueReport";

        [MethodImpl(MethodImplOptions.NoInlining)]
        private string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
        }

        private SalesDbContext DbContext(string testName)
        {
            DbContextOptionsBuilder<SalesDbContext> optionsBuilder = new DbContextOptionsBuilder<SalesDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            SalesDbContext dbContext = new SalesDbContext(optionsBuilder.Options);

            return dbContext;
        }

        protected virtual Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IIdentityService)))
                .Returns(identityService);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(FinishingPrintingSalesContractLogic)))
                .Returns(new FinishingPrintingSalesContractLogic(new FinishingPrintingSalesContractDetailLogic(serviceProviderMock.Object,identityService,dbContext),serviceProviderMock.Object,identityService,dbContext));

            serviceProviderMock
                .Setup(x => x.GetService(typeof(ProductionOrder_RunWidthLogic)))
                .Returns(new ProductionOrder_RunWidthLogic(serviceProviderMock.Object, identityService, dbContext));

            serviceProviderMock
                .Setup(x => x.GetService(typeof(ProductionOrder_LampStandardLogic)))
                .Returns(new ProductionOrder_LampStandardLogic(serviceProviderMock.Object, identityService, dbContext));

            serviceProviderMock
                .Setup(x => x.GetService(typeof(ProductionOrder_DetailLogic)))
                .Returns(new ProductionOrder_DetailLogic(serviceProviderMock.Object, identityService, dbContext));

            serviceProviderMock
                .Setup(x => x.GetService(typeof(ProductionOrderLogic)))
                .Returns(new ProductionOrderLogic(serviceProviderMock.Object, identityService,dbContext));

            serviceProviderMock
                .Setup(x => x.GetService(typeof(MachineQueueReportLogic)))
                .Returns(new MachineQueueReportLogic(identityService,dbContext));
            return serviceProviderMock;
        }

        [Fact]
        public async void Get_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();

            ProductionOrderFacade productionOrderFacade = new ProductionOrderFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            ProductionOrderDataUtil productionOrderDataUtil = new ProductionOrderDataUtil(productionOrderFacade);
            var productionOrderData = await productionOrderDataUtil.GetTestData();

            IMachineQueueReport machineQueueReportFacade = new MachineQueueReportFacade(serviceProvider, dbContext);
            var Response = machineQueueReportFacade.Read(1,25,"{}");

            Assert.NotNull(Response);
        }

        [Fact]
        public async void Get_Success_Excel()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();

            ProductionOrderFacade productionOrderFacade = new ProductionOrderFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            ProductionOrderDataUtil productionOrderDataUtil = new ProductionOrderDataUtil(productionOrderFacade);
            var productionOrderData = await productionOrderDataUtil.GetTestData();

            IMachineQueueReport machineQueueReportFacade = new MachineQueueReportFacade(serviceProvider, dbContext);
            var filter = new
            {
                orderTypeName = productionOrderData.OrderTypeName,
                dateFrom = DateTime.Now.AddDays(-30),
                dateTo = DateTime.Now.AddDays(30),
                orderType = productionOrderData.OrderTypeName,
            };
            var Response = machineQueueReportFacade.GenerateExcel(filter: JsonConvert.SerializeObject(filter));

            Assert.NotNull(Response.Item2);
        }

        //[Fact]
        //public void Get_Success_Empty_Excel()
        //{
        //    var dbContext = DbContext(GetCurrentMethod());
        //    var serviceProvider = GetServiceProviderMock(dbContext).Object;

        //    var facade = new AcceptedROReportFacade(serviceProvider);

        //    var Response = facade.GenerateExcel();

        //    Assert.NotNull(Response.Item2);
        //}
    }
}
