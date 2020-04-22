using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.DOReturn;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.DOReturn;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.DOReturn;
using Com.Danliris.Service.Sales.Lib.Models.DOReturn;
using Com.Danliris.Service.Sales.Lib.Services;
using Moq;
using System;

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

            var doReturnLocalLogic = new DOReturnDetailLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(DOReturnDetailLogic)))
                .Returns(doReturnLocalLogic);

            var doReturnLogic = new DOReturnLogic(serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(DOReturnLogic)))
                .Returns(doReturnLogic);

            return serviceProviderMock;
        }
    }
}
