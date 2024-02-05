using FarmerApp.Core.Models.Interfaces;
using FarmerApp.Core.Models.Product;
using FarmerApp.Core.Models.User;

namespace FarmerApp.Core.Models.Treatment
{
    public class TreatmentModel : BaseModel, IHasUserModel
    {
        public string DrugName { get; set; }
        public string DrugWeight { get; set; }
        public DateTime? Date { get; set; }

        public string TreatedProductsIds { get; set; }

        public int? UserId { get; set; }
        public UserModel User { get; set; }

        public ICollection<ProductModel> Products { get; set; }

    }
}