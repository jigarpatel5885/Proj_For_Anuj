using Proj_Anuj.Models.ViewModels;
using Proj_Anuj.Models;

namespace Proj_Anuj.Services.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductVM>> GetAllProducts();

        Task<ProductVM> GetProduct(int Id);
        Task<IEnumerable<Colors>> GetColorsByProdudctId(int productId);

        Task<IEnumerable<Image>> GetImageByProdudctId(int productId);

    }
}
