using AutoMapper;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.DOSales;
using Com.Danliris.Service.Sales.Lib.Models.DOSales;
using Com.Danliris.Service.Sales.Lib.PDFTemplates;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.DOSales;
using Com.Danliris.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/sales/do-sales")]
    [Authorize]

    public class DOSalesController : BaseController<DOSalesModel, DOSalesViewModel, IDOSalesContract>
    {
        private readonly IDOSalesContract _facade;
        private readonly static string apiVersion = "1.0";
        public DOSalesController(IIdentityService identityService, IValidateService validateService, IDOSalesContract doSalesFacade, IMapper mapper, IServiceProvider serviceProvider) : base(identityService, validateService, doSalesFacade, mapper, apiVersion)
        {
            _facade = doSalesFacade;
        }

        [HttpGet("doSalesPdf/{Id}")]
        public async Task<IActionResult> GetDOSalesPDF([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var indexAcceptPdf = Request.Headers["Accept"].ToList().IndexOf("application/pdf");
                int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                DOSalesModel model = await Facade.ReadByIdAsync(Id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    DOSalesViewModel viewModel = Mapper.Map<DOSalesViewModel>(model);

                    DOSalesPdfTemplate PdfTemplate = new DOSalesPdfTemplate();
                    MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset);
                    return new FileStreamResult(stream, "application/pdf")
                    {
                        FileDownloadName = "DO_Sales/" + viewModel.DOSalesNo + ".pdf"
                    };
                }
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }
    }
}
