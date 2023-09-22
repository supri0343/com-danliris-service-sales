using Com.Danliris.Sales.Test.WebApi.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.DyeingPrintingReportInterface;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.DyeingPrintingReport;
using Com.Danliris.Service.Sales.WebApi.Controllers.DyeingPrintingReport;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace Com.Danliris.Sales.Test.WebApi.Controllers.DyeingReportTests
{
    public class MachineQueueReportControllerTest : BaseMonitoringControllerTest<MachineQueueReportController, MachineQueueReportViewModel, IMachineQueueReport>
    { 
    }
}
