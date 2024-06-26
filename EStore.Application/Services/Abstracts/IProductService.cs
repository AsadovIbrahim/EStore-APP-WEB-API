using EStore.Domain.Entities.Concretes;
using EStore.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Abstracts
{
    public interface IProductService
    {
        Task AddProductAsync(AddProductVM addProductVM);
        Task<ICollection<Product>> GetAllProductsAsync();
        Task<ProductVM?>GetProductByIdAsync(int id);
        Task<ICollection<Product>>GetProductByCategoryName(string categoryName);
        Task DeleteProductAsync(int id);
        Task<HttpStatusCode>UpdateProductAsync(UpdateProductVM updateProductVM);
    }
}
