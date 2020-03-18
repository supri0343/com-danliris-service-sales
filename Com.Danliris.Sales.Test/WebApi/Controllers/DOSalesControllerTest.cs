using AutoMapper;
using Com.Danliris.Sales.Test.WebApi.Utils;
using Com.Danliris.Service.Sales.Lib.AutoMapperProfiles.DOSalesProfiles;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.DOSales;
using Com.Danliris.Service.Sales.Lib.Models.DOSales;
using Com.Danliris.Service.Sales.Lib.ViewModels.DOSales;
using Com.Danliris.Service.Sales.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace Com.Danliris.Sales.Test.WebApi.Controllers
{
    public class DOSalesControllerTest : BaseControllerTest<DOSalesController, DOSalesModel, DOSalesViewModel, IDOSalesContract>
    {
        [Fact]
        public void Get_DO_Sales_Local_PDF_Success()
        {
            var vm = new DOSalesViewModel()
            {
                LocalType = "US",
                LocalDate = DateTimeOffset.UtcNow,
                DestinationBuyerName = "DestinationBuyerName",
                DestinationBuyerAddress = "DestinationBuyerAddress",
                SalesName = "SalesName",
                LocalHeadOfStorage = "LocalHeadOfStorage",
                PackingUom = "PCS",
                ImperialUom = "YDS",
                MetricUom = "MTR",
                Disp = 1,
                Op = 1,
                Sc = 1,
                LocalRemark = "LocalRemark",
                DOSalesLocalItems = new List<DOSalesLocalViewModel>()
                {
                    new DOSalesLocalViewModel()
                    {
                        TotalPacking = 1,
                        TotalMetric = 1,
                        TotalImperial = 1,
                    }
                }

            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<DOSalesViewModel>(It.IsAny<DOSalesModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetDOSalesPDF(1).Result;

            Assert.NotNull(response);

        }

        //[Fact]
        //public void Get_DO_Sales_Local_PDF_NotFound()
        //{
        //    var mocks = GetMocks();
        //    mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(DOSalesModel));
        //    var controller = GetController(mocks);
        //    var response = controller.GetDOSalesLocalPDF(1).Result;

        //    int statusCode = this.GetStatusCode(response);
        //    Assert.Equal((int)HttpStatusCode.NotFound, statusCode);

        //}

        //[Fact]
        //public void Get_DO_Sales_Local_PDF_Exception()
        //{
        //    var mocks = GetMocks();
        //    mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("error"));
        //    var controller = GetController(mocks);
        //    var response = controller.GetDOSalesLocalPDF(1).Result;

        //    int statusCode = this.GetStatusCode(response);
        //    Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);

        //}

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DOSalesMapper>();
                cfg.AddProfile<DOSalesLocalMapper>();
            });
            var mapper = configuration.CreateMapper();

            DOSalesViewModel salesInvoiceViewModel = new DOSalesViewModel { Id = 1 };
            DOSalesModel salesInvoiceModel = mapper.Map<DOSalesModel>(salesInvoiceViewModel);

            Assert.Equal(salesInvoiceViewModel.Id, salesInvoiceModel.Id);

            DOSalesLocalViewModel salesInvoiceDetailViewModel = new DOSalesLocalViewModel { Id = 1 };
            DOSalesLocalModel salesInvoiceDetailModel = mapper.Map<DOSalesLocalModel>(salesInvoiceDetailViewModel);

            Assert.Equal(salesInvoiceDetailViewModel.Id, salesInvoiceDetailModel.Id);
        }

        [Fact]
        public void Validate_Null_Model_and_DetailViewModel()
        {
            List<DOSalesViewModel> viewModels = new List<DOSalesViewModel>
            { };
            foreach (var viewModel in viewModels)
            {
                var defaultValidationResult = viewModel.Validate(null);
                Assert.True(defaultValidationResult.Count() > 0);
            }
        }
    }
}
