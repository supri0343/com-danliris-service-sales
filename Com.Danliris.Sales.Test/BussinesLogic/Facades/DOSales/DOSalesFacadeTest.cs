using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.DOSales;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.DOSales;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.DOSales;
using Com.Danliris.Service.Sales.Lib.Models.DOSales;
using Com.Danliris.Service.Sales.Lib.Services;
using Moq;
using System;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.DOSales
{
    public class DOSalesFacadeTest : BaseFacadeTest<SalesDbContext, DOSalesFacade, DOSalesLogic, DOSalesModel, DOSalesDataUtil>
    {
        private const string ENTITY = "DOSales";
        public DOSalesFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            var salesInvoiceDetailLogic = new DOSalesLocalLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(DOSalesLocalLogic)))
                .Returns(salesInvoiceDetailLogic);

            var salesInvoiceLogic = new DOSalesLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(DOSalesLogic)))
                .Returns(salesInvoiceLogic);

            return serviceProviderMock;
        }
    }
}
