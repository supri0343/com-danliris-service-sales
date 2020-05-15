using Com.Danliris.Sales.Test.BussinesLogic.Utils;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.DOReturn;
using Com.Danliris.Service.Sales.Lib.Models.DOReturn;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.Danliris.Sales.Test.BussinesLogic.DataUtils.DOReturn
{
    public class DOReturnDataUtil : BaseDataUtil<DOReturnFacade, DOReturnModel>
    {
        public DOReturnDataUtil(DOReturnFacade facade) : base(facade)
        {
        }

        public override async Task<DOReturnModel> GetNewData()
        {
            return new DOReturnModel()
            {
                Code = "code",
                AutoIncreament = 1,
                DOReturnNo = "DOReturnNo",
                Type = "Type",
                Date = DateTimeOffset.UtcNow,
                ReturnFrom = "ReturnFrom",
                LKTPNo = "LKTPNo",
                HeadOfStorage = "HeadOfStorage",
                Remark = "Remark",
                DOReturnDetails = new List<DOReturnDetailModel>()
                {
                    new DOReturnDetailModel()
                    {
                        SalesInvoiceId = 1,
                        SalesInvoiceNo = "SalesInvoiceNo",
                        DOReturnDetailItems = new List<DOReturnDetailItemModel>()
                        {
                            new DOReturnDetailItemModel()
                            {
                                DOSalesId = 1,
                                DOSalesNo = "DOSAalesNo",
                                DOReturnItems = new List<DOReturnItemModel>()
                                {
                                    new DOReturnItemModel()
                                    {
                                        ShipmentDocumentId = 1,
                                        ShipmentDocumentCode = "ShipmentDocumentCode",
                                        ProductCode = "ProductCode",
                                        ProductName = "ProductName",
                                        Quantity = "Quantity",
                                        PackingUom = "PackingUom",
                                        UomId = 1,
                                        UomUnit = "UomUnit",
                                        Total = 100,
                                    },
                                },
                            },
                        },
                    },
                },
            };
        }
    }
}
