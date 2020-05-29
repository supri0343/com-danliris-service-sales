using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.DOReturn;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.DOReturn;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.DOReturn;
using Com.Danliris.Service.Sales.Lib.Models.DOReturn;
using Com.Danliris.Service.Sales.Lib.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.DOReturn
{
    public class DOReturnFacadeTest : BaseFacadeTest<SalesDbContext, DOReturnFacade, DOReturnLogic, DOReturnModel, DOReturnDataUtil>
    {
        private const string ENTITY = "DOReturn";
        public DOReturnFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            var doReturnItemLogic = new DOReturnItemLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(DOReturnItemLogic)))
                .Returns(doReturnItemLogic);

            var doReturnDetailItemLogic = new DOReturnDetailItemLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(DOReturnDetailItemLogic)))
                .Returns(doReturnDetailItemLogic);

            var doReturnDetailLogic = new DOReturnDetailLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(DOReturnDetailLogic)))
                .Returns(doReturnDetailLogic);

            var doReturnLogic = new DOReturnLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(DOReturnLogic)))
                .Returns(doReturnLogic);

            return serviceProviderMock;
        }

        [Fact]
        public async void Delete_DoReturn_Detail()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DOReturnFacade facade = Activator.CreateInstance(typeof(DOReturnFacade), serviceProvider, dbContext) as DOReturnFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();
            var response = await facade.UpdateAsync((int)data.Id, data);

            Assert.NotEqual(response, 0);
        }

        [Fact]
        public async void Add_New_And_Delete_DoReturn_Detail()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DOReturnFacade facade = Activator.CreateInstance(typeof(DOReturnFacade), serviceProvider, dbContext) as DOReturnFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            data.DOReturnDetails = new List<DOReturnDetailModel>()
            {
                new DOReturnDetailModel()
                {
                        SalesInvoiceId = 1,
                        SalesInvoiceNo = "SalesInvoiceNo",
                        DOReturnDetailItems = new List<DOReturnDetailItemModel>()
                        {
                            new DOReturnDetailItemModel()
                            {
                                DOSalesId = 1,
                                DOSalesNo = "DOSalesNo",
                            },
                        },
                        DOReturnItems = new List<DOReturnItemModel>()
                        {
                            new DOReturnItemModel()
                            {
                                ShippingOutId = 1,
                                BonNo = "BonNo",
                                ProductId = 1,
                                ProductCode = "ProductCode",
                                ProductName = "ProductName",
                                QuantityPacking = 1,
                                PackingUom = "PackingUom",
                                ItemUom = "ItemUom",
                                QuantityItem = 1,
                            },
                        },
                }
            };

            var response = await facade.UpdateAsync((int)data.Id, data);

            Assert.NotEqual(response, 0);
        }


    }
}
