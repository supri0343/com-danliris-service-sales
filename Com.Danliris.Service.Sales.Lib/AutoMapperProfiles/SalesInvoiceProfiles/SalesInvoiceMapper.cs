using AutoMapper;
using Com.Danliris.Service.Sales.Lib.Models.SalesInvoice;
using Com.Danliris.Service.Sales.Lib.ViewModels.SalesInvoice;

namespace Com.Danliris.Service.Sales.Lib.AutoMapperProfiles.SalesInvoiceProfiles
{
    public class SalesInvoiceMapper : Profile
    {
        public SalesInvoiceMapper()
        {
            CreateMap<SalesInvoiceModel, SalesInvoiceViewModel>()

                .ForPath(d => d.Buyer.Id, opt => opt.MapFrom(s => s.BuyerId))
                .ForPath(d => d.Buyer.Name, opt => opt.MapFrom(s => s.BuyerName))
                .ForPath(d => d.Buyer.Address, opt => opt.MapFrom(s => s.BuyerAddress))
                .ForPath(d => d.Buyer.NPWP, opt => opt.MapFrom(s => s.BuyerNPWP))

                .ForPath(d => d.Currency.Id, opt => opt.MapFrom(s => s.CurrencyId))
                .ForPath(d => d.Currency.Code, opt => opt.MapFrom(s => s.CurrencyCode))
                .ForPath(d => d.Currency.Symbol, opt => opt.MapFrom(s => s.CurrencySymbol))
                .ForPath(d => d.Currency.Rate, opt => opt.MapFrom(s => s.CurrencyRate))

                .ReverseMap();
        }
    }
}
