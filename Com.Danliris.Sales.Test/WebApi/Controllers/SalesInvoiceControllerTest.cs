using AutoMapper;
using Com.Danliris.Sales.Test.WebApi.Utils;
using Com.Danliris.Service.Sales.Lib.AutoMapperProfiles.SalesInvoiceProfiles;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.SalesInvoice;
using Com.Danliris.Service.Sales.Lib.Models.SalesInvoice;
using Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Com.Danliris.Service.Sales.Lib.ViewModels.SalesInvoice;
using Com.Danliris.Service.Sales.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace Com.Danliris.Sales.Test.WebApi.Controllers
{
    public class SalesInvoiceControllerTest : BaseControllerTest<SalesInvoiceController, SalesInvoiceModel, SalesInvoiceViewModel, ISalesInvoiceContract>
    {
        [Fact]
        public void Get_Delivery_Order_PDF_Success()
        {
            var vm = new SalesInvoiceViewModel()
            {
                DeliveryOrderNo = "DeliveryOrderNo",
                SalesInvoiceNo = "SalesInvoiceNo",
                SalesInvoiceDate = DateTimeOffset.Now,
                Buyer = new BuyerViewModel()
                {
                    Name = "BuyerName",
                    Code = "BuyerCode",
                    Address = "BuyerAddress",
                },
                Remark = "Remark",
                SalesInvoiceDetails = new List<SalesInvoiceDetailViewModel>()
                {
                    new SalesInvoiceDetailViewModel()
                    {
                        SalesInvoiceItems = new List<SalesInvoiceItemViewModel>()
                        {
                            new SalesInvoiceItemViewModel()
                            {
                                ProductName = "ProductName",
                                Uom = new UomViewModel()
                                {
                                    Unit = "PACKS",
                                },
                                Quantity = "Quantity",
                                PackingUom = "PackingUom",
                                Total = 1,
                            }
                        }
                    }
                }

            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<SalesInvoiceViewModel>(It.IsAny<SalesInvoiceModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetDeliveryOrderPDF(1).Result;

            Assert.NotNull(response);

        }

        [Fact]
        public void Get_Delivery_Order_PDF_NotFound()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(SalesInvoiceModel));
            var controller = GetController(mocks);
            var response = controller.GetDeliveryOrderPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);

        }

        [Fact]
        public void Get_Delivery_Order_PDF_Exception()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("error"));
            var controller = GetController(mocks);
            var response = controller.GetDeliveryOrderPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);

        }

        [Fact]
        public void Get_Sales_Invoice_PDF_VatType_Is_PPN_Umum_And_CurrencySymbol_Is_IDR()
        {
            var vm = new SalesInvoiceViewModel()
            {
                Buyer = new BuyerViewModel()
                {
                    Name = "BuyerName",
                    Code = "BuyerCode",
                    Address = "BuyerAddress",
                    NPWP = "BuyerNPWP",
                },
                SalesInvoiceNo = "SalesInvoiceNo",
                SalesInvoiceDate = DateTimeOffset.Now,
                DueDate = DateTimeOffset.Now,
                IDNo = "IDNo",
                Currency = new CurrencyViewModel()
                {
                    Symbol = "Rp",
                },
                Remark = "Remark",
                VatType = "PPN Umum",
                SalesInvoiceDetails = new List<SalesInvoiceDetailViewModel>()
                {
                    new SalesInvoiceDetailViewModel()
                    {
                        SalesInvoiceItems = new List<SalesInvoiceItemViewModel>()
                        {
                            new SalesInvoiceItemViewModel()
                            {
                        ProductCode = "ProductCode",
                        Quantity = "Quantity",
                        PackingUom = "PackingUom",
                        Uom = new UomViewModel()
                        {
                            Unit = "PACKS",
                        },
                        ProductName = "ProductName",
                        Total = 1,
                        Price = 1,
                        Amount = 1,
                    }
                }
                    }
                }

            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<SalesInvoiceViewModel>(It.IsAny<SalesInvoiceModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetSalesInvoicePDF(1).Result;

            Assert.NotNull(response);

        }
        [Fact]
        public void Get_Sales_Invoice_PDF_VatType_Is_PPN_BUMN_And_CurrencySymbol_Is_USD()
        {
            var vm = new SalesInvoiceViewModel()
            {
                Buyer = new BuyerViewModel()
                {
                    Name = "BuyerName",
                    Code = "BuyerCode",
                    Address = "BuyerAddress",
                    NPWP = "BuyerNPWP",
                },
                SalesInvoiceNo = "SalesInvoiceNo",
                SalesInvoiceDate = DateTimeOffset.Now,
                DueDate = DateTimeOffset.Now,
                IDNo = "IDNo",
                Currency = new CurrencyViewModel()
                {
                    Symbol = "$",
                },
                Remark = "Remark",
                VatType = "PPN BUMN",
                SalesInvoiceDetails = new List<SalesInvoiceDetailViewModel>()
                {
                    new SalesInvoiceDetailViewModel()
                    {
                        SalesInvoiceItems = new List<SalesInvoiceItemViewModel>()
                        {
                            new SalesInvoiceItemViewModel()
                            {
                        ProductCode = "ProductCode",
                        Quantity = "Quantity",
                        PackingUom = "PackingUom",
                        Uom = new UomViewModel()
                        {
                            Unit = "PACKS",
                        },
                        ProductName = "ProductName",
                        Total = 1,
                        Price = 1,
                        Amount = 1,
                    }
                } } }

            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<SalesInvoiceViewModel>(It.IsAny<SalesInvoiceModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetSalesInvoicePDF(1).Result;

            Assert.NotNull(response);

        }

        [Fact]
        public void Get_Sales_Invoice_PDF__Other_CurrencySymbol()
        {
            var vm = new SalesInvoiceViewModel()
            {
                Currency = new CurrencyViewModel()
                {
                    Symbol = "Rp",
                },
            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<SalesInvoiceViewModel>(It.IsAny<SalesInvoiceModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetSalesInvoicePDF(1).Result;

            Assert.NotNull(response);

        }


        [Fact]
        public void Get_Sales_Invoice_PDF_NotFound()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(SalesInvoiceModel));
            var controller = GetController(mocks);
            var response = controller.GetSalesInvoicePDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);

        }

        [Fact]
        public void Get_Sales_Invoice_PDF_Exception()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("error"));
            var controller = GetController(mocks);
            var response = controller.GetSalesInvoicePDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);

        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SalesInvoiceMapper>();
                cfg.AddProfile<SalesInvoiceDetailMapper>();
                cfg.AddProfile<SalesInvoiceItemMapper>();
            });
            var mapper = configuration.CreateMapper();

            SalesInvoiceViewModel salesInvoiceViewModel = new SalesInvoiceViewModel { Id = 1 };
            SalesInvoiceModel salesInvoiceModel = mapper.Map<SalesInvoiceModel>(salesInvoiceViewModel);

            Assert.Equal(salesInvoiceViewModel.Id, salesInvoiceModel.Id);

            SalesInvoiceDetailViewModel salesInvoiceDetailViewModel = new SalesInvoiceDetailViewModel { Id = 1 };
            SalesInvoiceDetailModel salesInvoiceDetailModel = mapper.Map<SalesInvoiceDetailModel>(salesInvoiceDetailViewModel);

            Assert.Equal(salesInvoiceDetailViewModel.Id, salesInvoiceDetailModel.Id);

            SalesInvoiceItemViewModel salesInvoiceItemViewModel = new SalesInvoiceItemViewModel { Id = 1 };
            SalesInvoiceItemModel salesInvoiceItemModel = mapper.Map<SalesInvoiceItemModel>(salesInvoiceItemViewModel);

            Assert.Equal(salesInvoiceItemViewModel.Id, salesInvoiceItemModel.Id);
        }

        [Fact]
        public void Validate_Validation_ViewModel()
        {
            List<SalesInvoiceViewModel> viewModels = new List<SalesInvoiceViewModel>
            {
                new SalesInvoiceViewModel{
                    SalesInvoiceType = "",
                    SalesInvoiceDate = DateTimeOffset.UtcNow.AddDays(1),
                    DeliveryOrderNo = "",
                    Currency = new CurrencyViewModel()
                    {
                        Id = 0,
                        Code = "",
                        Rate = 0,
                    },
                    Buyer = new BuyerViewModel()
                    {
                        Id = 0,
                        Name = "",
                        Code = "",
                    },
                    DueDate = DateTimeOffset.UtcNow.AddDays(-1),
                    TotalPayment = 0,
                    TotalPaid = -1,
                    //SalesInvoiceDetails = new List<SalesInvoiceDetailViewModel>()
                    //{
                    //    new SalesInvoiceDetailViewModel()
                    //    {
                    //    ShipmentDocumentId = 0,
                    //    ShipmentDocumentCode = "",
                    //        SalesInvoiceItems = new List<SalesInvoiceItemViewModel>()
                    //        {
                    //            new SalesInvoiceItemViewModel()
                    //            {
                    //                ProductCode = "",
                    //                Quantity = "",
                    //                Uom = new UomViewModel()
                    //                {
                    //                    Id = 0,
                    //                    Unit = "",
                    //                },
                    //                ProductName = "",
                    //                Amount = 0,
                    //            }                 
                    //        } 
                    //    } 
                    //}
                }
            };
            foreach (var viewModel in viewModels)
            {
                var defaultValidationResult = viewModel.Validate(null);
                Assert.True(defaultValidationResult.Count() > 0);
            }
        }

        [Fact]
        public void Validate_Duplicate_DetailViewModel()
        {
            List<SalesInvoiceViewModel> viewModels = new List<SalesInvoiceViewModel>
            {
                new SalesInvoiceViewModel {
                    DueDate = DateTimeOffset.Now,
                    Currency = new CurrencyViewModel() {},
                    SalesInvoiceDetails = new List<SalesInvoiceDetailViewModel>()
                    {
                        new SalesInvoiceDetailViewModel()
                        {
                            SalesInvoiceItems = new List<SalesInvoiceItemViewModel>()
                            {
                                new SalesInvoiceItemViewModel()
                                {
                                    SalesInvoiceDetailId = 2,
                                    ProductCode = "ProductCode",
                                    Quantity = "Quantity",
                                    PackingUom = "PackingUom",
                                    Total = 10,
                                    Uom = new UomViewModel()
                                    {
                                        Id = 10,
                                        Unit = "PCS",
                                    },
                                    ProductName = "ProductName",
                                    Price = 100,
                                    Amount = 100,
                                },
                                new SalesInvoiceItemViewModel()
                                {
                                    SalesInvoiceDetailId = 2,
                                    ProductCode = "ProductCode",
                                    Quantity = "Quantity",
                                    PackingUom = "PackingUom",
                                    Total = 10,
                                    Uom = new UomViewModel()
                                    {
                                        Id = 10,
                                        Unit = "PCS",
                                    },
                                    ProductName = "ProductName",
                                    Price = 100,
                                    Amount = 100,
                                },
                            }
                        },
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
        public void Should_Success_Read_By_Buyer()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.ReadByBuyerId(It.IsAny<int>())).Returns(new List<SalesInvoiceModel>() { new SalesInvoiceModel() });
            mocks.Mapper.Setup(m => m.Map<List<SalesInvoiceViewModel>>(It.IsAny<List<SalesInvoiceModel>>())).Returns(new List<SalesInvoiceViewModel>());
            var controller = GetController(mocks);
            var response = controller.ReadByBuyerId(It.IsAny<int>());
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void Should_ReturnFailed_Read_By_Buyer_ThrowException()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.ReadByBuyerId(It.IsAny<int>())).Returns(new List<SalesInvoiceModel>() { new SalesInvoiceModel() });
            mocks.Mapper.Setup(m => m.Map<List<SalesInvoiceViewModel>>(It.IsAny<List<SalesInvoiceModel>>())).Throws(new Exception());
            var controller = GetController(mocks);
            var response = controller.ReadByBuyerId(It.IsAny<int>());
            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }
    }
}
