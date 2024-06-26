using EStore.Application.Repositories.Concretes;
using EStore.Application.Services.Abstracts;
using EStore.Domain.Entities.Concretes;
using EStore.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Persistance.Services.Concretes
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task AddProductAsync(AddProductVM addProductVM)
        {
            var product = new Product
            {
                Name=addProductVM.Name,
                Description=addProductVM.Description,
                CategoryId=addProductVM.CategoryId,
                Price=addProductVM.Price,
            };
            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.Delete(id);
            await _productRepository.SaveChangesAsync();
        }

        public async Task<ICollection<Product>> GetAllProductsAsync()
        {
            var products= await _productRepository.GetAllAsync();
            var productDTO = products.Select(p => new Product
            {
                Id=p.Id,
                Name=p.Name,
                Description=p.Description,
                CategoryId=p.CategoryId,
                Price=p.Price,
            }).ToList();
            return productDTO;
        }

        public async Task<ICollection<Product>> GetProductByCategoryName(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName)) {
                return null;
            }
            var result=await _productRepository.GetByCategoryNameAsync(categoryName);
            return result;
        }

        public async Task<ProductVM?> GetProductByIdAsync(int id)
        {
            var product=await _productRepository.GetByIdAsync(id);
            var productDTO = new ProductVM
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Price = product.Price,
            };
            return productDTO;
        }

        public async Task<HttpStatusCode> UpdateProductAsync(UpdateProductVM updateProductVM)
        {
            var category = await _productRepository.GetByIdAsync(updateProductVM.Id);
            if (category == null)
            {
                return HttpStatusCode.NotFound;
            }
            category.Name = updateProductVM.Name;
            category.Description = updateProductVM.Description;
            category.Price = updateProductVM.Price;
            category.CategoryId = updateProductVM.CategoryId;

            await _productRepository.Update(category);
            await _productRepository.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        public async Task DeleteProduct(int id)
        {
            await _productRepository.Delete(id); 
            await _productRepository.SaveChangesAsync();
        }
    }
}
