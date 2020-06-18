using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.SalesInvoiceExport;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.SalesInvoiceExport;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.SalesInvoiceExport;
using Com.Danliris.Service.Sales.Lib.Models.SalesInvoiceExport;
using Com.Danliris.Service.Sales.Lib.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.SalesInvoiceExport
{
    public class SalesInvoiceExportFacadeTest : BaseFacadeTest<SalesDbContext, SalesInvoiceExportFacade, SalesInvoiceExportLogic, SalesInvoiceExportModel, SalesInvoiceExportDataUtil>
    {
        private const string ENTITY = "SalesInvoiceExport";
        public SalesInvoiceExportFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock.Setup(s => s.GetService(typeof(IHttpClientService)))
                .Returns(new HttpClientTestService());

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            var salesInvoiceItemLogic = new SalesInvoiceExportItemLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(SalesInvoiceExportItemLogic)))
                .Returns(salesInvoiceItemLogic);

            var salesInvoiceDetailLogic = new SalesInvoiceExportDetailLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(SalesInvoiceExportDetailLogic)))
                .Returns(salesInvoiceDetailLogic);

            var salesInvoiceLogic = new SalesInvoiceExportLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(SalesInvoiceExportLogic)))
                .Returns(salesInvoiceLogic);


            return serviceProviderMock;
        }
    }
}
