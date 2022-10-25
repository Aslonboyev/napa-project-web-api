using NapaProject.Models;
using NapaProject.Services.ViewModels.Products;
using System.Linq.Expressions;

namespace NapaProject.Services.Interfaces
{
    public interface IProductAuditService
    {
        Task<ProductAuditViewModel> GetAsync(Expression<Func<ProductAudit, bool>> expression);

        Task<IEnumerable<ProductAuditViewModel>> GetAllAsync(Expression<Func<ProductAudit, bool>> expression = null);
    }
}
