using AutoMapper;
using Com.Danliris.Service.Sales.Lib.Models.GarmentMasterPlan.MaxWHConfirmModel;
using Com.Danliris.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MaxWHConfirmViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.AutoMapperProfiles.GarmentMasterPlanProfiles
{
    public class MaxWHConfirmProfile : Profile
    {
        public MaxWHConfirmProfile()
        {
            CreateMap<MaxWHConfirm, MaxWHConfirmViewModel>()
                .ReverseMap();
        }
    }
}
