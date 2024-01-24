namespace FarmerApp.Models.ViewModels.ResponseModels
{
    public class CustomerResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
		public string AccountNumber { get; set; }
		public string HVHH { get; set; }
        public IEnumerable<Sale> Sales { get; set; }
    }
}