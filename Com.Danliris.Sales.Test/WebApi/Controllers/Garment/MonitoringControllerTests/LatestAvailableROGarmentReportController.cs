using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.Garment;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.Garment;
using Com.Danliris.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.Danliris.Service.Sales.WebApi.Controllers.Garment.MonitoringControllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/report/latest-available-ro-garment")]
    [Authorize]
    public class LatestAvailableROGarmentReportController : BaseMonitoringController<LatestAvailableROGarmentReportViewModel, ILatestAvailableROGarmentReportFacade>
    {
        private readonly static string apiVersion = "1.0";

        public LatestAvailableROGarmentReportController(IIdentityService identityService, ILatestAvailableROGarmentReportFacade facade) : base(identityService, facade, apiVersion)
        {
        }
    }
}
