using AutoMapper;
using Com.Danliris.Sales.Test.WebApi.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.AutoMapperProfiles.DeliveryNoteProductionProfiles;
using Com.Danliris.Service.Sales.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace Com.Danliris.Sales.Test.WebApi.Controllers
{
    public class DeliveryNoteProductionControllerTest : BaseControllerTest<DeliveryNoteProductionController, DeliveryNoteProductionModel, DeliveryNoteProductionViewModel, IDeliveryNoteProduction>
    {
        [Fact]
        public void Get_PDF_Success()
        {
            var vm = new DeliveryNoteProductionViewModel()
            {
                Code = "Code",
                Date = DateTimeOffset.Now,
                SalesContract = new SalesContract()
                {
                    Buyer = new Buyer()
                    {
                        Name = "name",
                        Type = "type"
                    },
                    Comodity = new Comodity()
                    {
                        Id = 1,
                        Name = "name",
                    },
                    ComodityDescription= "ComodityDescription",
                    OrderQuantity= 2,
                    SalesContractNo= "SalesContractNo",
                    UomUnit= "UomUnit",
                    DeliveredTo= "DeliveredTo"

                },

                //DestinationBuyerName = "DestinationBuyerName",
                Unit = "Unit",
                Subject = "Lainnya",
                OtherSubject = "OtherSubject",
                Month = "Month",
                Year = "Year",
                BallMark= "BallMark",
                Sample= "Sample",
                Remark= "Remark",
                YarnSales= "YarnSales",
                MonthandYear= "MonthandYear"

            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<DeliveryNoteProductionViewModel>(It.IsAny<DeliveryNoteProductionModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetDOSalesPDF(1).Result;

            Assert.NotNull(response);
        }

        [Fact]
        public void Get_PDF_Subject_Success()
        {
            var vm = new DeliveryNoteProductionViewModel()
            {
                Code = "Code",
                Date = DateTimeOffset.Now,
                SalesContract = new SalesContract()
                {
                    Buyer = new Buyer()
                    {
                        Name = "name",
                        Type = "type"
                    },
                    Comodity = new Comodity()
                    {
                        Id = 1,
                        Name = "name",
                    },
                    ComodityDescription = "ComodityDescription",
                    OrderQuantity = 2,
                    SalesContractNo = "SalesContractNo",
                    UomUnit = "UomUnit",
                    DeliveredTo = "DeliveredTo"

                },

                Unit = "Unit",
                Subject = "Order Jual",
                OtherSubject = "",
                Month = "Month",
                Year = "Year",
                BallMark = "BallMark",
                Sample = "Sample",
                Remark = "Remark",
                YarnSales = "YarnSales",
                MonthandYear = "MonthandYear"

            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<DeliveryNoteProductionViewModel>(It.IsAny<DeliveryNoteProductionModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetDOSalesPDF(1).Result;

            Assert.NotNull(response);
        }

        [Fact]
        public void Get_DO_Sales_PDF_NotFound()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(DeliveryNoteProductionModel));
            var controller = GetController(mocks);
            var response = controller.GetDOSalesPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);

        }

        [Fact]
        public void Get_DO_Sales_PDF_Exception()
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
                cfg.AddProfile<DeliveryNoteProductionMapper>();
            });
            var mapper = configuration.CreateMapper();

            DeliveryNoteProductionViewModel salesInvoiceViewModel = new DeliveryNoteProductionViewModel { Id = 1 };
            DeliveryNoteProductionModel salesInvoiceModel = mapper.Map<DeliveryNoteProductionModel>(salesInvoiceViewModel);

            Assert.Equal(salesInvoiceViewModel.Id, salesInvoiceModel.Id);

        }

    }
}
