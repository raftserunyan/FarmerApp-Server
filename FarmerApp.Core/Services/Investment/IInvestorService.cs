﻿using FarmerApp.Core.Models.Investment;
using FarmerApp.Core.Services.Common;
using FarmerApp.Data.Entities;

namespace FarmerApp.Core.Services.Investment
{
    public interface IInvestorService : ICommonService<InvestorModel, InvestorEntity>
    {
    }
}