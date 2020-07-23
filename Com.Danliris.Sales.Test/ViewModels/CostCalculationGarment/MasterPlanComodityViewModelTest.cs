using Com.Danliris.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Sales.Test.ViewModels.CostCalculationGarment
{
    public class MasterPlanComodityViewModelTest
    {
        [Fact]
        public void should_Success_Instantiate()
        {
            MasterPlanComodityViewModel viewModel = new MasterPlanComodityViewModel()
            {
                Name = "Name",
                Code = "Code",

            };

            Assert.Equal("Name", viewModel.Name);
            Assert.Equal("Code", viewModel.Code);
        }

    }
}
