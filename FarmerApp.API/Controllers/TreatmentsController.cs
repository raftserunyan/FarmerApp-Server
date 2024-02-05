using AutoMapper;
using FarmerApp.API.Controllers;
using FarmerApp.Core.Models.Treatment;
using FarmerApp.Core.Services.Treatment;
using FarmerApp.Data.Entities;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.Controllers
{
    public class TreatmentsController : BaseController<TreatmentEntity, TreatmentModel, TreatmentResponseModel, TreatmentRequestModel, TreatmentRequestModel>
    {
        public TreatmentsController(IMapper mapper, ITreatmentService treatmentService)
           : base(treatmentService, mapper)
        {
        }
    }
}