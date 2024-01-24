namespace FarmerApp.Models.ViewModels.ResponseModels
{
	public class TreatmentResponseModel
	{
		public int Id { get; set; }
		public string DrugName { get; set; }
		public string DrugWeight { get; set; }
        public DateTime TreatmentDate { get; set; }
		public ICollection<ProductResponseModel> Products { get; set; }
    }
}

