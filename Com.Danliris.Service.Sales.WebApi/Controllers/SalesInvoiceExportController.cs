using AutoMapper;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.SalesInvoiceExport;
using Com.Danliris.Service.Sales.Lib.Models.SalesInvoiceExport;
using Com.Danliris.Service.Sales.Lib.PDFTemplates;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.SalesInvoiceExport;
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
    [Route("v{version:apiVersion}/sales/sales-invoices-export")]
    [Authorize]

    public class SalesInvoiceExportController : BaseController<SalesInvoiceExportModel, SalesInvoiceExportViewModel, ISalesInvoiceExportContract>
    {
        private readonly ISalesInvoiceExportContract _facade;
        private readonly static string apiVersion = "1.0";
        public SalesInvoiceExportController(IIdentityService identityService, IValidateService validateService, ISalesInvoiceExportContract salesInvoiceExportFacade, IMapper mapper, IServiceProvider serviceProvider) : base(identityService, validateService, salesInvoiceExportFacade, mapper, apiVersion)
        {
            _facade = salesInvoiceExportFacade;
        }

        [HttpGet("sales-invoice-export-pdf/{Id}")]
        public async Task<IActionResult> GetSalesInvoicePDF([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var indexAcceptPdf = Request.Headers["Accept"].ToList().IndexOf("application/pdf");
                int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                SalesInvoiceExportModel model = await Facade.ReadByIdAsync(Id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    SalesInvoiceExportViewModel viewModel = Mapper.Map<SalesInvoiceExportViewModel>(model);

                    SalesInvoiceExportPdfTemplate PdfTemplate = new SalesInvoiceExportPdfTemplate();
                    MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset);
                    return new FileStreamResult(stream, "application/pdf")
                    {
                        FileDownloadName = "Faktur_Penjualan/" + viewModel.SalesInvoiceNo + ".pdf"
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
