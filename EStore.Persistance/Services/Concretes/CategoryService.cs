using EStore.Application.Repositories.Concretes;
using EStore.Application.Services.Abstracts;
using EStore.Domain.DTO_s;
using EStore.Domain.Entities.Concretes;
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
        public async Task<AddCategoryResponseDTO> AddCategoryAsync(AddCategoryRequestDTO addCategoryRequestDTO)
        {
            var category = new Category
            {
                Name = addCategoryRequestDTO.Name
            };
            await _categoryRepository.AddAsync(category);

            return new AddCategoryResponseDTO
            {
                Id=category.Id
            };
        }

        public async Task<RequestResponseDTO> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return new RequestResponseDTO
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = $"Category with ID {id} not found!"
                };
            }
            await _categoryRepository.Delete(category);
            return new RequestResponseDTO
            {

                StatusCode = HttpStatusCode.NoContent
            };
        }
        public async Task<List<GetAllCategoryResponseDTO>> GetAllCategoriesAsync(GetAllCategoryRequestDTO getAllCategoryRequestDTO)
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories.Select(c => new GetAllCategoryResponseDTO
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();
        }
        public async Task<GetAllCategoryResponseDTO> GetCategoryByIdAsync(int id)
        {
            var category=await _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return null;
            }
            return new GetAllCategoryResponseDTO
            {
                Id=category.Id,
                Name=category.Name,
            };
                
        }

        public async Task<RequestResponseDTO> UpdateCategoryAsync(UpdateCategoryRequestDTO updateCategoryRequestDTO)
        {
            var category = await _categoryRepository.GetByIdAsync(updateCategoryRequestDTO.Id);
            if (category == null)
            {
                return new RequestResponseDTO
                {
                    StatusCode=HttpStatusCode.NotFound,
                    Message = $"Category with ID {updateCategoryRequestDTO.Id} not found!"
                };
            }
            category.Name = updateCategoryRequestDTO.Name;
            await _categoryRepository.Update(category);

            return new RequestResponseDTO
            {
                StatusCode=HttpStatusCode.NoContent
            };
        }
    }
}
