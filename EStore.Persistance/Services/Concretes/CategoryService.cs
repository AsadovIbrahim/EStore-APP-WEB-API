using EStore.Application.Repositories.Concretes;
using EStore.Application.Services.Abstracts;
using EStore.Domain.DTO_s;
using EStore.Domain.Entities.Concretes;
using EStore.Domain.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Persistance.Services.Concretes
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task AddCategoryAsync(AddCategoryVM addCategoryVM)
        {
            var category = new Category
            {
                Name = addCategoryVM.Name
            };
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllCategoriesAsync(int page,int size)
        {
            var category = await _categoryRepository.GetAllCategoriesAsync(page,size);
            var result = category.Select(p => new Category
            {
                Id = p.Id,
                Name = p.Name,
            }).ToList();
            return result;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            var result = new Category
            {
                Id=category.Id,
                Name = category.Name
            };
            return result;
        }
        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryRepository.Delete(id);
            await _categoryRepository.SaveChangesAsync();
        }
        public async Task<HttpStatusCode> UpdateCategoryAsync(UpdateCategoryVM updateCategoryVM)
        {
            var category = await _categoryRepository.GetByIdAsync(updateCategoryVM.Id);
            if (category == null)
                return HttpStatusCode.NotFound;

            category.Name = updateCategoryVM.Name;

            await _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
