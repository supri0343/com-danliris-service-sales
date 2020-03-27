using AutoMapper;
using Com.Danliris.Service.Sales.Lib.Models.SalesReceipt;
using Com.Danliris.Service.Sales.Lib.ViewModels.SalesReceipt;

namespace Com.Danliris.Service.Sales.Lib.AutoMapperProfiles.SalesReceiptProfiles
{
    public class SalesReceiptMapper : Profile
    {
        public SalesReceiptMapper()
        {
            CreateMap<SalesReceiptModel, SalesReceiptViewModel>()

                //.ForPath(d => d.Unit.Id, opt => opt.MapFrom(s => s.UnitId))
                //.ForPath(d => d.Unit.Name, opt => opt.MapFrom(s => s.UnitName))
                
                .ForPath(d => d.Buyer.Id, opt => opt.MapFrom(s => s.BuyerId))
                .ForPath(d => d.Buyer.Name, opt => opt.MapFrom(s => s.BuyerName))
                .ForPath(d => d.Buyer.Address, opt => opt.MapFrom(s => s.BuyerAddress))

                .ForPath(d => d.Currency.Id, opt => opt.MapFrom(s => s.CurrencyId))
                .ForPath(d => d.Currency.Code, opt => opt.MapFrom(s => s.CurrencyCode))
                .ForPath(d => d.Currency.Symbol, opt => opt.MapFrom(s => s.CurrencySymbol))
                .ForPath(d => d.Currency.Rate, opt => opt.MapFrom(s => s.CurrencyRate))

                .ForPath(d => d.Bank.Id, opt => opt.MapFrom(s => s.BankId))
                .ForPath(d => d.Bank.AccountName, opt => opt.MapFrom(s => s.AccountName))
                .ForPath(d => d.Bank.AccountNumber, opt => opt.MapFrom(s => s.AccountNumber))
                .ForPath(d => d.Bank.BankName, opt => opt.MapFrom(s => s.BankName))
                .ForPath(d => d.Bank.Code, opt => opt.MapFrom(s => s.BankCode))


                .ReverseMap();
        }
    }
}
