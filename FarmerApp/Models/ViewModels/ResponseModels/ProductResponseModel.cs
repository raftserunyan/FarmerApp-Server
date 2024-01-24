namespace FarmerApp.Models.ViewModels.ResponseModels
{
    public class ProductResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
		public int PriceKG { get; set; }
		public IEnumerable<Sale> Sales { get; set; }
    }
}