﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.CostCalculationGarmentLogic;
using Com.Danliris.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using Com.Danliris.Service.Sales.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Com.Danliris.Service.Sales.Lib.PDFTemplates;
using Com.Danliris.Service.Sales.Lib.Helpers;
using Microsoft.AspNetCore.JsonPatch;
using Com.Danliris.Service.Sales.Lib.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Microsoft.EntityFrameworkCore;
using Com.Danliris.Service.Sales.Lib.ViewModels.CostCalculationGarment.Cancel_Approval;

namespace Com.Danliris.Service.Sales.WebApi.Controllers
{
	[Produces("application/json")]
	[ApiVersion("1.0")]
	[Route("v{version:apiVersion}/cost-calculation-garments")]
	[Authorize]
	public class CostCalculationGarmentController : BaseController<CostCalculationGarment, CostCalculationGarmentViewModel, ICostCalculationGarment>
	{
		private readonly static string apiVersion = "1.0";
		private readonly IIdentityService Service;
        private readonly IHttpClientService HttpClientService;
        public CostCalculationGarmentController(IIdentityService identityService, IValidateService validateService, ICostCalculationGarment facade, IMapper mapper, IServiceProvider serviceProvider) : base(identityService, validateService, facade, mapper, apiVersion)
		{
			Service = identityService;
            HttpClientService = serviceProvider.GetService<IHttpClientService>();
        }

        [HttpGet("dynamic")]
        public IActionResult GetDynamic(int page = 1, int size = 25, string order = "{}", string select = null, string keyword = null, string filter = "{}", string search = "[]")
        {
            try
            {
                ValidateUser();

                ReadResponse<dynamic> read = Facade.ReadDynamic(page, size, order, select, keyword, filter, search);

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                    .Ok(Mapper, read.Data, page, size, read.Count, read.Data.Count, read.Order, read.Selected);
                return Ok(Result);
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpGet("materials/dynamic")]
        public IActionResult GetMaterials(int page = 1, int size = 25, string order = "{}", string select = null, string keyword = null, string filter = "{}", string search = "[]")
        {
            try
            {
                ValidateUser();

                ReadResponse<dynamic> read = Facade.ReadMaterials(page, size, order, select, keyword, filter, search);

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                    .Ok(Mapper, read.Data, page, size, read.Count, read.Data.Count, read.Order, read.Selected);
                return Ok(Result);
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

		[HttpGet("pdf/{id}")]
		public async Task<IActionResult> GetPDF([FromRoute]int Id)
		{
			 

				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

			try
			{
				var indexAcceptPdf = Request.Headers["Accept"].ToList().IndexOf("application/pdf");
				int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
				CostCalculationGarment model = await Facade.ReadByIdAsync(Id);
				CostCalculationGarmentViewModel viewModel = Mapper.Map<CostCalculationGarmentViewModel>(model);

				if (model == null)
				{
					Dictionary<string, object> Result =
						new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
						.Fail();
					return NotFound(Result);
				}
				else
				{
                    //Get GarmentCategory
                    List<GarmentCategoryViewModel> GarmentCategoryViewModel;
                    var response = HttpClientService.GetAsync($@"{Lib.Helpers.APIEndpoint.Core}master/garment-categories?size=10000").Result.Content.ReadAsStringAsync();
                    Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Result);
                    GarmentCategoryViewModel = JsonConvert.DeserializeObject<List<GarmentCategoryViewModel>>(result.GetValueOrDefault("data").ToString());

                    CostCalculationGarmentPdfTemplate PdfTemplate = new CostCalculationGarmentPdfTemplate();
					MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset, GarmentCategoryViewModel);

					return new FileStreamResult(stream, "application/pdf")
					{
						FileDownloadName = "Cost Calculation Export Garment " + viewModel.RO_Number + (viewModel.IsPosted ? "" : " - DRAFT") + ".pdf"
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

		[HttpGet("budget/{id}")]
		public async Task<IActionResult> GetBudget([FromRoute]int Id)
		{
			try
			{
				Service.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;
				Service.Token = Request.Headers["Authorization"].First().Replace("Bearer ", "");

				//await Service.GeneratePO(Id);
				CostCalculationGarment model = await Facade.ReadByIdAsync(Id);
				CostCalculationGarmentViewModel viewModel = Mapper.Map<CostCalculationGarmentViewModel>(model);

				int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);

                //Get GarmentCategory
                List<GarmentCategoryViewModel> GarmentCategoryViewModel;
                var response = HttpClientService.GetAsync($@"{Lib.Helpers.APIEndpoint.Core}master/garment-categories?size=10000").Result.Content.ReadAsStringAsync();
                Dictionary<string, object> result = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Result);
                GarmentCategoryViewModel = JsonConvert.DeserializeObject<List<GarmentCategoryViewModel>>(result.GetValueOrDefault("data").ToString());

                CostCalculationGarmentBudgetPdfTemplate PdfTemplate = new CostCalculationGarmentBudgetPdfTemplate();
				MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset, GarmentCategoryViewModel);

				return new FileStreamResult(stream, "application/pdf")
				{
					FileDownloadName = "Budget Export Garment " + viewModel.RO_Number + (viewModel.IsPosted ? "" : " - DRAFT") + ".pdf"
				};
			}
			catch (Exception e)
			{
				Dictionary<string, object> Result =
					new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
					.Fail();
				return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
			}
		}

        [HttpGet("with-product-names/{Id}")]
        public async Task<IActionResult> GetById_RO_Garment_Validation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Service.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;
                Service.Token = Request.Headers["Authorization"].First().Replace("Bearer ", "");

                CostCalculationGarment model = await Facade.ReadByIdAsync(id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    CostCalculationGarmentViewModel viewModel = Mapper.Map<CostCalculationGarmentViewModel>(model);

                    var productIds = viewModel.CostCalculationGarment_Materials.Select(m => m.Product.Id).Distinct().ToList();
                    var productDicts = await Facade.GetProductNames(productIds);

                    foreach (var material in viewModel.CostCalculationGarment_Materials)
                    {
                        material.Product.Name = productDicts.GetValueOrDefault(material.Product.Id);
                    }

                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                        .Ok<CostCalculationGarmentViewModel>(viewModel);
                    return Ok(Result);
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute]int id, [FromBody]JsonPatchDocument<CostCalculationGarment> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var model = await Facade.ReadByIdAsync(id);
                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    ValidateUser();

                    await Facade.Patch(id, patch);

                    return NoContent();
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

        [HttpPut("isvalidate-ro-sample/{Id}")]
        public async Task<IActionResult> PutRoSample([FromRoute] int id, [FromBody] CostCalculationGarmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var model = await Facade.ReadByIdAsync(id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }

                model.IsValidatedROSample = true;
                IdentityService.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
                IdentityService.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");

                await Facade.UpdateAsync(id, model);

                return NoContent();
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpGet("ro-acceptance")]
        public IActionResult GetForROAcceptance(int page = 1, int size = 25, string order = "{}", [Bind(Prefix = "Select[]")]List<string> select = null, string keyword = null, string filter = "{}")
        {
            try
            {
                ReadResponse<CostCalculationGarment> read = Facade.ReadForROAcceptance(page, size, order, select, keyword, filter);

                //Tuple<List<TModel>, int, Dictionary<string, string>, List<string>> Data = Facade.Read(page, size, order, select, keyword, filter);
                List<CostCalculationGarmentViewModel> DataVM = Mapper.Map<List<CostCalculationGarmentViewModel>>(read.Data);

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                    .Ok<CostCalculationGarmentViewModel>(Mapper, DataVM, page, size, read.Count, DataVM.Count, read.Order, read.Selected);
                return Ok(Result);

            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpPost("acceptance")]
        public async Task<IActionResult> AcceptCC([FromBody]List<long> listId)
        {
            try
            {
                IdentityService.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;

                await Facade.AcceptanceCC(listId, IdentityService.Username);

                return Ok();
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpGet("ro-available")]
        public IActionResult GetForROAvailable(int page = 1, int size = 25, string order = "{}", [Bind(Prefix = "Select[]")]List<string> select = null, string keyword = null, string filter = "{}")
        {
            try
            {
                ReadResponse<CostCalculationGarment> read = Facade.ReadForROAvailable(page, size, order, select, keyword, filter);

                //Tuple<List<TModel>, int, Dictionary<string, string>, List<string>> Data = Facade.Read(page, size, order, select, keyword, filter);
                List<CostCalculationGarmentViewModel> DataVM = Mapper.Map<List<CostCalculationGarmentViewModel>>(read.Data);

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                    .Ok<CostCalculationGarmentViewModel>(Mapper, DataVM, page, size, read.Count, DataVM.Count, read.Order, read.Selected);
                return Ok(Result);

            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpPost("available")]
        public async Task<IActionResult> AvailableCC([FromBody]List<long> listId)
        {
            try
            {
                IdentityService.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;

                await Facade.AvailableCC(listId, IdentityService.Username);

                return Ok();
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpGet("ro-distribute")]
        public IActionResult GetForRODistribute(int page = 1, int size = 25, string order = "{}", [Bind(Prefix = "Select[]")]List<string> select = null, string keyword = null, string filter = "{}")
        {
            try
            {
                ReadResponse<CostCalculationGarment> read = Facade.ReadForRODistribution(page, size, order, select, keyword, filter);

                //Tuple<List<TModel>, int, Dictionary<string, string>, List<string>> Data = Facade.Read(page, size, order, select, keyword, filter);
                List<CostCalculationGarmentViewModel> DataVM = Mapper.Map<List<CostCalculationGarmentViewModel>>(read.Data);

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                    .Ok<CostCalculationGarmentViewModel>(Mapper, DataVM, page, size, read.Count, DataVM.Count, read.Order, read.Selected);
                return Ok(Result);

            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpPost("distribute")]
        public async Task<IActionResult> DistributeCC([FromBody]List<long> listId)
        {
            try
            {
                IdentityService.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;

                await Facade.DistributeCC(listId, IdentityService.Username);

                return Ok();
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpPost("post")]
        public async Task<IActionResult> PostCC([FromBody]List<long> listId)
        {
            try
            {
                ValidateUser();

                int result = await Facade.PostCC(listId);
                if (result < 1)
                {
                    Dictionary<string, object> Result = 
                        new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, "No changes applied.")
                        .Fail();
                    return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
                }
                return NoContent();
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpPut("unpost/{id}")]
        public async Task<IActionResult> UnpostCC(long id, [FromBody]string reason)
        {
            try
            {
                ValidateUser();

                if (string.IsNullOrWhiteSpace(reason))
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.BAD_REQUEST_STATUS_CODE, Common.BAD_REQUEST_MESSAGE)
                        .Fail("Alasan tidak diisi.");
                    return BadRequest(Result);

                }

                await Facade.UnpostCC(id, reason);
                return NoContent();
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpGet("unpost-reason-creators")]
        public IActionResult ReadUnpostReasonCreators(string keyword = "", int page = 1, int size = 25)
        {
            try
            {
                List<string> creators = Facade.ReadUnpostReasonCreators(keyword, page, size);

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                    .Ok(creators);
                return Ok(Result);

            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }
		[HttpGet("data")]
		public IActionResult GetComodityQtyOrderHoursBuyerByRo([FromBody]string ro)
		{
			try
			{
				var indexAcceptPdf = Request.Headers["Accept"].ToList().IndexOf("application/pdf");
				var viewModel = Facade.GetComodityQtyOrderHoursBuyerByRo(ro);
				Dictionary<string, object> Result =
				new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
				.Ok(viewModel);
				return Ok(Result);

			}
			catch (Exception e)
			{
				Dictionary<string, object> Result =
					new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
					.Fail();
				return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
			}
		}

		[HttpGet("materials/by-prmasteritemids")]
        public IActionResult GetMaterialsByPRMasterItemIds(int page = 1, int size = 25, string order = "{}", string select = null, string keyword = null, string filter = "{}", string search = "[]", string prmasteritemids = "[]")
        {
            try
            {
                ValidateUser();

                ReadResponse<dynamic> read = Facade.ReadMaterialsByPRMasterItemIds(page, size, order, select, keyword, filter, search, prmasteritemids);

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                    .Ok(Mapper, read.Data, page, size, read.Count, read.Data.Count, read.Order, read.Selected);
                return Ok(Result);
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpGet("by-ro/{ro}")]
        public async Task<IActionResult> GetByRO([FromRoute]string ro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Service.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;
                Service.Token = Request.Headers["Authorization"].First().Replace("Bearer ", "");

                CostCalculationGarment model = await Facade.ReadByRO(ro);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.NOT_FOUND_STATUS_CODE, Common.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    CostCalculationGarmentViewModel viewModel = Mapper.Map<CostCalculationGarmentViewModel>(model);

                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                        .Ok<CostCalculationGarmentViewModel>(viewModel);
                    return Ok(Result);
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
        [HttpGet("by-ros")]
        public async Task<IActionResult> GetByROs([FromBody]List<string> ro)
        {
            try
            {
                Service.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;
                Service.Token = Request.Headers["Authorization"].First().Replace("Bearer ", "");

                var model = await Facade.ReadByROs(ro);

                List<CostCalculationGarmentViewModel> viewModel = Mapper.Map<List<CostCalculationGarmentViewModel>>(model);

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                    .Ok<List<CostCalculationGarmentViewModel>>(viewModel);
                return Ok(Result);

            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        #region Cancel Approval
        [HttpGet("read-cancel-approval")]
        public IActionResult ReadForCancelApproval(int page = 1, int size = 25, string order = "{}", List<string> select = null, string keyword = null, string filter = "{}", string search = "[]")
        {
            try
            {
                ValidateUser();

                ReadResponse<CostCalculationGarment> read = Facade.ReadForCancelApproval(page, size, order, select, keyword, filter);

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.OK_STATUS_CODE, Common.OK_MESSAGE)
                    .Ok(Mapper, read.Data, page, size, read.Count, read.Data.Count, read.Order, read.Selected);
                return Ok(Result);
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpPut("cancel-approval/{id}")]
        public virtual async Task<IActionResult> CancelApproval([FromRoute] int id, [FromBody] CancelApprovalCostCalculationGarmentViewModel data)
        {
            try
            {
                ValidateUser();

                await Facade.CancelApproval(id, data.DeletedRemark);

                return NoContent();
            }

            catch (DbUpdateConcurrencyException e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpGet("cancel-approval/report")]
        public IActionResult GetReportAll(DateTime? dateFrom, DateTime? dateTo, int page, int size)
        {
            int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
            string accept = Request.Headers["Accept"];

            try
            {
                DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : Convert.ToDateTime(dateFrom);
                DateTime DateTo = dateTo == null ? DateTime.Now : Convert.ToDateTime(dateTo);

                var data = Facade.ReadCancelApproval(DateFrom, DateTo, page, size, offset);

                return Ok(new
                {
                    apiVersion = apiVersion,
                    data = data.Item1,
                    info = new { total = data.Item2 },
                    message = Common.OK_MESSAGE,
                    statusCode = Common.OK_STATUS_CODE
                });
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new Utilities.ResultFormatter(apiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpGet("cancel-approval/download")]
        public IActionResult GetXlsCancelApproval(DateTime? dateFrom, DateTime? dateTo)
        {

            try
            {
                byte[] xlsInBytes;
                int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : Convert.ToDateTime(dateFrom);
                DateTime DateTo = dateTo == null ? DateTime.Now : Convert.ToDateTime(dateTo);

                var xls = Facade.GenerateExcelCancelApproval(dateFrom, dateTo, offset);

                string filename = String.Format("Laporan Cancel Approval CostCalculation - {0} - {1}.xlsx", DateFrom.ToString("dd-MM-yyyy"), DateTo.ToString("dd-MM-yyyy"));

                xlsInBytes = xls.ToArray();
                var file = File(xlsInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                return file;

            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new Utilities.ResultFormatter(apiVersion, Common.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(Common.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        #endregion
    }
}