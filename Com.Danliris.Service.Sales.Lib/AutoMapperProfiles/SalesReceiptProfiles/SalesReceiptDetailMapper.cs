using AutoMapper;
using Com.Danliris.Service.Sales.Lib.Models.SalesReceipt;
using Com.Danliris.Service.Sales.Lib.ViewModels.SalesReceipt;

namespace Com.Danliris.Service.Sales.Lib.AutoMapperProfiles.SalesReceiptProfiles
{
    public class SalesReceiptDetailMapper : Profile
    {
        public SalesReceiptDetailMapper()
        {
            CreateMap<SalesReceiptDetailModel, SalesReceiptDetailViewModel>()

                .ForPath(d => d.SalesInvoice.Id, opt => opt.MapFrom(s => s.SalesInvoiceId))
                .ForPath(d => d.SalesInvoice.SalesInvoiceNo, opt => opt.MapFrom(s => s.SalesInvoiceNo))

                .ForPath(d => d.SalesInvoice.Currency.Id, opt => opt.MapFrom(s => s.CurrencyId))
                .ForPath(d => d.SalesInvoice.Currency.Code, opt => opt.MapFrom(s => s.CurrencyCode))
                .ForPath(d => d.SalesInvoice.Currency.Symbol, opt => opt.MapFrom(s => s.CurrencySymbol))
                .ForPath(d => d.SalesInvoice.Currency.Rate, opt => opt.MapFrom(s => s.CurrencyRate))

                .ReverseMap();
        }
    }
}
