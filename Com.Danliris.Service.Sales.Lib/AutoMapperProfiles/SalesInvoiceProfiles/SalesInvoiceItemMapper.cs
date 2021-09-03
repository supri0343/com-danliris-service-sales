using AutoMapper;
using Com.Danliris.Service.Sales.Lib.Models.SalesInvoice;
using Com.Danliris.Service.Sales.Lib.ViewModels.SalesInvoice;

namespace Com.Danliris.Service.Sales.Lib.AutoMapperProfiles.SalesInvoiceProfiles
{
    public class SalesInvoiceItemMapper : Profile
    {
        public SalesInvoiceItemMapper()
        {
            CreateMap<SalesInvoiceItemModel, SalesInvoiceItemViewModel>()

                .ReverseMap();
        }
    }
}
