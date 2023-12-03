using EcommerceApp.Models.Domin;

namespace EcommerceApp.Models.DTO
{
    public class GetCategoryDTO
    {
        public string Name { get; set; }

        public List<Products> Products { get; set; }

    }
}
