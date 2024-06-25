using EStore.Domain.DTO_s;
using EStore.Domain.Entities.Concretes;
using EStore.Domain.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Abstracts
{
    public interface ICategoryService
    {
        Task AddCategoryAsync(AddCategoryVM addCategoryRequestDTO);
        Task<List<Category>> GetAllCategoriesAsync(int page,int size);
        Task<Category> GetCategoryByIdAsync(int id);
        Task<HttpStatusCode> UpdateCategoryAsync(UpdateCategoryVM updateCategoryRequestDTO);
        Task DeleteCategoryAsync(int id);

    }
}
