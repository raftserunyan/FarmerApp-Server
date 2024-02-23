using AutoMapper;
using FarmerApp.API.Controllers;
using FarmerApp.API.Models.ViewModels.ResponseModels.Treatment;
using FarmerApp.Core.Models.Treatment;
using FarmerApp.Core.Query;
using FarmerApp.Core.Services.Treatment;
using FarmerApp.Core.Wrappers;
using FarmerApp.Data.Entities;
using FarmerApp.Data.Specifications.Treatment;
using FarmerApp.Models.ViewModels.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FarmerApp.Controllers
{
    public class TreatmentsController : BaseController<TreatmentEntity, TreatmentModel, TreatmentResponseModel, TreatmentRequestModel, TreatmentRequestModel>
    {
        public TreatmentsController(IMapper mapper, ITreatmentService treatmentService)
           : base(treatmentService, mapper)
        {
        }

        public override async Task<ActionResult<PagedResult<TreatmentResponseModel>>> Read([FromBody] BaseQueryModel query)
        {
            var treatments = await _service.GetAll(new TreatmentsWithDepsSpecification(), query);

            return _mapper.Map<PagedResult<TreatmentResponseModel>>(treatments);
        }

        public override async Task<ActionResult<TreatmentResponseModel>> Create([BindRequired, FromBody] TreatmentRequestModel model)
        {
            var businessModel = _mapper.Map<TreatmentModel>(model);
            businessModel.UserId = UserId;

            var data = await _service.Add(businessModel);

            // Fix this when depth builder is available
            return await ReadById((int)data.Id);
        }
    }
}