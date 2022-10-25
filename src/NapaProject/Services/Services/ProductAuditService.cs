using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NapaProject.AppDbContexts;
using NapaProject.Models;
using NapaProject.Services.Exceptions;
using NapaProject.Services.Interfaces;
using NapaProject.Services.ViewModels.Products;
using System.Linq.Expressions;
using System.Net;

namespace NapaProject.Services.Services
{
    public class ProductAuditService : IProductAuditService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductAuditService(AppDbContext appDbContext, IMapper mapper)
        {
            _context = appDbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductAuditViewModel>> GetAllAsync(Expression<Func<ProductAudit, bool>> expression = null)
        {
            if (expression is null)
                expression = p => true;

            return from product in _context.ProductAudits.Where(expression)
                   select _mapper.Map<ProductAuditViewModel>(product);
        }

        public async Task<ProductAuditViewModel> GetAsync(Expression<Func<ProductAudit, bool>> expression)
        {
            var product = await _context.ProductAudits.FirstOrDefaultAsync(expression);

            if (product is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, message: "Product not found");

            return _mapper.Map<ProductAuditViewModel>(product);
        }
    }
}
