using AutoMapper;
using Com.Danliris.Sales.Test.WebApi.Utils;
using Com.Danliris.Service.Sales.Lib.AutoMapperProfiles.SalesReceiptProfiles;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.SalesReceipt;
using Com.Danliris.Service.Sales.Lib.Models.SalesReceipt;
using Com.Danliris.Service.Sales.Lib.ViewModels.IntegrationViewModel;
using Com.Danliris.Service.Sales.Lib.ViewModels.SalesInvoice;
using Com.Danliris.Service.Sales.Lib.ViewModels.SalesReceipt;
using Com.Danliris.Service.Sales.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace Com.Danliris.Sales.Test.WebApi.Controllers
{
    public class SalesReceiptControllerTest : BaseControllerTest<SalesReceiptController, SalesReceiptModel, SalesReceiptViewModel, ISalesReceiptContract>
    {
        [Fact]
        public void Get_Sales_Receipt_PDF_Success()
        {
            var vm = new SalesReceiptViewModel()
            {
                SalesReceiptDate = DateTimeOffset.Now,
                Unit = new UnitViewModel()
                {
                    Name = "Name",
                },
                Buyer = new BuyerViewModel()
                {
                    Name = "Name",
                    Address = "Address",
                },
                Currency = new CurrencyViewModel()
                {
                    Code = "IDR",
                    Symbol = "Rp",
                    Rate = 14000,
                },
                Bank = new AccountBankViewModel()
                {
                    BankName = "BCA",
                },
                SalesReceiptDetails = new List<SalesReceiptDetailViewModel>()
                {
                    new SalesReceiptDetailViewModel()
                    {
                        VatType = "PPN BUMN",
                        SalesInvoice = new SalesInvoiceViewModel()
                        {
                            SalesInvoiceNo = "SalesInvoiceNo",
                            Currency = new CurrencyViewModel()
                            {
                                Code = "IDR",
                                Symbol = "Rp",
                                Rate = 14000,
                            },
                        },
                    }
                }

            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<SalesReceiptViewModel>(It.IsAny<SalesReceiptModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetSalesReceiptPDF(1).Result;

            Assert.NotNull(response);

        }

        [Fact]
        public void Get_Sales_Receipt_PDF_NotFound()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(SalesReceiptModel));
            var controller = GetController(mocks);
            var response = controller.GetSalesReceiptPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);

        }

        [Fact]
        public void Get_Sales_Receipt_PDF_Exception()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("error"));
            var controller = GetController(mocks);
            var response = controller.GetSalesReceiptPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);

        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SalesReceiptMapper>();
                cfg.AddProfile<SalesReceiptDetailMapper>();
            });
            var mapper = configuration.CreateMapper();

            SalesReceiptViewModel salesReceiptViewModel = new SalesReceiptViewModel { Id = 1 };
            SalesReceiptModel salesReceiptModel = mapper.Map<SalesReceiptModel>(salesReceiptViewModel);

            Assert.Equal(salesReceiptViewModel.Id, salesReceiptModel.Id);

            SalesReceiptDetailViewModel salesReceiptDetailViewModel = new SalesReceiptDetailViewModel { Id = 1 };
            SalesReceiptDetailModel salesReceiptDetailModel = mapper.Map<SalesReceiptDetailModel>(salesReceiptDetailViewModel);

            Assert.Equal(salesReceiptDetailViewModel.Id, salesReceiptDetailModel.Id);
        }

        [Fact]
        public void Validate_Validation_ViewModel()
        {
            List<SalesReceiptViewModel> viewModels = new List<SalesReceiptViewModel>
            {
                new SalesReceiptViewModel{
                    SalesReceiptDate = DateTimeOffset.UtcNow.AddDays(-1),
                    Unit = new UnitViewModel()
                    {
                        Id = 0,
                        Name = "",
                    },
                    Buyer = new BuyerViewModel()
                    {
                        Id = 0,
                        Name = "",
                        Address = "",
                    },
                    OriginAccountNumber = "",
                    Currency = new CurrencyViewModel()
                    {
                        Id = 0,
                        Code = "",
                        Symbol = "",
                        Rate = 0,
                    },
                    Bank = new AccountBankViewModel()
                    {
                        Id = 0,
                        AccountName = "",
                        AccountNumber = "",
                        BankName = "",
                        Code = "",
                    },
                    AdministrationFee = -1,
                    TotalPaid = -1,
                    SalesReceiptDetails = new List<SalesReceiptDetailViewModel>{
                        new SalesReceiptDetailViewModel{
                            SalesReceiptId = 0,
                            SalesInvoice = new SalesInvoiceViewModel()
                            {
                                Id = 0,
                                SalesInvoiceNo = "",
                                Currency = new CurrencyViewModel()
                                {
                                    Id = 0,
                                    Code = "",
                                    Symbol = "",
                                    Rate = 0,
                                },
                            },
                            DueDate = DateTimeOffset.UtcNow.AddDays(-1),
                            VatType = "",
                            Tempo = -1,
                            TotalPayment = -1,
                            TotalPaid = -1,
                            Paid = -1,
                            Nominal = -1,
                            Unpaid = -1,
                            IsPaidOff = false
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
        public void Validate_Duplicate_DetailViewModel()
        {
            List<SalesReceiptViewModel> viewModels = new List<SalesReceiptViewModel>
            {
                new SalesReceiptViewModel{
                    Unit = new UnitViewModel()
                    {
                        Id = 14,
                    },
                    Buyer = new BuyerViewModel()
                    {
                        Id = 28,
                    },
                    Currency = new CurrencyViewModel()
                    {
                        Id = 8,
                    },
                    Bank = new AccountBankViewModel()
                    {
                        Id = 98,
                    },
                    SalesReceiptDetails = new List<SalesReceiptDetailViewModel>{
                        new SalesReceiptDetailViewModel{
                            SalesReceiptId = 10,
                            SalesInvoice = new SalesInvoiceViewModel()
                            {
                                Id = 10,
                                SalesInvoiceNo = "SalesInvoiceNo",
                                Currency = new CurrencyViewModel()
                            {
                                Id = 10,
                                Code = "USD",
                                Symbol = "$",
                                Rate = 10,
                            },
                            },
                            DueDate = DateTimeOffset.UtcNow,
                            VatType = "PPN Kawasan Berikat",
                            Tempo = 10,
                            TotalPayment = 10,
                            TotalPaid = 10,
                            Paid = 10,
                            Nominal = 10,
                            Unpaid = 10,
                            OverPaid = 10,
                            IsPaidOff = true
                        },
                        new SalesReceiptDetailViewModel{
                            SalesReceiptId = 10,
                            SalesInvoice = new SalesInvoiceViewModel()
                            {
                                Id = 10,
                                SalesInvoiceNo = "SalesInvoiceNo",
                                Currency = new CurrencyViewModel()
                                {
                                    Id = 10,
                                    Code = "USD",
                                    Symbol = "$",
                                    Rate = 10,
                                },
                            },
                            DueDate = DateTimeOffset.UtcNow,
                            VatType = "PPN Kawasan Berikat",
                            Tempo = 10,
                            TotalPayment = 10,
                            TotalPaid = 10,
                            Paid = 10,
                            Nominal = 10,
                            Unpaid = 10,
                            OverPaid = 10,
                            IsPaidOff = true
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
        public void Validate_CurrencySymbol_And_VatType_For_PDF()
        {
            List<SalesReceiptViewModel> viewModels = new List<SalesReceiptViewModel>
            {
                new SalesReceiptViewModel{
                    Unit = new UnitViewModel()
                    {
                        Id = 8,
                    },
                    Buyer = new BuyerViewModel()
                    {
                        Id = 14,
                    },
                    Currency = new CurrencyViewModel()
                    {
                        Id = 2,
                    },
                    Bank = new AccountBankViewModel()
                    {
                        Id = 18,
                    },
                    SalesReceiptDetails = new List<SalesReceiptDetailViewModel>{
                        new SalesReceiptDetailViewModel{
                            VatType = "PPN Umum",
                            SalesInvoice = new SalesInvoiceViewModel()
                            {
                                Id = 1,
                                Currency = new CurrencyViewModel()
                                {
                                    Id = 20,
                                    Symbol = "$",
                                    Rate = 1000,
                                },
                            },
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
    }
}
