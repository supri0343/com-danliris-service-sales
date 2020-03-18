using AutoMapper;
using Com.Danliris.Sales.Test.WebApi.Utils;
using Com.Danliris.Service.Sales.Lib.AutoMapperProfiles.DOSalesProfiles;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.DOSales;
using Com.Danliris.Service.Sales.Lib.Models.DOSales;
using Com.Danliris.Service.Sales.Lib.ViewModels.DOSales;
using Com.Danliris.Service.Sales.Lib.ViewModels.FinishingPrinting;
using Com.Danliris.Service.Sales.Lib.ViewModels.ProductionOrder;
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
                DOSalesType = "Lokal",
                DOSalesNo = "DOSalesNo",
                LocalDate = DateTimeOffset.Now,
                LocalHeadOfStorage = "LocalHeadOfStorage",
                DestinationBuyerName = "DestinationBuyerName",
                PackingUom = "PCS",
                MetricUom = "MTR",
                ImperialUom = "YDS",
            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<DOSalesViewModel>(It.IsAny<DOSalesModel>())).Returns(vm);
            mocks.Mapper.Setup(s => s.Map<DOSalesViewModel>(It.IsAny<DOSalesModel>())).Returns(new DOSalesViewModel()
            {
                DOSalesLocalItems = new List<DOSalesLocalViewModel>()
                {
                    new DOSalesLocalViewModel()
                    {
                        ProductionOrder = new ProductionOrderViewModel()
                        {
                            OrderNo = "OrderNo",
                        },
                        UnitOrCode = "UnitCode",
                        TotalPacking = 1,
                        TotalMetric = 1,
                        TotalImperial = 1,
                    }
                }
            });
            var controller = GetController(mocks);
            var response = controller.GetDOSalesPDF(1).Result;

            Assert.NotNull(response);
        }

        [Fact]
        public void Get_DO_Sales_Export_PDF_Success()
        {
            var vm = new DOSalesViewModel()
            {
                DOSalesType = "Ekspor",
                DOSalesNo = "DOSalesNo",
                ExportDate = DateTimeOffset.Now,
                DoneBy = "DoneBy",
                FillEachBale = 1,
                ExportRemark = "ExportRemark",
            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<DOSalesViewModel>(It.IsAny<DOSalesModel>())).Returns(vm);
            mocks.Mapper.Setup(s => s.Map<DOSalesViewModel>(It.IsAny<DOSalesModel>())).Returns(new DOSalesViewModel()
            {
                ExportSalesContract = new FinishingPrintingSalesContractViewModel()
                {
                    SalesContractNo = "SalesContractNo",
                    PieceLength = "PieceLength",
                    OrderQuantity = 1,
                },
            });
            var controller = GetController(mocks);
            var response = controller.GetDOSalesPDF(1).Result;

            Assert.NotNull(response);
        }

        [Fact]
        public void Get_Sales_Receipt_PDF_NotFound()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(DOSalesModel));
            var controller = GetController(mocks);
            var response = controller.GetDOSalesPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);

        }

        [Fact]
        public void Get_Sales_Receipt_PDF_Exception()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("error"));
            var controller = GetController(mocks);
            var response = controller.GetDOSalesPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);

        }

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
        public void Validate_Validation_ViewModel_For_Local()
        {
            List<DOSalesViewModel> viewModels = new List<DOSalesViewModel>
            {
                new DOSalesViewModel{
                    DOSalesType = "Lokal",
                    LocalType = "",
                    LocalDate = DateTimeOffset.Now.AddYears(1),
                    LocalSalesContract = new FinishingPrintingSalesContractViewModel(){ },
                    DestinationBuyerName = "",
                    DestinationBuyerAddress = "",
                    LocalHeadOfStorage = "",
                    SalesName = "",
                    PackingUom = "",
                    MetricUom = "",
                    ImperialUom = "",
                    Disp = -1,
                    Op = -1,
                    Sc = -1,
                    DOSalesLocalItems = new List<DOSalesLocalViewModel>()
                    {
                        new DOSalesLocalViewModel()
                        {
                            TotalPacking = -1,
                            TotalMetric = -1,
                            TotalImperial = -1,
                        }
                    }
                }
            };
            foreach (var viewModel in viewModels)
            {
                var defaultValidationResult = viewModel.Validate(null);
                Assert.True(defaultValidationResult.Count() > 0);
            }
        }

        [Fact]
        public void Validate_Validation_ViewModel_For_Local_2()
        {
            List<DOSalesViewModel> viewModels = new List<DOSalesViewModel>
            {
                new DOSalesViewModel{
                    DOSalesType = "Lokal",
                    LocalType = null,
                    LocalDate = DateTimeOffset.Now.AddYears(-1),
                    LocalSalesContract = null,
                    DestinationBuyerName = "",
                    DestinationBuyerAddress = "",
                    LocalHeadOfStorage = "",
                    SalesName = "",
                    PackingUom = "",
                    MetricUom = "",
                    ImperialUom = "",
                    Disp = null,
                    Op = null,
                    Sc = null,
                    DOSalesLocalItems = null,
                }
            };
            foreach (var viewModel in viewModels)
            {
                var defaultValidationResult = viewModel.Validate(null);
                Assert.True(defaultValidationResult.Count() > 0);
            }
        }

        [Fact]
        public void Validate_Validation_ViewModel_For_Export()
        {
            List<DOSalesViewModel> viewModels = new List<DOSalesViewModel>
            {
                new DOSalesViewModel{
                    DOSalesType = "Ekspor",
                    ExportType = "",
                    ExportDate = DateTimeOffset.Now,
                    DoneBy = "",
                    ExportSalesContract = new FinishingPrintingSalesContractViewModel() { },
                    FillEachBale = -1,
                }
            };
            foreach (var viewModel in viewModels)
            {
                var defaultValidationResult = viewModel.Validate(null);
                Assert.True(defaultValidationResult.Count() > 0);
            }
        }

        [Fact]
        public void Validate_Null_Model_and_DetailViewModel()
        {
            List<DOSalesViewModel> viewModels = new List<DOSalesViewModel>
            {
            };
            foreach (var viewModel in viewModels)
            {
                var defaultValidationResult = viewModel.Validate(null);
                Assert.True(defaultValidationResult.Count() > 0);
            }
        }
    }
}
