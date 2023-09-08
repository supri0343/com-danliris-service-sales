using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.DyeingPrintingReportFacades;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.DyeingPrintingReportInterface;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.DyeingPrintingReport;
using Com.Danliris.Service.Sales.WebApi.Helpers;
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
    public class MachineQueueReportController : Controller
    {
        private string ApiVersion = "1.0.0";
        private readonly IMachineQueueReport _facade;
        private readonly IIdentityService _identityService;

        public MachineQueueReportController(IMachineQueueReport facade, IIdentityService identityService)
        {
            _facade = facade;
            _identityService = identityService;
        }
        private void VerifyUser()
        {
            _identityService.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
            _identityService.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
            _identityService.TimezoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
        }

        [HttpGet]
        public async Task<IActionResult> GetReportAll(int page, int size, string filter = "{}")
        {
            int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
            string accept = Request.Headers["Accept"];

            try
            {
                VerifyUser();
                var data = await _facade.Read(page,size,filter);

                return Ok(new
                {
                    apiVersion = ApiVersion,
                    data = data.Item1,
                    info = new { total = data.Item2 },
                    message = General.OK_MESSAGE,
                    statusCode = General.OK_STATUS_CODE
                });
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new Helpers.ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpGet("download")]
        public async Task<IActionResult> GetXlsAll(string filter = "{}")
        {

            try
            {
                VerifyUser();
                byte[] xlsInBytes;
                int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);

                var xls = await _facade.GenerateExcel(filter);

                string filename = String.Format("Laporan Order Belum Diproduksi Mesin.xlsx");

                //xlsInBytes = xls.ToArray();
                return File(xls.Item1.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", xls.Item2);
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new Helpers.ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }
    }
}
