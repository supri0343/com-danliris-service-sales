using AutoMapper;
using Com.Danliris.Service.Sales.Lib.Models.SalesInvoiceExport;
using Com.Danliris.Service.Sales.Lib.ViewModels.SalesInvoiceExport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.AutoMapperProfiles.SalesInvoiceExportProfiles
{
    public class SalesInvoiceExportMapper : Profile
    {
        public SalesInvoiceExportMapper()
        {
            CreateMap<SalesInvoiceExportModel, SalesInvoiceExportViewModel>()

                .ReverseMap();
        }
    }
}
