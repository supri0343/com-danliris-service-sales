﻿using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.FinisihingPrintingSalesContract;
using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.ProductionOrder;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.Models.ProductionOrder;
using Com.Danliris.Service.Sales.Lib.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.ProductionOrder
{
    public class ProductionOrderFacadeTest : BaseFacadeTest<SalesDbContext, ProductionOrderFacade, ProductionOrderLogic, ProductionOrderModel, ProductionOrderDataUtil>
    {
        private const string ENTITY = "ProductionOrder";
        public ProductionOrderFacadeTest() : base(ENTITY)
        {
        }

        public override async void Get_All_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);
            

            var Response = facade.Read(1, 25, "{}", new List<string>(), null, "{}");

            Assert.NotEqual(Response.Data.Count, 0);
        }

        public override async void Create_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }

        public override async void Delete_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);

            var Response = await facade.DeleteAsync((int)data.Id);
            Assert.NotEqual(Response, 0);
        }

        public override async void Get_By_Id_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);

            var Response = facade.ReadByIdAsync((int)data.Id);

            Assert.NotEqual(Response.Id, 0);
        }

        public override async void Update_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            ProductionOrderFacade facade = Activator.CreateInstance(typeof(ProductionOrderFacade), serviceProvider, dbContext) as ProductionOrderFacade;
            FinishingPrintingSalesContractFacade finishingPrintingSalesContractFacade = new FinishingPrintingSalesContractFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            FinisihingPrintingSalesContractDataUtil finisihingPrintingSalesContractDataUtil = new FinisihingPrintingSalesContractDataUtil(finishingPrintingSalesContractFacade);
            var salesData = await finisihingPrintingSalesContractDataUtil.GetTestData();
            var data = DataUtil(facade).GetNewData();
            data.SalesContractId = salesData.Id;
            var model = await facade.CreateAsync(data);


            var response = await facade.UpdateAsync((int)data.Id, data);

            Assert.NotEqual(response, 0);
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IIdentityService)))
                .Returns(identityService);

            var productionOrderLogic = new ProductionOrderLogic(serviceProviderMock.Object, identityService, dbContext);
            var finishingprintingDetailObject = new FinishingPrintingSalesContractDetailLogic(serviceProviderMock.Object, identityService, dbContext);
            var finishingprintingLogic = new FinishingPrintingSalesContractLogic(finishingprintingDetailObject, serviceProviderMock.Object, identityService, dbContext);
            
            serviceProviderMock
                .Setup(x => x.GetService(typeof(ProductionOrderLogic)))
                .Returns(productionOrderLogic);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(FinishingPrintingSalesContractLogic)))
                .Returns(finishingprintingLogic);
            
            return serviceProviderMock;
        }
    }
}
