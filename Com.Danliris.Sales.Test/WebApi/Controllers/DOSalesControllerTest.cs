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
                Date = DateTimeOffset.Now,
                HeadOfStorage = "HeadOfStorage",
                Buyer = new Service.Sales.Lib.ViewModels.IntegrationViewModel.BuyerViewModel()
                {
                    Name = "BuyerName",
                },
                //DestinationBuyerName = "DestinationBuyerName",
                PackingUom = "PCS",
                LengthUom = "MTR",
                Disp = 1,
                Op = 1,
                Sc = 1,
                SalesContract = new FinishingPrintingSalesContractViewModel()
                {
                    SalesContractNo = "SalesContractNo",
                    Buyer = new Service.Sales.Lib.ViewModels.IntegrationViewModel.BuyerViewModel()
                    {
                        Name = "BuyerName",
                    },
                    Material = new Service.Sales.Lib.ViewModels.IntegrationViewModel.ProductViewModel()
                    {
                        Name = "MaterialName",
                    },
                    MaterialConstruction = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialConstructionViewModel()
                    {
                        Name = "MaterialConstructionName",
                    },
                },
                DOSalesDetailItems = new List<DOSalesDetailViewModel>()
                {
                    new DOSalesDetailViewModel()
                    {
                        ProductionOrder = new ProductionOrderViewModel()
                        {
                            OrderNo = "OrderNo",
                            Material = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialViewModel()
                            {
                                Name = "MaterialName",
                            },
                            MaterialConstruction = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialConstructionViewModel()
                            {
                                Name = "MaterialConstructionName",
                            },
                        },
                        UnitOrCode = "UnitCode",
                        Packing = 1,
                        Length = 1,
                        ConvertionValue = 1,
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

        [Fact]
        public void Get_DO_Sales_Export_PDF_Success()
        {
            var vm = new DOSalesViewModel()
            {
                DOSalesType = "Ekspor",
                DOSalesNo = "DOSalesNo",
                Date = DateTimeOffset.Now,
                DoneBy = "DoneBy",
                PackingUom = "PT",
                WeightUom = "BALE",
                SalesContract = new FinishingPrintingSalesContractViewModel()
                {
                    SalesContractNo = "SalesContractNo",
                    MaterialConstruction = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialConstructionViewModel()
                    {
                        Name = "MaterialConstructionName",
                    },
                    Buyer = new Service.Sales.Lib.ViewModels.IntegrationViewModel.BuyerViewModel()
                    {
                        Name = "BuyerName",
                    },
                    PieceLength = "PieceLength",
                    Commodity = new Service.Sales.Lib.ViewModels.IntegrationViewModel.CommodityViewModel()
                    {
                        Name = "CommodityName",
                    },
                    OrderQuantity = 1,
                },
                FillEachBale = 1,
                Remark = "Remark",
                DOSalesDetailItems = new List<DOSalesDetailViewModel>()
                {
                    new DOSalesDetailViewModel()
                    {
                        ProductionOrder = new ProductionOrderViewModel()
                        {
                            OrderNo = "OrderNo",
                            Material = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialViewModel()
                            {
                                Name = "MaterialName",
                            },
                            MaterialConstruction = new Service.Sales.Lib.ViewModels.IntegrationViewModel.MaterialConstructionViewModel()
                            {
                                Name = "MaterialConstructionName",
                            },
                        },
                        UnitOrCode = "UnitCode",
                        Packing = 1,
                        Weight = 1,
                        ConvertionValue = 1,
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
                cfg.AddProfile<DOSalesDetailMapper>();
            });
            var mapper = configuration.CreateMapper();

            DOSalesViewModel salesInvoiceViewModel = new DOSalesViewModel { Id = 1 };
            DOSalesModel salesInvoiceModel = mapper.Map<DOSalesModel>(salesInvoiceViewModel);

            Assert.Equal(salesInvoiceViewModel.Id, salesInvoiceModel.Id);

            DOSalesDetailViewModel salesInvoiceDetailViewModel = new DOSalesDetailViewModel { Id = 1 };
            DOSalesDetailModel salesInvoiceDetailModel = mapper.Map<DOSalesDetailModel>(salesInvoiceDetailViewModel);

            Assert.Equal(salesInvoiceDetailViewModel.Id, salesInvoiceDetailModel.Id);
        }

        [Fact]
        public void Validate_Validation_DetailViewModel()
        {
            List<DOSalesViewModel> viewModels = new List<DOSalesViewModel>
            {
                new DOSalesViewModel{
                    DOSalesType = "Lokal",
                    DOSalesDetailItems = new List<DOSalesDetailViewModel>()
                    {
                        new DOSalesDetailViewModel() { },
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
        public void Validate_Validation_ViewModel_For_Local()
        {
            List<DOSalesViewModel> viewModels = new List<DOSalesViewModel>
            {
                new DOSalesViewModel{
                    DOSalesType = "Lokal",
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
