using Com.Danliris.Sales.Test.BussinesLogic.DataUtils.Garment.GarmentMerchandiser;
using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic;
using Com.Danliris.Service.Sales.Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Sales.Test.BussinesLogic.Facades.Garment.GarmentMerchandiser
{
    public class RateFacadeTest : BaseFacadeTest<SalesDbContext, RateFacade, RateLogic, Rate, RateDataUtil>
    {
        private const string ENTITY = "Rate";

        public RateFacadeTest() : base(ENTITY)
        {
        }
    }
}
