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
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Sales.Test.WebApi.Controllers.DyeingReportTests
{
    public class MachineQueueReportControllerTest //: BaseMonitoringControllerTest<MachineQueueReportController, MachineQueueReportViewModel, IMachineQueueReport>
    {
        protected (Mock<IIdentityService> IdentityService, Mock<IMachineQueueReport> Facade) GetMocks()
        {
            return (IdentityService: new Mock<IIdentityService>(), Facade: new Mock<IMachineQueueReport>());
        }

        protected MachineQueueReportController GetController((Mock<IIdentityService> IdentityService, Mock<IMachineQueueReport> Facade) mocks)
        {
            var user = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            user.Setup(u => u.Claims).Returns(claims);
            MachineQueueReportController controller = (MachineQueueReportController)Activator.CreateInstance(typeof(MachineQueueReportController), mocks.Facade.Object, mocks.IdentityService.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = user.Object
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer unittesttoken";
            controller.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");
            return controller;
        }

        protected int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        [Fact]
        public async Task GetReportAll_WithoutException_ReturnOK()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(new Tuple<List<MachineQueueReportViewModel>, int>(new List<MachineQueueReportViewModel>(), 0));

            var controller = GetController(mocks);
            var response = await controller.GetReportAll(1, 25);

            int statusCode = this.GetStatusCode(response);

            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public async Task GetReportAll_Exception_InternalServer()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = GetController(mocks);
            var response = await controller.GetReportAll(1, 25);

            int statusCode = this.GetStatusCode(response);

            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }

        [Fact]
        public async Task DownloadExcel_WithoutException_ReturnOK()
        {
            var mocks = this.GetMocks();
            mocks.Facade.Setup(f => f.GenerateExcel(It.IsAny<string>()))
                .ReturnsAsync(new Tuple<MemoryStream, string>(new MemoryStream(), It.IsAny<string>()));

            var controller = GetController(mocks);
            var response = await controller.GetXlsAll("{}");
            Assert.NotNull(response);
        }
    }
}
