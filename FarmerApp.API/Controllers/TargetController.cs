using AutoMapper;
using FarmerApp.API.Controllers;
using FarmerApp.Core.Models.Target;
using FarmerApp.Core.Services.Target;
using FarmerApp.Data.Entities;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.Controllers
{
    public class TargetsController : BaseController<TargetEntity, TargetModel, TargetResponseModel, TargetRequestModel, TargetRequestModel>
    {
        public TargetsController(IMapper mapper, ITargetService targetService)
           : base(targetService, mapper)
        {
        }
    }
}