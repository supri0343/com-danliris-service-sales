using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.DOReturn;
using Com.Danliris.Service.Sales.Lib.Models.DOReturn;
using Com.Danliris.Service.Sales.Lib.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;

namespace Com.Danliris.Sales.Test.BussinesLogic.Logic.DOReturn
{
  public  class DOReturnItemLogicTest
    {
        private const string ENTITY = "DOReturnItemLogic";

        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
        }

        private SalesDbContext _dbContext(string testName)
        {
            DbContextOptionsBuilder<SalesDbContext> optionsBuilder = new DbContextOptionsBuilder<SalesDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            SalesDbContext dbContext = new SalesDbContext(optionsBuilder.Options);

            return dbContext;
        }

        public Mock<IServiceProvider> GetServiceProvider(string testname)
        {
            IIdentityService identityService = new IdentityService { Username = "Username" };
            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);
            
            serviceProvider.Setup(s => s.GetService(typeof(SalesDbContext)))
                .Returns(_dbContext(testname));

            return serviceProvider;
        }

        [Fact]
        public void Read_With_EmptyKeyword_Return_Success()
        {
            string testName = GetCurrentMethod();
            var dbContext = _dbContext(testName);
            IIdentityService identityService = new IdentityService { Username = "Username" };
            DOReturnItemLogic unitUnderTest = new DOReturnItemLogic(GetServiceProvider(testName).Object, identityService, dbContext);
            dbContext.DOReturnItems.Add(new DOReturnItemModel()
            {
                ProductName= "ProductName",
                Active = true,
                CreatedBy = "someone",
                ProductCode = "ProductCode",
              
                DOReturnDetailItemModel =new DOReturnDetailItemModel() { 
                    Active =true,
                    DOSalesNo = "DOSalesNo",
                },
                UId="1",
                ShipmentDocumentId = 1,
                ShipmentDocumentCode = "ShipmentDocumentCode",
                UomUnit = "UomUnit",
                UomId=1,
                CreatedUtc =DateTime.UtcNow,
                LastModifiedBy = "someone",
                Total=10000,
                Quantity="",
                PackingUom ="",
                CreatedAgent = "CreatedAgent",
                DeletedAgent = "DeletedAgent",
                IsDeleted = false,
                LastModifiedUtc = DateTime.UtcNow,
                LastModifiedAgent = "LastModifiedAgent"

            }); ;
            dbContext.SaveChanges();
            int page = 1;
            int size = 1;
            string order = "{}";
            string keyword = null;
            string filter = @"{""ProductName"":""""}";

            var result = unitUnderTest.Read(page, size, order, new List<string>() { "" }, keyword, "{}");
            Assert.NotEmpty(result.Data);
        }
}
}
