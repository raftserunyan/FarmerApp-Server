namespace FarmerApp.Core.Query;

public interface IOrdering
{
    List<OrderingItem> Orderings { get; set; }
}