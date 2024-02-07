namespace FarmerApp.API.Models.ViewModels.ResponseModels.Investment
{
    public class InvestorResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<InvestmentResponseModel> Investments { get; set; }
    }
}

