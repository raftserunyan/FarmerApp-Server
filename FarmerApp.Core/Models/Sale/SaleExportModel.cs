namespace FarmerApp.Core.Models.Sale
{
    public class SaleExportModel
    {
        public string Product { get; set; }
        public double Weight { get; set; }
        public int PriceKG { get; set; }
        public string Customer { get; set; }
        public int Paid { get; set; }
        public double Credit { get; set; }
        public DateTime? Date { get; set; }
    }
}
