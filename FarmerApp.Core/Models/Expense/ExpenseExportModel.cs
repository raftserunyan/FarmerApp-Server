namespace FarmerApp.Core.Models.Expense
{
    public class ExpenseExportModel
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Target { get; set; }
        public string Investor { get; set; }
        public DateTime? Date { get; set; }
    }
}
