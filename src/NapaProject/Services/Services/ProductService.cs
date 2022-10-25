using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NapaProject.AppDbContexts;
using NapaProject.Models;
using NapaProject.Services.AutoMappers;
using NapaProject.Services.Exceptions;
using NapaProject.Services.Helpers;
using NapaProject.Services.Interfaces;
using NapaProject.Services.ViewModels.Products;
using System.Linq.Expressions;
using System.Net;

namespace NapaProject.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(AppDbContext appDbContext, IMapper mapper)
        {
            _context = appDbContext;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();
        }

        public async Task DeleteAsync(Expression<Func<Product, bool>> expression)
        {
            var product = await _context.Products.FirstOrDefaultAsync(expression);

            if (product is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, message: "Post not found");

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync(Expression<Func<Product, bool>> expression = null)
        {
            if (expression is null)
                expression = p => true;

            return from blog in _context.Products.Where(expression)
                   select _mapper.Map<ProductViewModel>(blog);
        }

        public async Task<IEnumerable<ProductViewModelForAdmin>> GetAllForAdminAsync(Expression<Func<Product, bool>> expression = null)
        {
            if (expression is null)
                expression = p => true;

            return from blog in _context.Products.Where(expression)
                   select _mapper.Map<ProductViewModelForAdmin>(blog);
        }

        public async Task<ProductViewModel> GetAsync(Expression<Func<Product, bool>> expression)
        {
            var product = await _context.Products.FirstOrDefaultAsync(expression);

            if (product is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, message: "Product not found");

            return _mapper.Map<ProductViewModel>(product);
        }

        public async Task<ProductViewModel> CreateAsync(ProductCreateViewModel model)
        {
            var vatValue = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("VAT")["Vat_value"];

            var product = _mapper.Map<Product>(model);

            product.TotalPriceWithVat = (model.Price * model.Quantity) * (1 + Convert.ToInt32(vatValue));

            var result = (await _context.Products.AddAsync(product)).Entity;

            await _context.SaveChangesAsync();

            return _mapper.Map<ProductViewModel>(result);
        }

        public async Task UpdateAsync(long id, ProductPatchViewModel model)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
                throw new StatusCodeException(System.Net.HttpStatusCode.NotFound, message: "Product not found");

            var productAudits = new List<ProductAudit>();

            if (model.ItemName is not null)
            {
                product.ItemName = model.ItemName;
                productAudits.Add(AddToProductAudit(product.Id, model.ItemName));
            }
            if (model.Quantity > -1)
            {
                product.Quantity = model.Quantity;
                productAudits.Add(AddToProductAudit(product.Id, model.Quantity.ToString()));
            }

            if (model.Price > 0)
            {
                product.Price = model.Price;
                productAudits.Add(AddToProductAudit(product.Id, model.Price.ToString()));
            }

            _context.Products.Update(product);

            await _context.ProductAudits.AddRangeAsync(productAudits);

            await _context.SaveChangesAsync();
        }

        private ProductAudit AddToProductAudit(long id, string changed)
        {
            return new ProductAudit()
            {
                ProductId = id,
                ChangedTime = DateTime.UtcNow,
                ChangedBy = HttpContextHelper.UserId,
                Changed = changed
            };
        }

        public async Task<ProductViewModelForAdmin> GetForAdminAsync(Expression<Func<Product, bool>> expression)
        {
            var product = await _context.Products.FirstOrDefaultAsync(expression);

            if (product is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, message: "Product not found");

            return _mapper.Map<ProductViewModelForAdmin>(product);
        }
    }
}