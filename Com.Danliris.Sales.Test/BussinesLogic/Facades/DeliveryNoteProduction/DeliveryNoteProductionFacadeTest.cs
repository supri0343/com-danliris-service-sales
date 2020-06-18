using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.DeliveryNoteProduction;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.DeliveryNoteProduction;
using Com.Danliris.Service.Sales.Lib.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.DeliveryNoteProduction
{
    public class DeliveryNoteProductionFacadeTest : BaseFacadeTest<SalesDbContext, DeliveryNoteProductionFacade, DeliveryNoteProductionLogic, DeliveryNoteProductionModel, DeliveryNoteProductionDataUtil>
    {

        private const string ENTITY = "DOSales";
        public DeliveryNoteProductionFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            var doSalesLogic = new DeliveryNoteProductionLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(DeliveryNoteProductionLogic)))
                .Returns(doSalesLogic);

            return serviceProviderMock;
        }
    }
}
