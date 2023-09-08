using Com.Danliris.Service.Sales.Lib.Utilities.BaseInterface;
using Com.Danliris.Service.Sales.Lib.ViewModels.DyeingPrintingReport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.DyeingPrintingReportInterface
{
    public interface IMachineQueueReport //: IBaseMonitoringFacade<MachineQueueReportViewModel>
    {
        Task<Tuple<MemoryStream, string>> GenerateExcel(string filter = "{}");
        Task<Tuple<List<MachineQueueReportViewModel>, int>> Read(int page = 1, int size = 25, string filter = "{}");
    }
}
