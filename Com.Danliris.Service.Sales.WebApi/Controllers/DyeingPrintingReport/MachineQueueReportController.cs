using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.DyeingPrintingReportFacades;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.DyeingPrintingReportInterface;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.DyeingPrintingReport;
using Com.Danliris.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.WebApi.Controllers.DyeingPrintingReport
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/report/machine-queues")]
    [Authorize]
    public class MachineQueueReportController : BaseMonitoringController<MachineQueueReportViewModel, IMachineQueueReport>
    {
        private readonly static string apiVersion = "1.0";

        public MachineQueueReportController(IIdentityService identityService, IMachineQueueReport facade) : base(identityService, facade, apiVersion)
        {
        }
    }
}
