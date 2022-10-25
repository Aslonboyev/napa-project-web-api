using NapaProject.Models;
using NapaProject.Services.ViewModels.Products;
using System.Linq.Expressions;

namespace NapaProject.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductViewModel> GetAsync(Expression<Func<Product, bool>> expression);

        Task<ProductViewModelForAdmin> GetForAdminAsync(Expression<Func<Product, bool>> expression);

        Task DeleteAsync(Expression<Func<Product, bool>> expression);

        Task<ProductViewModel> CreateAsync(ProductCreateViewModel model);

        Task UpdateAsync(long id, ProductPatchViewModel model);

        Task<IEnumerable<ProductViewModel>> GetAllAsync(Expression<Func<Product, bool>> expression = null!);

        Task<IEnumerable<ProductViewModelForAdmin>> GetAllForAdminAsync(Expression<Func<Product, bool>> expression = null!);
    }
}