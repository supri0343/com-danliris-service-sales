using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.DOAval;
using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.DOSales;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.DOAval;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.DOAval;
using Com.Danliris.Service.Sales.Lib.Models.DOSales;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.DOAval
{
    public class DOAvalFacadeTest : BaseFacadeTest<SalesDbContext, DOAvalFacade, DOAvalLogic, DOSalesModel, DOAvalDataUtil>
    {
        private const string ENTITY = "DOAval";
        public DOAvalFacadeTest() : base(ENTITY)
        {
        }


    }
}
