using Com.Danliris.Service.Sales.Lib.ViewModels.ProductionOrder;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Sales.Test.ViewModels.ProductionOrder
{
  public  class ProductionOrder_RunWidthViewModelTest
    {
        [Fact]
        public void Validate_Throws_NotImplementedException()
        {
            ProductionOrder_RunWidthViewModel viewModel = new ProductionOrder_RunWidthViewModel();
        
            Assert.Throws<NotImplementedException>(()=>viewModel.Validate(null));
        }
    }
}
