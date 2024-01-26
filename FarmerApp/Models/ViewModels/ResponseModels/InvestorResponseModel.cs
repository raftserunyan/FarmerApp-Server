namespace FarmerApp.Models.ViewModels.ResponseModels
{
	public class InvestorResponseModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
        public string PhoneNumber { get; set; }
		public ICollection<InvestmentResponseModel> Investments { get; set; }
	}
}

