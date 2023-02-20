using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.DOSales;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.DOSales;
using Com.Danliris.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/sales/do-sales/monitoring")]
    [Authorize]
    public class DOSalesMonitoringController : BaseMonitoringController<DOSalesMonitoringViewModel, IDOSalesMonitoring>
    {
        private readonly static string apiVersion = "1.0";

        public DOSalesMonitoringController(IIdentityService identityService, IDOSalesMonitoring facade) : base(identityService, facade, apiVersion)
        {
        }
    }
}