using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.SalesInvoice;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.SalesInvoice;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.SalesInvoice;
using Com.Danliris.Service.Sales.Lib.Models.SalesInvoice;
using Com.Danliris.Service.Sales.Lib.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.SalesInvoice
{
    public class SalesInvoiceFacadeTest : BaseFacadeTest<SalesDbContext, SalesInvoiceFacade, SalesInvoiceLogic, SalesInvoiceModel, SalesInvoiceDataUtil>
    {
        private const string ENTITY = "SalesInvoice";
        public SalesInvoiceFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            var salesInvoiceItemLogic = new SalesInvoiceItemLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(SalesInvoiceItemLogic)))
                .Returns(salesInvoiceItemLogic);

            var salesInvoiceDetailLogic = new SalesInvoiceDetailLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(SalesInvoiceDetailLogic)))
                .Returns(salesInvoiceDetailLogic);

            var salesInvoiceLogic = new SalesInvoiceLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(SalesInvoiceLogic)))
                .Returns(salesInvoiceLogic);


            return serviceProviderMock;
        }

        [Fact]
        public async void Update_From_SalesReceipt_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            SalesInvoiceFacade facade = new SalesInvoiceFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();
            var model = new SalesInvoiceUpdateModel()
            {
                IsPaidOff = false,
                TotalPaid = 1000,
            };


            var Response = await facade.UpdateFromSalesReceiptAsync((int)data.Id, model);
            Assert.NotEqual(Response, 0);
        }

        [Fact]
        public virtual async void Read_By_BuyerId_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            SalesInvoiceFacade facade = new SalesInvoiceFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade).GetTestData();

            var Response = facade.ReadByBuyerId((int)data.BuyerId);

            Assert.NotEqual(Response.Count, 0);
        }

        [Fact]
        public async void GetReport_Success()
        {
            var dbContext = DbContext(GetCurrentMethod() + "GetReport_Success");
            var serviceProvider = GetServiceProviderMock(dbContext);
            SalesInvoiceFacade facade = new SalesInvoiceFacade(serviceProvider.Object, dbContext);

            var data = await DataUtil(facade).GetTestData();

            serviceProvider.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(new SalesInvoiceHttpClientTestService(data));

            SalesInvoiceFacade facade2 = new SalesInvoiceFacade(serviceProvider.Object, dbContext);

            var Response = await facade2.GetReport(data.BuyerId, data.Id, data.IsPaidOff, data.DueDate.AddDays(-1), data.DueDate.AddDays(1), 7);

            Assert.NotEqual(Response.Count, 0);

            Response = await facade2.GetReport(data.BuyerId, data.Id, data.IsPaidOff, data.DueDate.AddDays(-1), null, 7);

            Assert.NotEqual(Response.Count, 0);

            Response = await facade2.GetReport(data.BuyerId, data.Id, data.IsPaidOff, null, data.DueDate.AddDays(1), 7);

            Assert.NotEqual(Response.Count, 0);

            Response = await facade2.GetReport(data.BuyerId, data.Id, data.IsPaidOff, null, null, 7);

            Assert.NotEqual(Response.Count, 0);
        }

        [Fact]
        public async void GenerateExcel_Success()
        {
            var dbContext = DbContext(GetCurrentMethod() + "GenerateExcel_Success");
            var serviceProvider = GetServiceProviderMock(dbContext);

            SalesInvoiceFacade facade = new SalesInvoiceFacade(serviceProvider.Object, dbContext);

            var data = await DataUtil(facade).GetTestData();

            serviceProvider.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(new SalesInvoiceHttpClientTestService(data));
            var facade2 = new SalesInvoiceFacade(serviceProvider.Object, dbContext);


            var Response = await facade2.GenerateExcel(data.BuyerId, data.Id, data.IsPaidOff, data.DueDate.AddDays(-1), data.DueDate.AddDays(1), 7);

            Assert.NotNull(Response);

            Response = await facade2.GenerateExcel(data.BuyerId, data.Id, data.IsPaidOff, data.DueDate.AddDays(-3), data.DueDate.AddDays(-2), 7);

            Assert.NotNull(Response);
        }

        [Fact]
        public virtual async void Create_Success_SalesInvoiceType_Is_BLL()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            SalesInvoiceFacade facade = Activator.CreateInstance(typeof(SalesInvoiceFacade), serviceProvider, dbContext) as SalesInvoiceFacade;

            var data = await DataUtil(facade, dbContext).GetNewData_SalesInvoiceType_Is_BLL();

            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public virtual async void Create_Success_SalesInvoiceType_Is_BON()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            SalesInvoiceFacade facade = Activator.CreateInstance(typeof(SalesInvoiceFacade), serviceProvider, dbContext) as SalesInvoiceFacade;

            var data = await DataUtil(facade, dbContext).GetNewData_SalesInvoiceType_Is_BON();

            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public virtual async void Create_Success_SalesInvoiceType_Is_BGM()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            SalesInvoiceFacade facade = Activator.CreateInstance(typeof(SalesInvoiceFacade), serviceProvider, dbContext) as SalesInvoiceFacade;

            var data = await DataUtil(facade, dbContext).GetNewData_SalesInvoiceType_Is_BGM();

            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public virtual async void Create_Success_SalesInvoiceType_Is_BPF()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            SalesInvoiceFacade facade = Activator.CreateInstance(typeof(SalesInvoiceFacade), serviceProvider, dbContext) as SalesInvoiceFacade;

            var data = await DataUtil(facade, dbContext).GetNewData_SalesInvoiceType_Is_BPF();

            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public virtual async void Create_Success_SalesInvoiceType_Is_BPR()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            SalesInvoiceFacade facade = Activator.CreateInstance(typeof(SalesInvoiceFacade), serviceProvider, dbContext) as SalesInvoiceFacade;

            var data = await DataUtil(facade, dbContext).GetNewData_SalesInvoiceType_Is_BPR();

            var response = await facade.CreateAsync(data);

            Assert.NotEqual(response, 0);
        }
    }
}
