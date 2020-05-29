using AutoMapper;
using Com.Danliris.Sales.Test.WebApi.Utils;
using Com.Danliris.Service.Sales.Lib.AutoMapperProfiles.DOReturnProfiles;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.DOReturn;
using Com.Danliris.Service.Sales.Lib.Models.DOReturn;
using Com.Danliris.Service.Sales.Lib.ViewModels.DOReturn;
using Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Com.Danliris.Service.Sales.Lib.ViewModels.SalesInvoice;
using Com.Danliris.Service.Sales.WebApi.Controllers;
using iTextSharp.text;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace Com.Danliris.Sales.Test.WebApi.Controllers
{
    public class DOReturnControllerTest : BaseControllerTest<DOReturnController, DOReturnModel, DOReturnViewModel, IDOReturnContract>
    {
        [Fact]
        public void Get_DO_Return_PDF_Success()
        {
            var vm = new DOReturnViewModel()
            {
                DOReturnType = "Type",
                DOReturnNo = "DOReturnNo",
                AutoIncreament = 1,
                DOReturnDate = DateTimeOffset.Now,
                HeadOfStorage = "HeadOfStorage",
                ReturnFrom = new BuyerViewModel()
                {
                    Id = 1,
                    Name = "ReturnFromName",
                },
                LTKPNo = "LKTPNo",
                Remark = "Remark",
                DOReturnDetails = new List<DOReturnDetailViewModel>()
                {
                    new DOReturnDetailViewModel()
                    {
                        SalesInvoice = new SalesInvoiceViewModel() { },
                        DOReturnDetailItems = new List<DOReturnDetailItemViewModel>()
                        {
                            new DOReturnDetailItemViewModel()
                            {
                                DOSales = new Service.Sales.Lib.ViewModels.DOSales.DOSalesViewModel() { },
                            }
                        },
                        DOReturnItems = new List<DOReturnItemViewModel>()
                        {
                            new DOReturnItemViewModel()
                            {
                                ShipmentDocumentId = 1,
                                ShipmentDocumentCode = "ShipmentDocumentCode",
                                ProductName = "ProductName",
                                ProductCode = "ProductCode",
                                Quantity = "Quantity",
                                PackingUom = "PackingUom",
                                Uom = new UomViewModel() { },
                                Total = 100,
                            }
                        },
                    }
                }
            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<DOReturnViewModel>(It.IsAny<DOReturnModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetDOReturnPDF(1).Result;

            Assert.NotNull(response);
        }

        [Fact]
        public void Get_DO_Return_PDF_NotFound()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(DOReturnModel));
            var controller = GetController(mocks);
            var response = controller.GetDOReturnPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);

        }

        [Fact]
        public void Get_DO_Return_PDF_Exception()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("error"));
            var controller = GetController(mocks);
            var response = controller.GetDOReturnPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);

        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DOReturnMapper>();
                cfg.AddProfile<DOReturnDetailMapper>();
                cfg.AddProfile<DOReturnDetailItemMapper>();
                cfg.AddProfile<DOReturnItemMapper>();
            });
            var mapper = configuration.CreateMapper();

            DOReturnViewModel salesInvoiceViewModel = new DOReturnViewModel { Id = 1 };
            DOReturnModel salesInvoiceModel = mapper.Map<DOReturnModel>(salesInvoiceViewModel);

            Assert.Equal(salesInvoiceViewModel.Id, salesInvoiceModel.Id);

            DOReturnDetailViewModel salesInvoiceDetailViewModel = new DOReturnDetailViewModel { Id = 1 };
            DOReturnDetailModel salesInvoiceDetailModel = mapper.Map<DOReturnDetailModel>(salesInvoiceDetailViewModel);

            Assert.Equal(salesInvoiceDetailViewModel.Id, salesInvoiceDetailModel.Id);

            DOReturnDetailItemViewModel salesInvoiceDetailItemViewModel = new DOReturnDetailItemViewModel { Id = 1 };
            DOReturnDetailItemModel salesInvoiceDetailItemModel = mapper.Map<DOReturnDetailItemModel>(salesInvoiceDetailItemViewModel);

            Assert.Equal(salesInvoiceDetailItemViewModel.Id, salesInvoiceDetailItemModel.Id);

            DOReturnItemViewModel salesInvoiceItemViewModel = new DOReturnItemViewModel { Id = 1 };
            DOReturnItemModel salesInvoiceItemModel = mapper.Map<DOReturnItemModel>(salesInvoiceItemViewModel);

            Assert.Equal(salesInvoiceItemViewModel.Id, salesInvoiceItemModel.Id);
        }

        [Fact]
        public void Validate_Validation_ViewModel()
        {
            List<DOReturnViewModel> viewModels = new List<DOReturnViewModel>
            {
                new DOReturnViewModel()
                {
                    ReturnFrom = new BuyerViewModel() {},
                    DOReturnDetails = new List<DOReturnDetailViewModel>()
                    {
                        new DOReturnDetailViewModel()
                        {
                            SalesInvoice = new SalesInvoiceViewModel()
                            {
                                Id = 1,
                                SalesInvoiceNo ="SalesInvoiceNo",
                            },
                            DOReturnDetailItems = new List<DOReturnDetailItemViewModel>()
                            {
                                new DOReturnDetailItemViewModel()
                                {
                                    DOSales = new Service.Sales.Lib.ViewModels.DOSales.DOSalesViewModel() {},
                                },
                                new DOReturnDetailItemViewModel() {},
                            },
                            DOReturnItems = new List<DOReturnItemViewModel>()
                            {
                                new DOReturnItemViewModel()
                                {
                                    Quantity = "10",
                                    Uom = new UomViewModel() {},
                                },
                                 new DOReturnItemViewModel() {},
                            }
                        },
                        new DOReturnDetailViewModel()
                        {
                            SalesInvoice = new SalesInvoiceViewModel()
                            {
                                Id = 1,
                                SalesInvoiceNo ="SalesInvoiceNo",
                            },
                            DOReturnDetailItems = new List<DOReturnDetailItemViewModel>() { },
                            DOReturnItems = new List<DOReturnItemViewModel>() { },
                        },



                        new DOReturnDetailViewModel()
                        {
                            SalesInvoice = new SalesInvoiceViewModel()
                            {
                                Id = 1,
                                SalesInvoiceNo ="SalesInvoiceNo",
                            },
                            DOReturnDetailItems = new List<DOReturnDetailItemViewModel>()
                            {
                                new DOReturnDetailItemViewModel()
                                {
                                    //DOReturnItems = new List<DOReturnItemViewModel>() { },
                                },
                            },
                        },


                    },
                },


                new DOReturnViewModel() {},
                new DOReturnViewModel() 
                {
                    ReturnFrom = new BuyerViewModel() {},
                },
                new DOReturnViewModel()
                {
                    ReturnFrom = new BuyerViewModel() {},
                    DOReturnDetails = new List<DOReturnDetailViewModel>()
                    {
                        new DOReturnDetailViewModel() { },
                    },
                },
                new DOReturnViewModel()
                {
                    ReturnFrom = new BuyerViewModel() {},
                    DOReturnDetails = new List<DOReturnDetailViewModel>()
                    {
                        new DOReturnDetailViewModel()
                        {
                            DOReturnDetailItems = new List<DOReturnDetailItemViewModel>()
                            {
                                new DOReturnDetailItemViewModel() { },
                            },
                        },
                    },
                },
                new DOReturnViewModel()
                {
                    ReturnFrom = new BuyerViewModel() {},
                    DOReturnDetails = new List<DOReturnDetailViewModel>()
                    {
                        new DOReturnDetailViewModel()
                        {
                            DOReturnDetailItems = new List<DOReturnDetailItemViewModel>()
                            {
                                new DOReturnDetailItemViewModel() {},
                            },
                            DOReturnItems = new List<DOReturnItemViewModel>()
                            {
                                new DOReturnItemViewModel() {},
                            },
                        },
                    },
                },
            };
            foreach (var viewModel in viewModels)
            {
                var defaultValidationResult = viewModel.Validate(null);
                Assert.True(defaultValidationResult.Count() > 0);
            }
        }
    }
}
